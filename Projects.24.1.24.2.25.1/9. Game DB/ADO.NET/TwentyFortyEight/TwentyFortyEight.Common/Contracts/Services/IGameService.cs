using TwentyFortyEight.Common.BusinessModels;

namespace TwentyFortyEight.Common.Contracts.Services
{
    public interface IGameService
    {
        GameModel GetByGameId(int gameId);
        GameModel GetByUserId(int userId);
        GameModel MakeMove(int gameId, MoveDirection direction);
        bool IsGameOver(int gameId);
        bool IsGameWon(int gameId);
        void RemoveGame(int gameId);
        void ResetGame(int userId);
    }
}
