using Microsoft.EntityFrameworkCore;

namespace MinesweeperEF.Server.Data;

public sealed class AppDbContext : DbContext {
    public DbSet<User> Users => Set<User>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<SavedGame> SavedGames => Set<SavedGame>();
}
