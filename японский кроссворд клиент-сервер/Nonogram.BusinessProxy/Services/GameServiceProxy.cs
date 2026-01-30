using Newtonsoft.Json;
using Nonogram.Common.BusinessDtos;
using Nonogram.Common.BusinessModels;
using Nonogram.Common.BusinessDtos;
using Nonogram.Common.BusinessModels;
using Nonogram.Common.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text; // Добавьте эту строку

namespace Nonogram.BusinessProxy.Services
{
    public class GameServiceProxy : IGameService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _jsonSettings;

        public GameServiceProxy()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5001/"), // Изменили на 5001
                Timeout = TimeSpan.FromSeconds(30)
            };

            // Добавьте заголовки
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            _jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None,
                NullValueHandling = NullValueHandling.Ignore
            };

            Console.WriteLine($"GameServiceProxy initialized with BaseAddress: {_httpClient.BaseAddress}");
        }

        public void InitializeGame(GameModel model)
        {
            // Инициализация происходит на сервере при создании игры
            throw new NotImplementedException("Use GetByUserId for initialization");
        }

        public GameModel MakeMove(int gameId, int row, int column)
        {
            var request = new MakeMoveRequest
            {
                GameId = gameId,
                Row = row,
                Column = column
            };

            var json = JsonConvert.SerializeObject(request, _jsonSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("api/game/make-move", content).Result;
            response.EnsureSuccessStatusCode();

            var responseJson = response.Content.ReadAsStringAsync().Result;
            var reply = JsonConvert.DeserializeObject<GameReply>(responseJson, _jsonSettings);

            return FromDto(reply);
        }

        public bool IsGameOver(int gameId)
        {
            var request = new GameIdRequest { GameId = gameId };
            var json = JsonConvert.SerializeObject(request, _jsonSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("api/game/is-over", content).Result;
            response.EnsureSuccessStatusCode();

            var responseJson = response.Content.ReadAsStringAsync().Result;
            var reply = JsonConvert.DeserializeObject<BooleanReply>(responseJson, _jsonSettings);

            return reply.IsTrue;
        }

        public bool IsGameWon(int gameId)
        {
            var request = new GameIdRequest { GameId = gameId };
            var json = JsonConvert.SerializeObject(request, _jsonSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("api/game/is-won", content).Result;
            response.EnsureSuccessStatusCode();

            var responseJson = response.Content.ReadAsStringAsync().Result;
            var reply = JsonConvert.DeserializeObject<BooleanReply>(responseJson, _jsonSettings);

            return reply.IsTrue;
        }

        public GameModel GetByUserId(int userId)
        {
            Console.WriteLine($"GameServiceProxy.GetByUserId called with userId: {userId}");

            try
            {
                var request = new UserIdRequest { UserId = userId };
                var json = JsonConvert.SerializeObject(request, _jsonSettings);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine($"Sending request to: api/game/get-by-user-id");
                Console.WriteLine($"Request content: {json}");

                var response = _httpClient.PostAsync("api/game/get-by-user-id", content).Result;

                Console.WriteLine($"Response status: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"Error response: {errorContent}");
                    return null;
                }

                var responseJson = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Response received, length: {responseJson.Length}");

                var reply = JsonConvert.DeserializeObject<GameReply>(responseJson, _jsonSettings);

                if (reply == null)
                {
                    Console.WriteLine("Deserialized reply is null!");
                    return null;
                }

                Console.WriteLine($"Reply: Id={reply.Id}, UserId={reply.UserId}, Mistakes={reply.MistakesCount}");
                Console.WriteLine($"RowClues count: {reply.RowClues?.Count ?? 0}");
                Console.WriteLine($"ColumnClues count: {reply.ColumnClues?.Count ?? 0}");
                Console.WriteLine($"Cells length: {reply.Cells?.Length ?? 0}");

                return FromDto(reply);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetByUserId: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public GameModel GetByGameId(int gameId)
        {
            var request = new GameIdRequest { GameId = gameId };
            var json = JsonConvert.SerializeObject(request, _jsonSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("api/game/get-by-game-id", content).Result;
            response.EnsureSuccessStatusCode();

            var responseJson = response.Content.ReadAsStringAsync().Result;
            var reply = JsonConvert.DeserializeObject<GameReply>(responseJson, _jsonSettings);

            return FromDto(reply);
        }

        public void RemoveGame(int gameId)
        {
            var request = new GameIdRequest { GameId = gameId };
            var json = JsonConvert.SerializeObject(request, _jsonSettings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "api/game/remove")
            {
                Content = content
            };

            var response = _httpClient.SendAsync(requestMessage).Result;
            response.EnsureSuccessStatusCode();
        }

        private GameModel FromDto(GameReply reply)
        {
            if (reply == null)
            {
                Console.WriteLine("FromDto: reply is null!");
                return null;
            }

            var model = new GameModel
            {
                Id = reply.Id,
                UserId = reply.UserId,
                MistakesCount = reply.MistakesCount
            };

            Console.WriteLine($"FromDto: Model created with Id={model.Id}");

            // Очищаем и заполняем подсказки
            model.RowClues.Clear();
            model.ColumnClues.Clear();

            if (reply.RowClues != null)
            {
                Console.WriteLine($"FromDto: Adding {reply.RowClues.Count} row clues");
                foreach (var clue in reply.RowClues)
                {
                    model.RowClues.Add(new System.Collections.Generic.List<int>(clue));
                }
            }
            else
            {
                Console.WriteLine("FromDto: RowClues is null!");
            }

            if (reply.ColumnClues != null)
            {
                Console.WriteLine($"FromDto: Adding {reply.ColumnClues.Count} column clues");
                foreach (var clue in reply.ColumnClues)
                {
                    model.ColumnClues.Add(new System.Collections.Generic.List<int>(clue));
                }
            }
            else
            {
                Console.WriteLine("FromDto: ColumnClues is null!");
            }

            // Заполняем клетки
            if (reply.Cells != null)
            {
                Console.WriteLine($"FromDto: Cells array length = {reply.Cells.Length}");

                if (reply.Cells.Length == Common.Definitions.Constants.RowCount * Common.Definitions.Constants.ColumnCount)
                {
                    for (int row = 0; row < Common.Definitions.Constants.RowCount; row++)
                    {
                        for (int column = 0; column < Common.Definitions.Constants.ColumnCount; column++)
                        {
                            model[row, column] = reply.Cells[row * Common.Definitions.Constants.ColumnCount + column];
                        }
                    }
                    Console.WriteLine("FromDto: Cells populated successfully");
                }
                else
                {
                    Console.WriteLine($"FromDto: ERROR! Expected {Common.Definitions.Constants.RowCount * Common.Definitions.Constants.ColumnCount} cells, got {reply.Cells.Length}");
                }
            }
            else
            {
                Console.WriteLine("FromDto: Cells array is null!");
            }

            return model;
        }
    }
}