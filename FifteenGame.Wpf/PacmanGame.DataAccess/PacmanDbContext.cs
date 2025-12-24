using System.Data.Entity;
using PacmanGame.DataAccess.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PacmanGame.DataAccess
{
    public class PacmanDbContext : DbContext
    {
        
        
        public PacmanDbContext() : base("PacmanConnection")
        {
            // Отключение инициализатора БД, так как вы используете миграции Code First
            Database.SetInitializer<PacmanDbContext>(null);
        }

        // DbSets для ваших таблиц
        public DbSet<GameUserEntity> GameUsers { get; set; }
        public DbSet<GameStateEntity> GameStates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            

            base.OnModelCreating(modelBuilder);

            // КОНФИГУРАЦИЯ СВЯЗЕЙ 
            modelBuilder.Entity<GameStateEntity>()
                .HasRequired(gs => gs.GameUser)              
                .WithMany(gu => gu.GameStates)               
                .HasForeignKey(gs => gs.GameUserId)          
                .WillCascadeOnDelete(true);                 

            // КОНФИГУРАЦИЯ ПОЛЬЗОВАТЕЛЯ 
            modelBuilder.Entity<GameUserEntity>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            // Уникальный индекс для логина (важно для регистрации/входа)
            modelBuilder.Entity<GameUserEntity>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}