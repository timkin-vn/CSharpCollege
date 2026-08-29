using System.Collections.Generic;
using System.Linq;
using Pacman.Common.Enums;
using Pacman.Common.Interfaces.Repositories;
using Pacman.Common.Models;

namespace Pacman.DataAccess.EF.Repositories
{
    public class LeaderboardRepository : ILeaderboardRepository
    {
        public IReadOnlyList<GameDto> GetTopScores(int count)
        {
            using (var db = new PacmanDbContext())
            {
                var games = db.Games
                    .OrderByDescending(g => g.Score)
                    .Take(count)
                    .ToList();

                return games.Select(g => new GameDto
                {
                    Id = g.Id,
                    UserId = g.UserId,
                    MapId = g.MapId,
                    Status = (GameStatus)g.Status,
                    Score = g.Score,
                    Lives = g.Lives,
                    CreatedAt = g.CreatedAt,
                    UpdatedAt = g.UpdatedAt
                }).ToList();
            }
        }
    }
}