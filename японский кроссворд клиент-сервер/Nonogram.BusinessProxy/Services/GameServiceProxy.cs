using Nonogram.BusinessProxy.Infrastructure;
using Nonogram.Common.BusinessDtos;
using Nonogram.Common.BusinessModels;
using Nonogram.Common.Definitions;
using Nonogram.Common.Services;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Nonogram.BusinessProxy.Services
{
    public class GameServiceProxy : IGameService
    {
        public void InitializeGame(GameModel model)
        {
            // Этот метод не используется через прокси
            throw new NotImplementedException("Используйте GetByUserId для получения инициализированной игры");
        }

        public GameModel MakeMove(int gameId, int row, int column)
        {
            var httpContent = JsonContent.Create(new MakeMoveRequest
            {
                GameId = gameId,
                Row = row,
                Column = column
            });

            var response = HttpConnection.HttpClient.PostAsync("api/game/make-move", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<GameReply>(response.Content.ReadAsStringAsync().Result);
            return FromDto(reply);
        }

        public bool IsGameOver(int gameId)
        {
            var httpContent = JsonContent.Create(new GameIdRequest { GameId = gameId });
            var response = HttpConnection.HttpClient.PostAsync("api/game/is-over", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<BooleanReply>(response.Content.ReadAsStringAsync().Result);
            return reply.IsTrue;
        }

        public bool IsGameWon(int gameId)
        {
            var httpContent = JsonContent.Create(new GameIdRequest { GameId = gameId });
            var response = HttpConnection.HttpClient.PostAsync("api/game/is-won", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<BooleanReply>(response.Content.ReadAsStringAsync().Result);
            return reply.IsTrue;
        }

        public GameModel GetByUserId(int userId)
        {
            var httpContent = JsonContent.Create(new UserIdRequest { UserId = userId });
            var response = HttpConnection.HttpClient.PostAsync("api/game/get-by-user-id", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<GameReply>(response.Content.ReadAsStringAsync().Result);
            return FromDto(reply);
        }

        public GameModel GetByGameId(int gameId)
        {
            var httpContent = JsonContent.Create(new GameIdRequest { GameId = gameId });
            var response = HttpConnection.HttpClient.PostAsync("api/game/get-by-game-id", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<GameReply>(response.Content.ReadAsStringAsync().Result);
            return FromDto(reply);
        }

        public void RemoveGame(int gameId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "api/game/remove");
            request.Content = JsonContent.Create(new GameIdRequest { GameId = gameId });
            var response = HttpConnection.HttpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
        }

        private GameModel FromDto(GameReply reply)
        {
            if (reply == null) return null;

            var model = new GameModel
            {
                Id = reply.Id,
                UserId = reply.UserId,
                MistakesCount = reply.MistakesCount
            };

            // Заполняем подсказки
            model.RowClues.Clear();
            model.ColumnClues.Clear();

            foreach (var clue in reply.RowClues)
            {
                model.RowClues.Add(clue);
            }

            foreach (var clue in reply.ColumnClues)
            {
                model.ColumnClues.Add(clue);
            }

            // Заполняем клетки
            int cellIndex = 0;
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model[row, column] = reply.Cells[cellIndex++];
                }
            }

            return model;
        }
    }
}