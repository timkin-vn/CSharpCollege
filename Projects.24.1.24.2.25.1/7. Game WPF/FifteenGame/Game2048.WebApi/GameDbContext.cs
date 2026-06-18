using Microsoft.EntityFrameworkCore;
using System;

namespace Game2048.WebApi
{
    public class GameSession
    {
        public int Id { get; set; } 
        public string PlayerName { get; set; }
        public int Score { get; set; } 
        public DateTime PlayedAt { get; set; } = DateTime.UtcNow; 
    }

    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

        public DbSet<GameSession> GameSessions { get; set; }
    }
}
