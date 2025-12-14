using System.Data.Entity;
using PacmanGame.DataAccess.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PacmanGame.DataAccess
{
    public class PacmanDbContext : DbContext
    {
        
        // EF автоматически ищет строку подключения по имени "PacmanConnection".
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
                .HasRequired(gs => gs.GameUser)              // GameStateEntity требует GameUserEntity
                .WithMany(gu => gu.GameStates)               // У GameUserEntity много GameStateEntity
                .HasForeignKey(gs => gs.GameUserId)          // Внешний ключ: GameUserId
                .WillCascadeOnDelete(true);                  // При удалении пользователя, его сохранения удаляются

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