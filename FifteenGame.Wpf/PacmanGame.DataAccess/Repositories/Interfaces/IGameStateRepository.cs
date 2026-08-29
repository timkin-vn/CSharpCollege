using PacmanGame.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace PacmanGame.DataAccess.Repositories.Interfaces
{
    public interface IGameStateRepository : IRepository<GameStateEntity>
    {
        IEnumerable<GameStateEntity> GetByUserId(int userId);
        IEnumerable<GameStateEntity> GetByUserIdWithPagination(int userId, int page, int pageSize);
        GameStateEntity GetLastGameStateByUserId(int userId);
        GameStateEntity GetHighScoreGameStateByUserId(int userId);
        IEnumerable<GameStateEntity> GetCompletedGamesByUserId(int userId);
        IEnumerable<GameStateEntity> GetGamesByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<GameStateEntity> GetTopScores(int count);
        int GetTotalGamesCount();
        int GetUserGamesCount(int userId);
        double GetAverageScore();
        double GetUserAverageScore(int userId);
        Dictionary<int, int> GetGamesCountByLevel();
        Dictionary<string, int> GetGamesCountByMonth();
    }
}