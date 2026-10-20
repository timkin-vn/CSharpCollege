using FifteenGame.DataAccess.EF.Entities;
using FifteenGame.DataAccess.EF.Migrations;
using System.Data.Entity;

namespace FifteenGame.DataAccess.EF.DataContext
{
    public class FifteenGameDataContext : DbContext
    {
        static FifteenGameDataContext()
        {
            // При первом обращении приводит БД к актуальной модели (создаёт MazeGames, удаляет старые Games и Cells).
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FifteenGameDataContext, Configuration>());
        }

        public FifteenGameDataContext() : base("EFMain")
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<MazeGame> MazeGames { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}