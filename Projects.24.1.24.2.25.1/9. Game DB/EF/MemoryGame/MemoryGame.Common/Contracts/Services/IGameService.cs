using MemoryGame.Common.BusinessModels;

namespace MemoryGame.Common.Contracts.Services
{
    public interface IGameService
    {
        GameModel GetByGameId(int gameId);

        GameModel GetByUserId(int userId);

        bool? IsGameOver(int gameId);

        GameModel MakeMove(int gameId, int row, int column);

        void RemoveGame(int gameId);
    }
}
