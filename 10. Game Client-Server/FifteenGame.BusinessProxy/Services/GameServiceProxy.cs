using FifteenGame.BusinessProxy.Infrastructure;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Dto;
using FifteenGame.Common.Services;
using System.Net.Http.Json;

namespace FifteenGame.BusinessProxy.Services
{
    public class GameServiceProxy : IGameService
    {
        public GameDto CreateGame(int userId, int mode)
        {
            var res = HttpConnection.Client.PostAsJsonAsync("api/game/create", new CreateGameRequest { UserId = userId, Mode = mode }).Result;
            return res.Content.ReadFromJsonAsync<GameDto>().Result;
        }

        public GameDto GetGame(int gameId)
        {
            return HttpConnection.Client.GetFromJsonAsync<GameDto>($"api/game/get/{gameId}").Result;
        }

        public GameDto MakeMove(int gameId, int row, int column)
        {
            var res = HttpConnection.Client.PostAsJsonAsync("api/game/move", new MoveRequest { GameId = gameId, Row = row, Column = column }).Result;
            return res.Content.ReadFromJsonAsync<GameDto>().Result;
        }
    }
}