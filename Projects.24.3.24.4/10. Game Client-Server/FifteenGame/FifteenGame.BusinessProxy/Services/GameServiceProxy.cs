using FifteenGame.BusinessProxy.Infrastucture;
using FifteenGame.Common.Contracts;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FifteenGame.BusinessProxy.Services
{
    public class GameServiceProxy : IGameService
    {
        public GameModel GetOrCreateGame(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/get-by-user-id/{userId}").Result;
                response.EnsureSuccessStatusCode();

                var json = response.Content.ReadAsStringAsync().Result;
                var reply = JsonConvert.DeserializeObject<GameReply>(json);
                return FromDto(reply);
            }
        }

        public GameModel MakeMove(int gameId, int row, int column)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var request = new MakeMoveRequest { GameId = gameId, Row = row, Column = column };
                var json = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = httpClient.PostAsync("api/games/make-move", httpContent).Result;
                response.EnsureSuccessStatusCode();

                var replyJson = response.Content.ReadAsStringAsync().Result;
                var reply = JsonConvert.DeserializeObject<GameReply>(replyJson);
                return FromDto(reply);
            }
        }

        private GameModel FromDto(GameReply reply)
        {
            if (reply == null) return null;
            return new GameModel
            {
                Id = reply.Id,
                UserId = reply.UserId,
                BoardState = reply.BoardState,
                Result = (GameResult)reply.Result,
                PlayerScore = reply.PlayerScore,
                ComputerScore = reply.ComputerScore
            };
        }
    }
}