using _2048Game.BusinessProxy.Infractructure;
using _2048Game.Common.BusinessDtos;
using _2048Game.Common.BusinessModels;
using _2048Game.Common.Definitions;
using _2048Game.Common.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _2048Game.BusinessProxy.Services
{
    public class GameServiceProxy : IGameService
    {
        public GameModel Board { get; set; }

        public event Action BoardUpdated;
        public event Action GameOver;

        public bool CheckGameOver(int gameId)
        {
            var httpContent = JsonContent.Create(new GameIdRequest { GameId = gameId });
            
            var response = HttpConnection.HttpClient.GetAsync($"api/game/check-game-over/{gameId}").Result;
            response.EnsureSuccessStatusCode();

            return bool.Parse(response.Content.ReadAsStringAsync().Result);
        }

        public GameModel GetByGameId(int gameId)
        {
            var httpContent = JsonContent.Create(new GameIdRequest { GameId = gameId });
            var response = HttpConnection.HttpClient.PostAsync("api/game/get-by-gameId", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var reply = JsonConvert.DeserializeObject<GameReply>(content);
            return FromDto(reply);
        }

        public GameModel GetByUserId(int userId)
        {
            var httpContent = JsonContent.Create(new UserIdRequest { UserId = userId });
            var response = HttpConnection.HttpClient.PostAsync("api/game/get-by-userId", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var reply = JsonConvert.DeserializeObject<GameReply>(content);
            return FromDto(reply);
        }

        public bool Move(MoveDirections.MoveDirection direction)
        {
            var httpContent = JsonContent.Create(new MoveRequest
            {
                UserId = Board.UserId,
                direction = direction
            });

            var response = HttpConnection.HttpClient.PostAsync("api/game/move", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonConvert.DeserializeObject<MoveReply>(response.Content.ReadAsStringAsync().Result);

            ApplyTiles(Board, reply.Tiles);
            Board.MoveCount = reply.MoveCount;

            BoardUpdated?.Invoke();

            if (reply.GameOver)
            {
                GameOver?.Invoke();
            }
            return reply.Success;
        }

        public void RemoveGame(int gameId)
        {
            var httpContent = JsonContent.Create(new GameIdRequest { GameId = gameId });
            var response = HttpConnection.HttpClient.PostAsync("api/game/remove-game", httpContent).Result;
            response.EnsureSuccessStatusCode();
            Board = null;
        }

        public async Task RestartAsync()
        {
            int targetUserId = Board?.UserId ?? 0;

            var content = new StringContent(JsonConvert.SerializeObject(new {UserId = targetUserId,}), Encoding.UTF8, "application/json");
            var response = await HttpConnection.HttpClient.PostAsync("api/game/restart", content);
            
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            Board = JsonConvert.DeserializeObject<GameModel>(body);

            StartGame(targetUserId);
            
            BoardUpdated?.Invoke();
        }

        public int Save()
        {
            var httpContent = JsonContent.Create(new UserIdRequest { UserId = Board.UserId });
            var response = HttpConnection.HttpClient.PostAsync("api/game/save", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonConvert.DeserializeObject<GameReply>(response.Content.ReadAsStringAsync().Result);
            Board = FromDto(reply);
            return Board.Id;
        }

        public void StartGame(int userId)
        {
            if (Board != null && Board.UserId == userId)
            {
                return;
            }
            var existingGame = GetByUserId(userId);
            if (existingGame != null)
            {
                Board = existingGame;
            }
            else
            {
                var httpContent = JsonContent.Create(new UserIdRequest { UserId = userId });
                var response = HttpConnection.HttpClient.PostAsync("api/game/start-game", httpContent).Result;
                response.EnsureSuccessStatusCode();

                Board = GetByUserId(userId);
            }
            BoardUpdated?.Invoke();
        }

        private GameModel FromDto(GameReply reply)
        {
            if (reply == null)
            {
                return null;
            }

            var model = new GameModel
            {
                Id = reply.Id,
                UserId = reply.UserId,
                MoveCount = reply.MoveCount,
                Tiles = new int[Constants.RowCount, Constants.ColumnCount]
            };

            ApplyTiles(model, reply.Tiles);

            return model;
        }

        private void ApplyTiles(GameModel model, int[,] tiles)
        {
            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    model.Tiles[r, c] = tiles[r,c];
                }
            }         
        }
    }
}
