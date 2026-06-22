using Game2048.Common.Models;

namespace Game2048.Common.Interfaces
{
    public interface IGameRepository
    {
        GameModel GetByUserId(int userId);
        void SaveGame(GameModel game);
        void Remove(int gameId);
    }
}
