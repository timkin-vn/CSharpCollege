using Blackjack.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blackjack.DataAccess.DataContext
{
    public class BlackjackDbContext : DbContext
    {
        private readonly string _connectionString;

        public BlackjackDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(u => u.Name).IsRequired().HasMaxLength(50);
                entity.HasIndex(u => u.Name).IsUnique();
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Games");
                entity.Property(g => g.Message).IsRequired();
                entity.Property(g => g.PlayerCards).IsRequired();
                entity.Property(g => g.DealerCards).IsRequired();
                entity.Property(g => g.DeckCards).IsRequired();
                entity.HasIndex(g => new { g.UserId, g.Status });
                entity.HasOne(g => g.User)
                    .WithMany(u => u.Games)
                    .HasForeignKey(g => g.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
