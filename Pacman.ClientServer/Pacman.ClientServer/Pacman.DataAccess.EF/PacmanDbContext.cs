using System.Data.Entity;
using Pacman.DataAccess.EF.Entities;

namespace Pacman.DataAccess.EF
{
    [DbConfigurationType(typeof(NpgsqlConfiguration))]
    public class PacmanDbContext : DbContext
    {
        public PacmanDbContext() : base("name=PacmanDb")
        {
            // Отключаем автоматические миграции - используем существующую БД
            Database.SetInitializer<PacmanDbContext>(null);

            // Отключаем Lazy Loading для предотвращения проблем с сериализацией
            Configuration.LazyLoadingEnabled = false;

            // Отключаем Proxy Creation для предотвращения создания динамических proxy-объектов
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MapEntity> Maps { get; set; }
        public DbSet<MapCellEntity> MapCells { get; set; }
        public DbSet<GameEntity> Games { get; set; }
        public DbSet<GameActorEntity> GameActors { get; set; }
        public DbSet<GameCollectibleStateEntity> GameCollectibleStates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Указываем схему public для PostgreSQL
            modelBuilder.HasDefaultSchema("public");

            // Настройка таблицы Users
            modelBuilder.Entity<UserEntity>()
                .ToTable("users")
                .HasKey(u => u.Id);

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Name)
                .IsUnique();

            // Настройка таблицы Maps
            modelBuilder.Entity<MapEntity>()
                .ToTable("maps")
                .HasKey(m => m.Id);

