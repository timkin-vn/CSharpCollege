using System.Configuration;
using SeaBattle.Common;
using Npgsql;

namespace SeaBattle.DataAccess
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
    ""MoveCount"" integer NOT NULL DEFAULT 0,
    ""Won""       boolean NOT NULL DEFAULT false,
    CONSTRAINT ""FK_Games_Users"" FOREIGN KEY (""UserId"")
        REFERENCES public.""Users"" (""Id"") ON DELETE CASCADE
);
CREATE INDEX IF NOT EXISTS ""IX_Games_UserId"" ON public.""Games"" (""UserId"");";
    }
}
