namespace Blackjack.Wpf.Infrastructure
{
    public static class AppSettings
    {
        // Отдельная база для этого проекта. Создаётся автоматически при первом запуске.
        // Если у вас другой пароль от PostgreSQL — поменяйте его здесь.
        public const string ConnectionString =
            "Host=localhost;Port=5432;Database=blackjack_wpf;Username=postgres;Password=4196";
    }
}
