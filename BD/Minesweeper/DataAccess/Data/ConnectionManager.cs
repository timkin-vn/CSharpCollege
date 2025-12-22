using Npgsql;

namespace Minesweeper.Common.Data
{
    public static class ConnectionManager
    {
        private const string ConnectionString = "Host=localhost;Port=5432;Database=minesweeper;Username=postgres;Password=1234";

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }
    }
}