using Npgsql;
using System.Configuration;
using System;

namespace FifteenGame.DataAccess
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            var databaseName = builder.Database;

            // Connect to default 'postgres' database to check/create the target database
            builder.Database = "postgres";
            var defaultConnectionString = builder.ToString();

            using (var connection = new NpgsqlConnection(defaultConnectionString))
            {
                connection.Open();

                // Check if database exists
                bool dbExists;
                using (var checkCommand = new NpgsqlCommand($"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'", connection))
                {
                    dbExists = checkCommand.ExecuteScalar() != null;
                }

                if (!dbExists)
                {
                    // Create database
                    using (var createCommand = new NpgsqlCommand($"CREATE DATABASE \"{databaseName}\"", connection))
                    {
                        createCommand.ExecuteNonQuery();
                    }
                }
            }

            // Now connect to the target database and create tables
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var createTablesQuery = @"
CREATE TABLE IF NOT EXISTS ""Users"" (
    ""Id"" SERIAL PRIMARY KEY,
    ""Name"" VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS ""Games"" (
    ""Id"" SERIAL PRIMARY KEY,
    ""UserId"" INT NOT NULL,
    ""MoveCount"" INT NOT NULL,
    FOREIGN KEY (""UserId"") REFERENCES ""Users"" (""Id"") ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS ""Cells"" (
    ""Id"" SERIAL PRIMARY KEY,
    ""GameId"" INT NOT NULL,
    ""Row"" INT NOT NULL,
    ""Column"" INT NOT NULL,
    ""Value"" INT NOT NULL,
    FOREIGN KEY (""GameId"") REFERENCES ""Games"" (""Id"") ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS ""MazeGames"" (
    ""Id"" SERIAL PRIMARY KEY,
    ""UserId"" INT NOT NULL,
    ""SerializedGameManager"" TEXT NOT NULL,
    FOREIGN KEY (""UserId"") REFERENCES ""Users"" (""Id"") ON DELETE CASCADE
);
";

                using (var command = new NpgsqlCommand(createTablesQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
