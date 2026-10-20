using FifteenGame.Common.Models;

namespace FifteenGame.Common.Contracts
{
    public interface IGameService
    {
        GameModel GetOrCreateGame(int userId);
        GameModel MakeMove(int gameId, int row, int column);
    }
}