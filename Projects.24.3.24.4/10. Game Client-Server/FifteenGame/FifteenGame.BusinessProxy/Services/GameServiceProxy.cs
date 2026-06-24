using FifteenGame.BusinessProxy.Infrastucture;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FifteenGame.BusinessProxy.Services
{
    public class GameServiceProxy : IGameService
    {
        public GameModel GetByGameId(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/get-by-game-id/{gameId}").Result;
                response.EnsureSuccessStatusCode();

                var reply = JsonSerializer.Deserialize<GameReply>(response.Content.ReadAsStringAsync().Result);
                return FromDto(reply);
            }
        }

        public GameModel GetByUserId(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/get-by-user-id/{userId}").Result;
                response.EnsureSuccessStatusCode();

                var reply = JsonSerializer.Deserialize<GameReply>(response.Content.ReadAsStringAsync().Result);
                return FromDto(reply);
            }
        }

        public bool IsGameOver(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/is-over/{gameId}").Result;
                response.EnsureSuccessStatusCode();

                var reply = JsonSerializer.Deserialize<BooleanReply>(response.Content.ReadAsStringAsync().Result);
                return reply.IsTrue;
            }
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var httpContent = JsonContent.Create(new MakeMoveRequest { GameId = gameId, Direction = direction.ToString() });
                var response = httpClient.PostAsync("api/games/make-move", httpContent).Result;
                response.EnsureSuccessStatusCode();

                var reply = JsonSerializer.Deserialize<GameReply>(response.Content.ReadAsStringAsync().Result);
                return FromDto(reply);
            }
        }

        public void RemoveGame(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.DeleteAsync($"api/games/remove/{gameId}").Result;
                response.EnsureSuccessStatusCode();
            }
        }

        private GameModel FromDto(GameReply game)
        {
            if (game == null)
            {
                return null;
            }

            var result = new GameModel
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                FreeCellColumn = game.FreeCellColumn,
                FreeCellRow = game.FreeCellRow,
            };

            int i = 0;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result[row, column] = game.Cells[i++];
                }
            }

            return result;
        }
    }
}
