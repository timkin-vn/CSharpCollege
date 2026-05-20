using FifteenGame.Data.Entities;
using System.Data.Entity;

namespace FifteenGame.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext() : base("name=FifteenGameConnection")
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}