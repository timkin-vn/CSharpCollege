using System.Data.Entity;
using Pacman.DataAccess.EF.Entities;

namespace Pacman.DataAccess.EF
{
    public class PacmanDbContext : DbContext
    {
        public PacmanDbContext() : base("name=PacmanDb")
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MapEntity> Maps { get; set; }
        public DbSet<MapCellEntity> MapCells { get; set; }
        public DbSet<GameEntity> Games { get; set; }
        public DbSet<GameActorEntity> GameActors { get; set; }
        public DbSet<GameCollectibleStateEntity> GameCollectibleStates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Указываем схему (по умолчанию public в PostgreSQL)
            modelBuilder.HasDefaultSchema("public");

            // Маппинг имен таблиц из C# в PostgreSQL
            modelBuilder.Entity<UserEntity>().ToTable("users");
            modelBuilder.Entity<MapEntity>().ToTable("maps");
            modelBuilder.Entity<MapCellEntity>().ToTable("map_cells");
            modelBuilder.Entity<GameEntity>().ToTable("games");
            modelBuilder.Entity<GameActorEntity>().ToTable("game_actors");

            // ИСПРАВЛЕНО: таблица без 's' в конце!
            modelBuilder.Entity<GameCollectibleStateEntity>().ToTable("game_collectible_state");

            base.OnModelCreating(modelBuilder);
        }
    }
}
