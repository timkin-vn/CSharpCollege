using PacmanGame.DataAccess.Entities;
using PacmanGame.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PacmanGame.DataAccess.Repositories
{
    
    public class GameStateRepository : Repository<GameStateEntity>, IGameStateRepository
    {
        public GameStateRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<GameStateEntity> GetByUserId(int userId)
        {
            return DbSet
                .Where(gs => gs.GameUserId == userId)
                .Include(gs => gs.GameUser)
                
                .OrderByDescending(gs => gs.CreatedAt)
                .ToList();
        }

        public IEnumerable<GameStateEntity> GetByUserIdWithPagination(int userId, int page, int pageSize)
        {
            return DbSet
                .Where(gs => gs.GameUserId == userId)
                .Include(gs => gs.GameUser)
                
                .OrderByDescending(gs => gs.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public GameStateEntity GetLastGameStateByUserId(int userId)
        {
            return DbSet
                .Where(gs => gs.GameUserId == userId)
                .Include(gs => gs.GameUser)
                
                .OrderByDescending(gs => gs.CreatedAt)
                .FirstOrDefault();
        }

        public GameStateEntity GetHighScoreGameStateByUserId(int userId)
        {
            return DbSet
                .Where(gs => gs.GameUserId == userId)
                .Include(gs => gs.GameUser)
                .OrderByDescending(gs => gs.Score)
                .FirstOrDefault();
        }

        public IEnumerable<GameStateEntity> GetCompletedGamesByUserId(int userId)
        {
            return DbSet
                .Where(gs => gs.GameUserId == userId && gs.IsGameOver)
                .Include(gs => gs.GameUser)
                .OrderByDescending(gs => gs.Score)
                .ToList();
        }

        public IEnumerable<GameStateEntity> GetGamesByDateRange(DateTime startDate, DateTime endDate)
        {
            return DbSet
                
                .Where(gs => gs.CreatedAt >= startDate && gs.CreatedAt <= endDate)
                .Include(gs => gs.GameUser)
                
                .OrderByDescending(gs => gs.CreatedAt)
                .ToList();
        }

        public IEnumerable<GameStateEntity> GetTopScores(int count)
        {
            return DbSet
                .Include(gs => gs.GameUser)
                .OrderByDescending(gs => gs.Score)
                .Take(count)
                .ToList();
        }

        public int GetTotalGamesCount()
        {
            return DbSet.Count();
        }

        public int GetUserGamesCount(int userId)
        {
            return DbSet.Count(gs => gs.GameUserId == userId);
        }

        public double GetAverageScore()
        {
            return DbSet.Any() ? DbSet.Average(gs => gs.Score) : 0;
        }

        public double GetUserAverageScore(int userId)
        {
            return DbSet
                .Where(gs => gs.GameUserId == userId)
                .Any()
                ? DbSet.Where(gs => gs.GameUserId == userId).Average(gs => gs.Score)
                : 0;
        }

        public Dictionary<int, int> GetGamesCountByLevel()
        {
            return DbSet
                .GroupBy(gs => gs.Level)
                .Select(g => new { Level = g.Key, Count = g.Count() })
                .ToDictionary(x => x.Level, x => x.Count);
        }

        public Dictionary<string, int> GetGamesCountByMonth()
        {
            return DbSet
                
                .GroupBy(gs => new { Year = gs.CreatedAt.Year, Month = gs.CreatedAt.Month })
                .Select(g => new
                {
                    MonthYear = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Count = g.Count()
                })
                .ToDictionary(x => x.MonthYear, x => x.Count);
        }

        
        public override GameStateEntity GetById(int id)
        {
            return DbSet
                .Include(gs => gs.GameUser)
                
                .FirstOrDefault(gs => gs.Id == id);
        }

        public override IEnumerable<GameStateEntity> GetAll()
        {
            return DbSet
                .Include(gs => gs.GameUser)
                
                .OrderByDescending(gs => gs.CreatedAt)
                .ToList();
        }
    }
}