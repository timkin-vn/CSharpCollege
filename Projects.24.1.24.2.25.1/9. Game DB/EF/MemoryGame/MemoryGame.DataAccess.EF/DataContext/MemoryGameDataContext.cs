using MemoryGame.DataAccess.EF.Entities;
using MemoryGame.DataAccess.EF.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.DataAccess.EF.DataContext
{
    public class MemoryGameDataContext : DbContext
    {
        static MemoryGameDataContext()
        {
            // При первом обращении к БД недостающие миграции применяются автоматически
            // (то же самое, что команда Update-Database)
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MemoryGameDataContext, Configuration>());
        }

        public MemoryGameDataContext() : base("EFMain")
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Cell> Cells { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
