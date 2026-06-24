using FifteenGame.Common.BusinessModels;

namespace FifteenGame.Common.Contracts.Services
{
    public interface IGameService
    {
        GameModel GetByGameId(int gameId);
        GameModel GetByUserId(int userId);
        bool? IsGameOver(int gameId);
        GameModel MakeMove(int gameId, MoveDirection direction);
        void RemoveGame(int gameId);
    }
}