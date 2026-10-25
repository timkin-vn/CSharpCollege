namespace Blackjack.DataAccess.DataContext
{
    public static class DatabaseInitializer
    {
        // Создаёт базу и таблицы, если их ещё нет
        public static void Initialize(string connectionString)
        {
            using (var db = new BlackjackDbContext(connectionString))
            {
                db.Database.EnsureCreated();
            }
        }
    }
}
