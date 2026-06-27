using FifteenGame.Common.BusinessModels;
using System;

namespace FifteenGame.Common.Contracts.Services
{
    public interface IGameService
    {
        int GetUserWinStreak(int userId);
        GameModel GetByGameId(int gameId);
        GameModel GetByUserId(int userId);
        void Move(GameModel model, int row, int column);
        bool IsGameOver(int gameId);
        bool IsGameOver(GameModel model);
        GameModel RestartGame(int userId);
        void RemoveGame(int gameId);
    }
}