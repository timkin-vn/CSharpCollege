using FifteenGame.BusinessProxy.Infrastructure;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Services;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace FifteenGame.BusinessProxy.Services
{
    public class GameServiceProxy : IGameService
    {
        private readonly JsonSerializerOptions _options;

        public GameServiceProxy()
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public StartGameReply StartNewGame(int userId)
        {
            
            var response = HttpConnection.HttpClient.PostAsync($"api/game/start?userId={userId}", null).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            
            var reply = JsonSerializer.Deserialize<StartGameReply>(json, _options);

            return reply;
        }

        
        public StartGameReply StartNewGame()
        {
            throw new NotImplementedException("Use StartNewGame(userId)");
        }

        public GameReply MakeMove(MakeMoveRequest request)
        {
            var httpContent = JsonContent.Create(request);
            var response = HttpConnection.HttpClient.PostAsync("api/game/shoot", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var reply = JsonSerializer.Deserialize<GameReply>(json, _options);

            return reply;
        }



        
    }
}