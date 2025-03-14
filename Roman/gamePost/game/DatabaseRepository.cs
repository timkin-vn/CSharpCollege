using System;
using System.Configuration;
using Npgsql;

namespace game
{
    internal class DatabaseRepository
    {

        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;


        public static void SaveWinner(string winner)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO game_results (winner, timestamp) VALUES (@winner, @timestamp)";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@winner", winner);
                        cmd.Parameters.AddWithValue("@timestamp", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

            }
        }


        public static string GetLastWinner()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT winner FROM game_results ORDER BY timestamp DESC LIMIT 1";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        var result = cmd.ExecuteScalar();
                        return result != null ? result.ToString() : "Нет данных";
                    }
                }
            }
            catch (Exception)
            {
                return "Ошибка при получении данных";
            }
        }
    }
}
