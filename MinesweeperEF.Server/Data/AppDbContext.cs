using Microsoft.EntityFrameworkCore;

namespace MinesweeperEF.Server.Data;

public sealed class AppDbContext : DbContext {
    public DbSet<User> Users => Set<User>();
    public DbSet<SavedGame> SavedGames => Set<SavedGame>();
    public DbSet<GameCell> GameCells => Set<GameCell>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
