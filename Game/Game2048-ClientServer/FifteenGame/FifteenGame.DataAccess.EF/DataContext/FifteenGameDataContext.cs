using FifteenGame.DataAccess.EF.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.DataContext
{
    public class FifteenGameDataContext : DbContext
    {
        public FifteenGameDataContext() : base("EFMain")
        {
            // Используем CreateDatabaseIfNotExists для стабильности
            Database.SetInitializer(new CreateDatabaseIfNotExists<FifteenGameDataContext>());
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<GameCell> GameCells { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Явно настраиваем User entity
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
                
            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            modelBuilder.Entity<User>()
                .HasMany(u => u.Games)
                .WithRequired(g => g.User)
                .HasForeignKey(g => g.UserId);
            
            // Настраиваем Game entity
            modelBuilder.Entity<Game>()
                .HasKey(g => g.Id);
                
            modelBuilder.Entity<Game>()
                .HasRequired(g => g.User)
                .WithMany(u => u.Games)
                .HasForeignKey(g => g.UserId);
            
            // Настраиваем GameCell entity
            modelBuilder.Entity<GameCell>()
                .HasKey(gc => gc.Id);
                
            modelBuilder.Entity<GameCell>()
                .HasRequired(gc => gc.Game)
                .WithMany(g => g.GameCells)
                .HasForeignKey(gc => gc.GameId);
        }
    }
}
