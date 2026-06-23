using Minesweeper.Common.Models;

namespace Minesweeper.Common.Interfaces
{
    public interface IGameService
    {
        GameModel GetByUserId(int userId);
        GameModel Apply(int userId, int row, int col, CellAction action);
        bool IsWon(int userId);
        bool IsGameOver(int userId);
        void Remove(int gameId);
    }
}
