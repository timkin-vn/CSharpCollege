using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using gg_web_business.Models;

namespace gg_web_business.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users"); 

                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Username).HasColumnName("username").IsRequired();

                entity.Property(e => e.PasswordHash).HasColumnName("password_hash").IsRequired();
            });
        }
    }
}
