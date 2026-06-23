using System.Data.Entity;
using SeaBattle.Common;
using SeaBattle.DataAccess.EF.Entities;

namespace SeaBattle.DataAccess.EF
{
    public class GameDbContext : DbContext
    {
        static GameDbContext()
        {
            Database.SetInitializer<GameDbContext>(null);
        }

        public GameDbContext() : base("name=" + Constants.ConnectionStringName)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<GameEntity> Games { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Constants.Schema);
            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<GameEntity>().ToTable("Games");
            base.OnModelCreating(modelBuilder);
        }
    }
}
