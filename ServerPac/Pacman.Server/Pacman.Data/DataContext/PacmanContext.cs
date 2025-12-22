using Pacman.Data.Entities;
using System.Data.Entity;

namespace Pacman.Data.DataContext
{
    public class PacmanContext : DbContext
    {
        public PacmanContext() : base("name=PacmanDbConnection") { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}