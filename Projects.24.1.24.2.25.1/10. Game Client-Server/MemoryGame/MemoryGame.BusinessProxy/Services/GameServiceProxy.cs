using MemoryGame.BusinessProxy.Infastructure;
using MemoryGame.Common.BusinessDtos;
using MemoryGame.Common.BusinessModels;
using MemoryGame.Common.Contracts.Services;
using MemoryGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MemoryGame.BusinessProxy.Services
{
    public class GameServiceProxy : IGameService
    {
        public GameModel GetByGameId(int gameId)
        {
            return Task.Run(() => GetByGameIdAsync(gameId)).Result;
        }

        private async Task<GameModel> GetByGameIdAsync(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = await httpClient.GetAsync($"api/games/get-by-game-id/{gameId}").ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var reply = JsonSerializer.Deserialize<GameReply>(json);
                return FromDto(reply);
            }
        }

        public GameModel GetByUserId(int userId)
        {
            return Task.Run(() => GetByUserIdAsync(userId)).Result;
        }

        private async Task<GameModel> GetByUserIdAsync(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = await httpClient.GetAsync($"api/games/get-by-user-id/{userId}").ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var reply = JsonSerializer.Deserialize<GameReply>(json);
                return FromDto(reply);
            }
        }

        public bool? IsGameOver(int gameId)
        {
            return Task.Run(() => IsGameOverAsync(gameId)).Result;
        }

        private async Task<bool?> IsGameOverAsync(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = await httpClient.GetAsync($"api/games/is-over/{gameId}").ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var reply = JsonSerializer.Deserialize<BooleanReply>(json);
                return reply.IsTrue;
            }
        }

        public GameModel MakeMove(int gameId, int row, int column)
        {
            return Task.Run(() => MakeMoveAsync(gameId, row, column)).Result;
        }

        private async Task<GameModel> MakeMoveAsync(int gameId, int row, int column)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var json = JsonSerializer.Serialize(new MakeMoveRequest { GameId = gameId, Row = row, Column = column });
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/games/make-move", httpContent).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var replyJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var reply = JsonSerializer.Deserialize<GameReply>(replyJson);
                return FromDto(reply);
            }
        }

        public void RemoveGame(int gameId)
        {
            Task.Run(() => RemoveGameAsync(gameId)).Wait();
        }

        private async Task RemoveGameAsync(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = await httpClient.DeleteAsync($"api/games/remove/{gameId}").ConfigureAwait(false);
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
            };

            int i = 0;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result[row, column] = game.Cells[i];
                    if (game.States != null)
                    {
                        result.SetState(row, column, (CardState)game.States[i]);
                    }

                    i++;
                }
            }

            return result;
        }
    }
}
