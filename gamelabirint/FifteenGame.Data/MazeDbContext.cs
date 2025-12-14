using System.Data.Entity;
using FifteenGame.Data.Entities;

namespace FifteenGame.Data
{
    // Важно слово public!
    public class MazeDbContext : DbContext
    {
        // Указываем имя строки подключения
        public MazeDbContext() : base("name=MazeDbConnection") { }

        // Эти строчки создадут таблицы
        public DbSet<User> Users { get; set; }
        public DbSet<GameState> GameStates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}