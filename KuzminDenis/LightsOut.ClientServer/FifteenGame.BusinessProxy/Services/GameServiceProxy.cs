using FifteenGame.BusinessProxy.Infrastructure;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Services;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace FifteenGame.BusinessProxy.Services
{
    public class GameServiceProxy : IGameService
    {
        public GameModel GetByGameId(int gameId)
        {
            var httpContent = JsonContent.Create(new GameIdRequest { GameId = gameId });
            var response = HttpConnection.HttpClient
                .PostAsync("api/game/get-by-game-id", httpContent).Result;

            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<GameReply>(
                response.Content.ReadAsStringAsync().Result);

            return FromDto(reply);
        }

        public GameModel GetByUserId(int userId)
        {
            var httpContent = JsonContent.Create(new UserIdRequest { UserId = userId });
            var response = HttpConnection.HttpClient
                .PostAsync("api/game/get-by-user-id", httpContent).Result;

            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<GameReply>(
                response.Content.ReadAsStringAsync().Result);

            return FromDto(reply);
        }

        public bool IsGameOver(int gameId)
        {
            var httpContent = JsonContent.Create(new GameIdRequest { GameId = gameId });
            var response = HttpConnection.HttpClient
                .PostAsync("api/game/is-over", httpContent).Result;

            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<BooleanReply>(
                response.Content.ReadAsStringAsync().Result);

            return reply.IsTrue;
        }

        public GameModel MakeMove(int gameId, int row, int column)
        {
            var httpContent = JsonContent.Create(new MakeMoveRequest
            {
                GameId = gameId,
                Row = row,
                Column = column
            });

            var response = HttpConnection.HttpClient
                .PostAsync("api/game/make-move", httpContent).Result;

            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<GameReply>(
                response.Content.ReadAsStringAsync().Result);

            return FromDto(reply);
        }

        public void RemoveGame(int gameId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "api/game/remove")
            {
                Content = JsonContent.Create(new GameIdRequest { GameId = gameId })
            };

            var response = HttpConnection.HttpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
        }

        private GameModel FromDto(GameReply game)
        {
            if (game == null)
                return null;

            var result = new GameModel
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount
            };

            int i = 0;
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    result[row, column] = game.Cells[i++] != 0;
                }
            }

            return result;
        }
    }
}
