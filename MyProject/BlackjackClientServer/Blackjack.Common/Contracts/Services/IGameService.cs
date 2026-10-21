using Blackjack.Common.Models;
using System.Collections.Generic;

namespace Blackjack.Common.Contracts.Services
{
    public interface IGameService
    {
        // Незаконченная игра пользователя или новая, если такой нет
        GameModel GetCurrentGame(int userId);

        GameModel StartNewGame(int userId);

        GameModel Hit(int gameId);

        GameModel Stand(int gameId);

        UserStatsModel GetStats(int userId);

        List<GameHistoryItem> GetHistory(int userId);
    }
}
