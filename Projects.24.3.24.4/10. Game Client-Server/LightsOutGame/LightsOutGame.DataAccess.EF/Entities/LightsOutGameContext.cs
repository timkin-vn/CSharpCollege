using LightsOutGame.DataAccess.EF.Entities;
using System.Data.Entity;

namespace LightsOutGame.DataAccess.EF.Entities
{
    public class LightsOutGameContext : DbContext
    {
        static LightsOutGameContext()
        {
            Database.SetInitializer<LightsOutGameContext>(null);
        }

        public LightsOutGameContext()
            : base("name=EFMain")
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<GameEntity> Games { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("Users", "public");
            modelBuilder.Entity<GameEntity>().ToTable("Games", "public");

            base.OnModelCreating(modelBuilder);
        }
    }
}
