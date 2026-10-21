using Blackjack.BusinessProxy.Infrastructure;
using Blackjack.Common.Contracts.Services;
using Blackjack.Common.Models;
using System.Collections.Generic;

namespace Blackjack.BusinessProxy.Services
{
    // Та же IGameService, но каждый вызов уходит на сервер по HTTP
    public class GameServiceProxy : IGameService
    {
        public GameModel GetCurrentGame(int userId)
        {
            return HttpConnection.Get<GameModel>($"api/games/current?userId={userId}");
        }

        public GameModel StartNewGame(int userId)
        {
            return HttpConnection.Post<GameModel>($"api/games/new?userId={userId}");
        }

        public GameModel Hit(int gameId)
        {
            return HttpConnection.Post<GameModel>($"api/games/{gameId}/hit");
        }

        public GameModel Stand(int gameId)
        {
            return HttpConnection.Post<GameModel>($"api/games/{gameId}/stand");
        }

        public UserStatsModel GetStats(int userId)
        {
            return HttpConnection.Get<UserStatsModel>($"api/games/stats?userId={userId}");
        }

        public List<GameHistoryItem> GetHistory(int userId)
        {
            return HttpConnection.Get<List<GameHistoryItem>>($"api/games/history?userId={userId}");
        }
    }
}
