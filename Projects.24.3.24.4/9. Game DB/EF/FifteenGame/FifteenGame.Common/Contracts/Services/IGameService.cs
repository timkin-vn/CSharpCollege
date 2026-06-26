using FifteenGame.Common.BusinessModels;
using System;

namespace FifteenGame.Common.Contracts.Services
{
    public interface IGameService
    {
        GameModel GetByGameId(int gameId);
        GameModel GetByUserId(int userId);
        void Initialize(GameModel model);

        void Move(GameModel model, int row, int column);

        bool IsGameOver(int gameId);
        bool IsGameOver(GameModel model);
        GameModel RestartGame(int userId);

        // Методы для визуала
        bool IsCellCovered(GameModel model, int row, int column);
        int GetVeggieNeighborsCount(GameModel model, int r, int c);
        int GetUserWinStreak(int userId);
        void RemoveGame(int gameId);
    }
}