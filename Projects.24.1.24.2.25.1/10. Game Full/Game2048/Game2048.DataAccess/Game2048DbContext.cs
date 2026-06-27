using Game2048.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Game2048.DataAccess;

public class Game2048DbContext : DbContext
{
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<GameEntity> Games => Set<GameEntity>();

    public Game2048DbContext(DbContextOptions<Game2048DbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(u =>
        {
            u.HasKey(x => x.Id);
            u.HasIndex(x => x.Name).IsUnique();
        });

        modelBuilder.Entity<GameEntity>(g =>
        {
            g.HasKey(x => x.Id);
            g.HasOne(x => x.User)
             .WithMany(x => x.Games)
             .HasForeignKey(x => x.UserId);
        });
    }
}
