using System.Data.Entity;
using Minesweeper.Common;
using Minesweeper.DataAccess.EF.Entities;

namespace Minesweeper.DataAccess.EF
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
        public DbSet<CellEntity> Cells { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Constants.Schema);

            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<GameEntity>().ToTable("Games");
            modelBuilder.Entity<CellEntity>().ToTable("Cells");

            modelBuilder.Entity<GameEntity>()
                .HasMany(g => g.Cells)
                .WithRequired()
                .HasForeignKey(c => c.GameId)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
