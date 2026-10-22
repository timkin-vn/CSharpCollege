using CheckersGame.Business.Contracts;
using CheckersGame.Business.Models;
using FifteenGame.BusinessProxy.Infastructure;
using FifteenGame.Common.BusinessDtos;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace FifteenGame.BusinessProxy.Services
{
    /// <summary>
    /// Клиентская реализация сервиса шашек: все вызовы уходят на сервер (WebApi).
    /// </summary>
    public class CheckersGameServiceProxy : ICheckersGameService
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public GameModel CreateNewGame(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.PostAsync($"api/checkers/create/{userId}", null).Result;
                return FromDto(ReadReply(response));
            }
        }

        public GameModel LoadGame(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/checkers/get/{gameId}").Result;
                var reply = ReadReply(response);
                if (reply == null)
                {
                    throw new InvalidOperationException($"Game {gameId} not found");
                }

                return FromDto(reply);
            }
        }

        public GameModel GetLastActiveGame(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/checkers/last-active/{userId}").Result;
                return FromDto(ReadReply(response));
            }
        }

        public void MakeMove(int gameId, int fromRow, int fromCol, int toRow, int toCol)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var request = new CheckersMakeMoveRequest
                {
                    GameId = gameId,
                    FromRow = fromRow,
                    FromCol = fromCol,
                    ToRow = toRow,
                    ToCol = toCol,
                };
                var response = httpClient.PostAsync("api/checkers/make-move", JsonContent.Create(request)).Result;
                EnsureSuccess(response);
            }
        }

        public void FinishCapture(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.PostAsync($"api/checkers/finish-capture/{gameId}", null).Result;
                EnsureSuccess(response);
            }
        }

        public bool IsGameOver(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/checkers/is-over/{gameId}").Result;
                EnsureSuccess(response);

                var reply = JsonSerializer.Deserialize<BooleanReply>(response.Content.ReadAsStringAsync().Result, JsonOptions);
                return reply?.IsTrue ?? false;
            }
        }

        public void DeleteGame(int gameId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.DeleteAsync($"api/checkers/remove/{gameId}").Result;
                EnsureSuccess(response);
            }
        }

        private static void EnsureSuccess(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    $"Сервер вернул {(int)response.StatusCode}: {response.Content.ReadAsStringAsync().Result}");
            }
        }

        private static CheckersGameReply ReadReply(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return null;
            }

            EnsureSuccess(response);

            var body = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrWhiteSpace(body) || body.Trim() == "null")
            {
                return null;
            }

            return JsonSerializer.Deserialize<CheckersGameReply>(body, JsonOptions);
        }

        private static GameModel FromDto(CheckersGameReply reply)
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
                CurrentPlayer = (Player)reply.CurrentPlayer,
            };

            int index = 0;
            for (int row = 0; row < GameModel.BoardSize; row++)
            {
                for (int col = 0; col < GameModel.BoardSize; col++)
                {
                    model[row, col] = (Piece)reply.Cells[index++];
                }
            }

            if (reply.IsCaptureInProgress)
            {
                model.StartCaptureSequence(reply.CaptureRow, reply.CaptureCol);
                if (reply.CapturedCells != null)
                {
                    foreach (var cell in reply.CapturedCells)
                    {
                        model.CapturedThisTurn.Add((cell / GameModel.BoardSize, cell % GameModel.BoardSize));
                    }
                }
            }

            return model;
        }
    }
}
