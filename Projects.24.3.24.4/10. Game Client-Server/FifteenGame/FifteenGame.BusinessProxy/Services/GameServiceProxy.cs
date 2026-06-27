using FifteenGame.BusinessProxy.Infrastucture;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using Newtonsoft.Json; // Добавили ньютонсофт для двумерных массивов
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
        // 1. Получаем стрик побед из базы данных сервера по HTTP
        public int GetUserWinStreak(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/win-streak/{userId}").Result;
                response.EnsureSuccessStatusCode();

                // Здесь простой int, можно оставить стандартный или перевести на Newtonsoft
                var reply = System.Text.Json.JsonSerializer.Deserialize<int>(response.Content.ReadAsStringAsync().Result);
                return reply;
            }
        }

        public GameModel GetByGameId(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/get-by-game-id/{gameId}").Result;
                response.EnsureSuccessStatusCode();

                // Исправлено: десериализация через Newtonsoft.Json
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var reply = JsonConvert.DeserializeObject<GameReply>(jsonString);
                return FromDto(reply);
            }
        }

        public GameModel GetByUserId(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/get-by-user-id/{userId}").Result;
                response.EnsureSuccessStatusCode();

                // Исправлено: десериализация через Newtonsoft.Json
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var reply = JsonConvert.DeserializeObject<GameReply>(jsonString);
                return FromDto(reply);
            }
        }

        // 2. Метод ХОДА (Постройка ларька) отправляет координаты клика на сервер
        public void Move(GameModel model, int row, int column)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                // Упаковываем запрос в DTO объект
                var requestData = new MakeMoveRequest
                {
                    GameId = model.Id,
                    Row = row,
                    Column = column
                };

                var httpContent = JsonContent.Create(requestData);
                var response = httpClient.PostAsync("api/games/move", httpContent).Result;
                response.EnsureSuccessStatusCode();

                // Исправлено: десериализация через Newtonsoft.Json
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var reply = JsonConvert.DeserializeObject<GameReply>(jsonString);

                // Обновляем нашу локальную модельку на клиенте полученными данными
                UpdateModelFromReply(model, reply);
            }
        }

        public bool IsGameOver(GameModel model)
        {
            // Логику окончания игры можно оставить прямо на клиенте, чтобы не слать лишний запрос
            return model.TurnsPlayed >= Constants.TargetTurns || model.Money < 0;
        }

        public bool IsGameOver(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/is-over/{gameId}").Result;
                response.EnsureSuccessStatusCode();

                var reply = System.Text.Json.JsonSerializer.Deserialize<BooleanReply>(response.Content.ReadAsStringAsync().Result);
                return reply.IsTrue;
            }
        }

        public GameModel RestartGame(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.PostAsync($"api/games/restart/{userId}", null).Result;
                response.EnsureSuccessStatusCode();

                // Исправлено: десериализация через Newtonsoft.Json
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var reply = JsonConvert.DeserializeObject<GameReply>(jsonString);
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

        // Вспомогательный метод для обновления существующей модели (чтобы сетка Grid не перерисовывалась с нуля)
        private void UpdateModelFromReply(GameModel model, GameReply reply)
        {
            if (model == null || reply == null) return;

            model.Money = reply.Money;
            model.TurnsPlayed = reply.MoveCount;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    model.SetPeopleCount(row, column, reply.PeopleCount[row, column]);
                    model.SetHasShop(row, column, reply.HasShop[row, column]);
                    model.SetIsVeggie(row, column, reply.IsVeggie[row, column]);
                    model.SetIsRevealed(row, column, reply.IsRevealed[row, column]);
                }
            }
        }

        // Преобразование из сетевого ответа (GameReply) в полноценную модельку GameModel
        private GameModel FromDto(GameReply game)
        {
            if (game == null) return null;

            var result = new GameModel
            {
                Id = game.Id,
                UserId = game.UserId,
                Money = game.Money,
                TurnsPlayed = game.MoveCount
            };

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    result.SetPeopleCount(row, column, game.PeopleCount[row, column]);
                    result.SetHasShop(row, column, game.HasShop[row, column]);
                    result.SetIsVeggie(row, column, game.IsVeggie[row, column]);
                    result.SetIsRevealed(row, column, game.IsRevealed[row, column]);
                }
            }

            return result;
        }
    }
}