using FifteenGame.BusinessProxy.Infastructure;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace FifteenGame.BusinessProxy.Services
{
    public class GameServiceProxy : IGameService
    {
        private static readonly JsonSerializerOptions JsonOptions =
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

        public GameModel GetByGameId(int gameId)
        {
            if (gameId <= 0)
                throw new ArgumentOutOfRangeException(nameof(gameId));

            using (var response = HttpConnection.HttpClient
                .GetAsync($"api/games/get-by-game-id/{gameId}")
                .GetAwaiter().GetResult())
            {
                return ReadGame(response);
            }
        }

        public GameModel GetByUserId(int userId)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            using (var response = HttpConnection.HttpClient
                .GetAsync($"api/games/get-by-user-id/{userId}")
                .GetAwaiter().GetResult())
            {
                return ReadGame(response);
            }
        }

        public bool? IsGameOver(int gameId)
        {
            if (gameId <= 0)
                throw new ArgumentOutOfRangeException(nameof(gameId));

            using (var response = HttpConnection.HttpClient
                .GetAsync($"api/games/is-over/{gameId}")
                .GetAwaiter().GetResult())
            {
                EnsureSuccess(response);

                string json = response.Content
                    .ReadAsStringAsync()
                    .GetAwaiter().GetResult();

                var reply = JsonSerializer.Deserialize<BooleanReply>(
                    json, JsonOptions);

                if (reply == null)
                    throw new InvalidOperationException(
                        "Сервер вернул пустой ответ о состоянии игры.");

                return reply.IsTrue;
            }
        }

        public GameModel MakeMove(int gameId, MoveDirection direction)
        {
            if (gameId <= 0)
                throw new ArgumentOutOfRangeException(nameof(gameId));

            if (!Enum.IsDefined(typeof(MoveDirection), direction) ||
                direction == MoveDirection.None)
            {
                throw new ArgumentException(
                    "Указано недопустимое направление хода.",
                    nameof(direction));
            }

            var content = JsonContent.Create(
                new MakeMoveRequest
                {
                    GameId = gameId,
                    Direction = direction.ToString()
                });

            using (content)
            using (var response = HttpConnection.HttpClient
                .PostAsync("api/games/make-move", content)
                .GetAwaiter().GetResult())
            {
                return ReadGame(response);
            }
        }

        public void RemoveGame(int gameId)
        {
            if (gameId <= 0)
                throw new ArgumentOutOfRangeException(nameof(gameId));

            using (var response = HttpConnection.HttpClient
                .DeleteAsync($"api/games/remove/{gameId}")
                .GetAwaiter().GetResult())
            {
                EnsureSuccess(response);
            }
        }

        private static GameModel ReadGame(HttpResponseMessage response)
        {
            EnsureSuccess(response);

            string json = response.Content
                .ReadAsStringAsync()
                .GetAwaiter().GetResult();

            var reply = JsonSerializer.Deserialize<GameReply>(
                json, JsonOptions);

            return FromDto(reply);
        }

        private static void EnsureSuccess(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            string details = response.Content == null
                ? string.Empty
                : response.Content.ReadAsStringAsync()
                    .GetAwaiter().GetResult();

            throw new HttpRequestException(
                $"Сервер вернул HTTP {(int)response.StatusCode} " +
                $"({response.ReasonPhrase}). {details}");
        }

        private static GameModel FromDto(GameReply game)
        {
            if (game == null)
                return null;

            int expectedCells = Constants.RowCount * Constants.ColumnCount;

            if (game.Cells == null || game.Cells.Length != expectedCells)
            {
                throw new InvalidOperationException(
                    $"Некорректный ответ сервера: ожидалось " +
                    $"{expectedCells} клеток.");
            }

            var result = new GameModel
            {
                Id = game.Id,
                UserId = game.UserId,
                MoveCount = game.MoveCount,
                FreeCellColumn = game.FreeCellColumn,
                FreeCellRow = game.FreeCellRow
            };

            int index = 0;
            int freeCells = 0;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0;
                     column < Constants.ColumnCount;
                     column++)
                {
                    int value = game.Cells[index++];
                    result[row, column] = value;

                    if (value == Constants.FreeCellValue)
                        freeCells++;
                }
            }

            if (freeCells != 1 ||
                game.FreeCellRow < 0 ||
                game.FreeCellRow >= Constants.RowCount ||
                game.FreeCellColumn < 0 ||
                game.FreeCellColumn >= Constants.ColumnCount ||
                result[game.FreeCellRow, game.FreeCellColumn]
                    != Constants.FreeCellValue)
            {
                throw new InvalidOperationException(
                    "Сервер вернул некорректное состояние игрового поля.");
            }

            return result;
        }
    }
}