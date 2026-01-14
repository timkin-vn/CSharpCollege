using FifteenGame.DataAccess.Entities;
using System.Data.Entity;

namespace FifteenGame.DataAccess.DataContext
{
    public class GameDbContext : DbContext
    {
        public GameDbContext() : base("name=MainConnection") { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<GameEntity> Games { get; set; }
    }
}