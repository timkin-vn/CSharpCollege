using FifteenGame.Common.BusinessModels;

namespace FifteenGame.Common.Contracts.Services
{
    public interface IGameService
    {
        GameModel GetByGameId(int gameId);

        GameModel GetByUserId(int userId);

        GameModel MoveLeft(int gameId);

        GameModel MoveRight(int gameId);

        GameModel MoveUp(int gameId);

        GameModel MoveDown(int gameId);

        bool IsGameOver(int gameId);

        void RemoveGame(int gameId);
    }
}