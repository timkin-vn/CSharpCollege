using Minesweeper.DataAccess.EF.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Minesweeper.DataAccess.EF.Data
{
    public class MinesweeperDbContext : DbContext
    {
        public MinesweeperDbContext(): base("name=MinesweeperDb")
        {
            Database.SetInitializer<MinesweeperDbContext>(null);
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<GameStateEntity> GameStates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<UserEntity>()
                .ToTable("users","public")
                .HasKey(u => u.Id);

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.Username)
                .HasColumnName("username")
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.CreatedAt)
                .HasColumnName("created_at");

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.TotalGamesPlayed)
                .HasColumnName("total_games_played");

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.GamesWon)
                .HasColumnName("games_won");

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.LastPlayedAt)
                .HasColumnName("last_played_at");

            modelBuilder.Entity<GameStateEntity>()
                .ToTable("game_states", "public")
                .HasKey(g => g.Id);

            modelBuilder.Entity<GameStateEntity>()
                .Property(g => g.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<GameStateEntity>()
                .Property(g => g.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            modelBuilder.Entity<GameStateEntity>()
                .Property(g => g.GameData)
                .HasColumnName("game_data")
                .IsRequired()
                .HasColumnType("text");

            modelBuilder.Entity<GameStateEntity>()
                .Property(g => g.IsGameOver)
                .HasColumnName("is_game_over");

            modelBuilder.Entity<GameStateEntity>()
                .Property(g => g.IsGameWon)
                .HasColumnName("is_game_won");

            modelBuilder.Entity<GameStateEntity>()
                .Property(g => g.PlayTime)
                .HasColumnName("play_time");

            modelBuilder.Entity<GameStateEntity>()
                .Property(g => g.CreatedAt)
                .HasColumnName("created_at");

            modelBuilder.Entity<GameStateEntity>()
                .Property(g => g.UpdatedAt)
                .HasColumnName("updated_at");

            modelBuilder.Entity<GameStateEntity>()
                .Property(g => g.FieldSize)
                .HasColumnName("field_size");

            modelBuilder.Entity<GameStateEntity>()
                .Property(g => g.MineCount)
                .HasColumnName("mine_count");

            modelBuilder.Entity<GameStateEntity>()
                .Property(g => g.FlagsPlaced)
                .HasColumnName("flags_placed");

            modelBuilder.Entity<GameStateEntity>()
                .Property(g => g.CellsRevealed)
                .HasColumnName("cells_revealed");

            modelBuilder.Entity<GameStateEntity>()
                .HasRequired(g => g.User)
                .WithMany(u => u.Games)
                .HasForeignKey(g => g.UserId)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}