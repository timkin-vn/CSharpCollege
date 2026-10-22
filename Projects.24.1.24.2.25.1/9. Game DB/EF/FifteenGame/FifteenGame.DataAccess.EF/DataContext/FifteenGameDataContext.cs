using FifteenGame.DataAccess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.DataContext
{
    public class FifteenGameDataContext : DbContext
    {
        static FifteenGameDataContext()
        {
            // Отключаем автоматический инициализатор
            Database.SetInitializer<FifteenGameDataContext>(null);

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public FifteenGameDataContext() : base("EFMain") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<CheckersGameEntity> CheckersGames { get; set; }
        public DbSet<CheckersMoveEntity> CheckersMoves { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<CheckersGameEntity>()
                .HasRequired(g => g.User)
                .WithMany(u => u.CheckersGames)
                .HasForeignKey(g => g.UserId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CheckersMoveEntity>()
                .HasRequired(m => m.Game)
                .WithMany(g => g.Moves)
                .HasForeignKey(m => m.GameId)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}