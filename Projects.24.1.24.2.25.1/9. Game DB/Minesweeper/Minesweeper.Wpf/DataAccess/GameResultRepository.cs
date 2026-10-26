using Npgsql;
using System;
using System.Configuration;

namespace Minesweeper.Wpf.DataAccess
{
    public class GameResultRepository
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["MinesweeperDb"].ConnectionString;

        public void SaveResult(bool won, int moves)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                const string sql = @"INSERT INTO game_results (is_won, moves, finished_at)
                                     VALUES (@won, @moves, CURRENT_TIMESTAMP);";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("won", won);
                    command.Parameters.AddWithValue("moves", moves);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
