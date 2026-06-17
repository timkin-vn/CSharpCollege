using FifteenGame.Common.BusinessModels;
using System.Collections.Generic;

namespace FifteenGame.Common.Contracts.Services
{
    public interface IGameService
    {
        GameModel GetByGameId(int gameId);
        GameModel GetByUserId(int userId);
        bool IsGameOver(int gameId);
        bool IsGameOver(GameModel model);  // ← ДОБАВИТЬ ЭТОТ МЕТОД
        GameModel MakeMove(int gameId, MoveDirection direction);
        void RemoveGame(int gameId);
    }
}