using Game2048.Common.Models;

namespace Game2048.Common.Interfaces
{
    public interface IGameService
    {
        GameModel GetByUserId(int userId);
        GameModel MakeMove(int userId, MoveDirection direction);
        bool IsWon(int userId);
        bool IsGameOver(int userId);
        void Remove(int gameId);
    }
}
