using System.Data.Entity;
using FifteenGame.Data.Entities;
using Npgsql;

namespace FifteenGame.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext() : base("name=GameDbConnection")
        {
            Database.SetInitializer<GameDbContext>(new CreateDatabaseIfNotExists<GameDbContext>());
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<User>().ToTable("users");
        }
    }
}