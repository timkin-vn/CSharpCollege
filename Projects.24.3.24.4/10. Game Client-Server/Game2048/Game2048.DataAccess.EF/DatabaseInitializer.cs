using System.Configuration;
using Game2048.Common;
using Npgsql;

namespace Game2048.DataAccess.EF
{
    public static class DatabaseInitializer
    {
        public static void EnsureCreated()
        {
            var cs = ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString;
            string dbName = new NpgsqlConnectionStringBuilder(cs).Database;

            var admin = new NpgsqlConnectionStringBuilder(cs) { Database = "postgres" }.ConnectionString;
            using (var conn = new NpgsqlConnection(admin))
            {
                conn.Open();
                bool exists;
                using (var check = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @n", conn))
                {
                    check.Parameters.AddWithValue("n", dbName);
                    exists = check.ExecuteScalar() != null;
                }
                if (!exists)
                {
                    using (var create = new NpgsqlCommand("CREATE DATABASE \"" + dbName + "\"", conn))
                        create.ExecuteNonQuery();
                }
            }

            using (var conn = new NpgsqlConnection(cs))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(Schema, conn))
                    cmd.ExecuteNonQuery();
            }
        }

        private const string Schema = @"
CREATE TABLE IF NOT EXISTS public.""Users"" (
    ""Id""   serial PRIMARY KEY,
    ""Name"" varchar(200) NOT NULL
);
CREATE TABLE IF NOT EXISTS public.""Games"" (
    ""Id""        serial PRIMARY KEY,
    ""UserId""    integer NOT NULL,
    ""Score""     integer NOT NULL DEFAULT 0,
    ""MoveCount"" integer NOT NULL DEFAULT 0
);
CREATE TABLE IF NOT EXISTS public.""Cells"" (
    ""Id""     serial PRIMARY KEY,
    ""GameId"" integer NOT NULL,
    ""Row""    integer NOT NULL,
    ""Column"" integer NOT NULL,
    ""Value""  integer NOT NULL DEFAULT 0,
    CONSTRAINT ""FK_Cells_Games"" FOREIGN KEY (""GameId"")
        REFERENCES public.""Games"" (""Id"") ON DELETE CASCADE
);
CREATE INDEX IF NOT EXISTS ""IX_Games_UserId"" ON public.""Games"" (""UserId"");
CREATE INDEX IF NOT EXISTS ""IX_Cells_GameId"" ON public.""Cells"" (""GameId"");";
    }
}
