using System;
using System.Configuration;
using System.Windows;
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения победителя: {ex.Message}");
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}");
                return "Ошибка при получении данных";
            }
        }

        public static void SaveGameState(string playerName, string[,] board, string currentPlayer)
        {
            try
            {
                string boardState = ConvertBoardToString(board);

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"INSERT INTO saved_games 
                                (player_name, board_state, current_player) 
                                VALUES (@playerName, @boardState, @currentPlayer)";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@playerName", playerName);
                        cmd.Parameters.AddWithValue("@boardState", boardState);
                        cmd.Parameters.AddWithValue("@currentPlayer", currentPlayer);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

        public static (string[,] board, string player)? LoadGameState(string playerName)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"SELECT board_state, current_player 
                                FROM saved_games 
                                WHERE player_name = @playerName 
                                ORDER BY created_at DESC 
                                LIMIT 1";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@playerName", playerName);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string savedState = reader.GetString(0);
                                string player = reader.GetString(1);
                                return (ParseBoardString(savedState), player);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
            return null;
        }

        private static string ConvertBoardToString(string[,] board)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sb.Append($"{i},{j}:{(string.IsNullOrEmpty(board[i, j]) ? " " : board[i, j])}|");
                }
            }
            return sb.ToString().TrimEnd('|');
        }

        private static string[,] ParseBoardString(string data)
        {
            string[,] board = new string[3, 3];
            var pairs = data.Split('|');

            foreach (var pair in pairs)
            {
                var parts = pair.Split(':');
                if (parts.Length == 2)
                {
                    var coords = parts[0].Split(',');
                    int x = int.Parse(coords[0]);
                    int y = int.Parse(coords[1]);
                    board[x, y] = parts[1] == " " ? "" : parts[1];
                }
            }
            return board;
        }
    }
}