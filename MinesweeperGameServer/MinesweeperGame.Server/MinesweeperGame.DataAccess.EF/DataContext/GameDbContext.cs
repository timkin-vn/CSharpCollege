using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MinesweeperGame.DataAccess.EF.Entities;

namespace MinesweeperGame.DataAccess.EF.DataContext
{
    public class GameDbContext : DbContext
    {
        public GameDbContext()
            : base("name=MinesweeperConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<GameDbContext>());
        }

        public DbSet<GameEntity> Games { get; set; }
        public DbSet<CellEntity> Cells { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameEntity>()
                .HasMany(g => g.Cells)
                .WithRequired(c => c.Game)
                .HasForeignKey(c => c.GameId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CellEntity>()
                .HasIndex(c => new { c.GameId, c.Row, c.Column })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}