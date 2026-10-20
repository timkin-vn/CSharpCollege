using FifteenGame.DataAccess.EF.Entities;
using System.Data.Entity;

namespace FifteenGame.DataAccess.EF.DataContext
{
    public class GameContext : DbContext
    {
        public GameContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<GameEntity> Games { get; set; }
    }
}