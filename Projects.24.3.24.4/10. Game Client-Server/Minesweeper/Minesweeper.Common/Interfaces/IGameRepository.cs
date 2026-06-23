using Minesweeper.Common.Models;

namespace Minesweeper.Common.Interfaces
{
    public interface IGameRepository
    {
        GameModel GetByUserId(int userId);
        void SaveGame(GameModel game);
        void Remove(int gameId);
    }
}
