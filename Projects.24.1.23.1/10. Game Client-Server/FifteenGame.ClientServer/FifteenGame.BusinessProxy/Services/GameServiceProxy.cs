using FifteenGame.Business.Models;
using FifteenGame.BusinessProxy.Infrastructure;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FifteenGame.BusinessProxy.Services
{
    public class GameServiceProxy : IGameService
    {
        private const int GemTypeCount = 5;
        private static readonly System.Random _rnd = new System.Random();

        public void Initialize(GameModel model)
        {
            model.MatchesCount = 0;
            model.IsFinished = false;
            for (int row = 0; row < GameModel.RowCount; row++)
                for (int col = 0; col < GameModel.ColumnCount; col++)
                    model[row, col] = _rnd.Next(1, GemTypeCount);
        }

        public bool Swap(GameModel model, int r1, int c1, int r2, int c2)
        {
            if (System.Math.Abs(r1 - r2) + System.Math.Abs(c1 - c2) != 1)
                return false;
            int temp = model[r1, c1];
            model[r1, c1] = model[r2, c2];
            model[r2, c2] = temp;
            var matches = CheckMatches(model);
            if (!matches.Any())
            {
                temp = model[r1, c1];
                model[r1, c1] = model[r2, c2];
                model[r2, c2] = temp;
                return false;
            }
            return true;
        }

        public List<(int row, int col)> CheckMatches(GameModel model)
        {
            var matches = new List<(int, int)>();
            for (int row = 0; row < GameModel.RowCount; row++)
                for (int col = 0; col < GameModel.ColumnCount - 2; col++)
                {
                    int val = model[row, col];
                    if (val != 0 && val == model[row, col + 1] && val == model[row, col + 2])
                    { matches.Add((row, col)); matches.Add((row, col + 1)); matches.Add((row, col + 2)); }
                }
            for (int col = 0; col < GameModel.ColumnCount; col++)
                for (int row = 0; row < GameModel.RowCount - 2; row++)
                {
                    int val = model[row, col];
                    if (val != 0 && val == model[row + 1, col] && val == model[row + 2, col])
                    { matches.Add((row, col)); matches.Add((row + 1, col)); matches.Add((row + 2, col)); }
                }
            return matches;
        }

        public void RemoveMatches(GameModel model, List<(int row, int col)> matches)
        {
            foreach (var (r, c) in matches) model[r, c] = 0;
        }

        public void Save(GameModel model)
        {
            var reply = new GameReply
            {
                Id = model.Id,
                UserId = model.UserId,
                MatchesCount = model.MatchesCount,
                IsFinished = model.IsFinished,
                Cells = new int[GameModel.RowCount * GameModel.ColumnCount],
            };
            int i = 0;
            for (int row = 0; row < GameModel.RowCount; row++)
                for (int col = 0; col < GameModel.ColumnCount; col++)
                    reply.Cells[i++] = model[row, col];

            var content = JsonContent.Create(reply);
            HttpConnection.HttpClient.PostAsync("api/game/save", content).Result.EnsureSuccessStatusCode();
        }

        public void ProcessMatches(GameModel model)
        {
            for (int col = 0; col < GameModel.ColumnCount; col++)
            {
                int writeRow = GameModel.RowCount - 1;
                for (int row = GameModel.RowCount - 1; row >= 0; row--)
                {
                    int cur = model[row, col];
                    if (cur != 0) { model[writeRow, col] = cur; if (writeRow != row) model[row, col] = 0; writeRow--; }
                }
                for (int row = writeRow; row >= 0; row--)
                    if (model[row, col] == 0) model[row, col] = _rnd.Next(1, 6);
            }
        }

        public void AddMatches(GameModel model, int count) => model.MatchesCount += count;

        public GameModel GetByUserId(int userId)
        {
            var content = JsonContent.Create(new UserIdRequest { UserId = userId });
            var response = HttpConnection.HttpClient.PostAsync("api/game/get-by-user-id", content).Result;
            response.EnsureSuccessStatusCode();
            var reply = JsonSerializer.Deserialize<GameReply>(response.Content.ReadAsStringAsync().Result);
            return FromReply(reply);
        }

        public GameModel GetByGameId(int gameId)
        {
            var content = JsonContent.Create(new GameIdRequest { GameId = gameId });
            var response = HttpConnection.HttpClient.PostAsync("api/game/get-by-game-id", content).Result;
            response.EnsureSuccessStatusCode();
            var reply = JsonSerializer.Deserialize<GameReply>(response.Content.ReadAsStringAsync().Result);
            return FromReply(reply);
        }

        public void RemoveGame(int gameId)
        {
            var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Delete, "api/game/remove");
            request.Content = JsonContent.Create(new GameIdRequest { GameId = gameId });
            HttpConnection.HttpClient.SendAsync(request).Result.EnsureSuccessStatusCode();
        }

        private GameModel FromReply(GameReply reply)
        {
            var model = new GameModel { Id = reply.Id, UserId = reply.UserId, MatchesCount = reply.MatchesCount, IsFinished = reply.IsFinished };
            int i = 0;
            for (int row = 0; row < GameModel.RowCount; row++)
                for (int col = 0; col < GameModel.ColumnCount; col++)
                    model[row, col] = reply.Cells[i++];
            return model;
        }
    }
}