using Game2048.Common.Models;
using Game2048.Common.Dtos;

namespace Game2048.Common.Repositories
{
    public interface IGameRepository
    {
        GameDto GetByGameId(int gameId);
        int CreateGame(GameModel game);
        void UpdateGame(int gameId, GameModel game);
        void DeleteGame(int gameId);
        GameDto GetLatestGame(int userId);
    }
}
