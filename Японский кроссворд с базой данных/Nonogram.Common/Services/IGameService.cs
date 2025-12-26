using Nonogram.Common.BusinessModels;

namespace Nonogram.Common.Services
{
    public interface IGameService
    {
        void InitializeGame(GameModel model);

        GameModel MakeMove(int gameId, int row, int column);

        bool IsGameOver(int gameId);

        bool IsGameWon(int gameId);

        GameModel GetByUserId(int userId);

        GameModel GetByGameId(int gameId);

        void RemoveGame(int gameId);
    }
}