            modelBuilder.Entity<MapEntity>()
                .Property(m => m.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<MapEntity>()
                .Property(m => m.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<MapEntity>()
                .Property(m => m.RowCount)
                .HasColumnName("row_count")
                .IsRequired();

            modelBuilder.Entity<MapEntity>()
                .Property(m => m.ColCount)
                .HasColumnName("col_count")
                .IsRequired();

            modelBuilder.Entity<MapEntity>()
                .HasIndex(m => m.Name)
                .IsUnique();

            // Настройка таблицы MapCells
            modelBuilder.Entity<MapCellEntity>()
                .ToTable("map_cells")
                .HasKey(mc => mc.Id);

            modelBuilder.Entity<MapCellEntity>()
                .Property(mc => mc.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<MapCellEntity>()
                .Property(mc => mc.MapId)
                .HasColumnName("map_id")
                .IsRequired();

            modelBuilder.Entity<MapCellEntity>()
                .Property(mc => mc.Row)
                .HasColumnName("row")
                .IsRequired();

            modelBuilder.Entity<MapCellEntity>()
                .Property(mc => mc.Col)
                .HasColumnName("col")
                .IsRequired();

            modelBuilder.Entity<MapCellEntity>()
                .Property(mc => mc.CellType)
                .HasColumnName("cell_type")
                .IsRequired();

            modelBuilder.Entity<MapCellEntity>()
                .HasRequired(mc => mc.Map)
                .WithMany(m => m.Cells)
                .HasForeignKey(mc => mc.MapId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<MapCellEntity>()
                .HasIndex(mc => new { mc.MapId, mc.Row, mc.Col })
                .IsUnique();

            // Настройка таблицы Games
            modelBuilder.Entity<GameEntity>()
                .ToTable("games")
                .HasKey(g => g.Id);

            modelBuilder.Entity<GameEntity>()
                .Property(g => g.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<GameEntity>()
                .Property(g => g.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            modelBuilder.Entity<GameEntity>()
                .Property(g => g.MapId)
                .HasColumnName("map_id")
                .IsRequired();

            modelBuilder.Entity<GameEntity>()
                .Property(g => g.Status)
                .HasColumnName("status")
                .IsRequired();

            modelBuilder.Entity<GameEntity>()
                .Property(g => g.Score)
                .HasColumnName("score")
                .IsRequired();

            modelBuilder.Entity<GameEntity>()
                .Property(g => g.Lives)
                .HasColumnName("lives")
                .IsRequired();

            modelBuilder.Entity<GameEntity>()
                .Property(g => g.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            modelBuilder.Entity<GameEntity>()
                .Property(g => g.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired();

            modelBuilder.Entity<GameEntity>()
                .HasRequired(g => g.User)
                .WithMany(u => u.Games)
                .HasForeignKey(g => g.UserId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<GameEntity>()
                .HasRequired(g => g.Map)
                .WithMany(m => m.Games)
                .HasForeignKey(g => g.MapId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<GameEntity>()
                .HasIndex(g => g.UserId);

            // Настройка таблицы GameActors
            modelBuilder.Entity<GameActorEntity>()
                .ToTable("game_actors")
                .HasKey(ga => ga.Id);

            modelBuilder.Entity<GameActorEntity>()
                .Property(ga => ga.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<GameActorEntity>()
                .Property(ga => ga.GameId)
                .HasColumnName("game_id")
                .IsRequired();

            modelBuilder.Entity<GameActorEntity>()
                .Property(ga => ga.ActorType)
                .HasColumnName("actor_type")
                .IsRequired();

            modelBuilder.Entity<GameActorEntity>()
                .Property(ga => ga.Row)
                .HasColumnName("row")
                .IsRequired();

            modelBuilder.Entity<GameActorEntity>()
                .Property(ga => ga.Col)
                .HasColumnName("col")
                .IsRequired();

            modelBuilder.Entity<GameActorEntity>()
                .Property(ga => ga.Direction)
                .HasColumnName("direction")
                .IsRequired();

            modelBuilder.Entity<GameActorEntity>()
                .Property(ga => ga.FrightenedTicksLeft)
                .HasColumnName("frightened_ticks_left")
                .IsRequired();

            modelBuilder.Entity<GameActorEntity>()
                .HasRequired(ga => ga.Game)
                .WithMany(g => g.Actors)
                .HasForeignKey(ga => ga.GameId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<GameActorEntity>()
                .HasIndex(ga => new { ga.GameId, ga.ActorType })
                .IsUnique();

            modelBuilder.Entity<GameActorEntity>()
                .HasIndex(ga => ga.GameId);

            // Настройка таблицы GameCollectibleStates
            modelBuilder.Entity<GameCollectibleStateEntity>()
                .ToTable("game_collectible_state")
                .HasKey(gcs => gcs.Id);

            modelBuilder.Entity<GameCollectibleStateEntity>()
                .Property(gcs => gcs.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<GameCollectibleStateEntity>()
                .Property(gcs => gcs.GameId)
                .HasColumnName("game_id")
                .IsRequired();

            modelBuilder.Entity<GameCollectibleStateEntity>()
                .Property(gcs => gcs.Row)
                .HasColumnName("row")
                .IsRequired();

            modelBuilder.Entity<GameCollectibleStateEntity>()
                .Property(gcs => gcs.Col)
                .HasColumnName("col")
                .IsRequired();

            modelBuilder.Entity<GameCollectibleStateEntity>()
                .Property(gcs => gcs.CollectibleType)
                .HasColumnName("collectible_type")
                .IsRequired();

            modelBuilder.Entity<GameCollectibleStateEntity>()
                .Property(gcs => gcs.IsEaten)
                .HasColumnName("is_eaten")
                .IsRequired();

            modelBuilder.Entity<GameCollectibleStateEntity>()
                .HasRequired(gcs => gcs.Game)
                .WithMany(g => g.CollectibleStates)
                .HasForeignKey(gcs => gcs.GameId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<GameCollectibleStateEntity>()
                .HasIndex(gcs => new { gcs.GameId, gcs.Row, gcs.Col })
                .IsUnique();

            modelBuilder.Entity<GameCollectibleStateEntity>()
                .HasIndex(gcs => gcs.GameId);
        }
    }
}
