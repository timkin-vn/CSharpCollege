using System;
using Minesweeper.Common.Data;
using Minesweeper.Common.Dto;
using Npgsql;

namespace Minesweeper.Common.Repositories
{
    public class UserRepository
    {
        public User GetOrCreateUser(string username)
        {
            using (var connection = ConnectionManager.GetConnection())
            {
                connection.Open();

                var checkSql = "SELECT id, username, created_at, total_games_played, games_won FROM users WHERE username = @username";
                using (var cmd = new NpgsqlCommand(checkSql, connection))
                {
                    cmd.Parameters.AddWithValue("username", username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                CreatedAt = reader.GetDateTime(2),
                                TotalGamesPlayed = reader.GetInt32(3),
                                GamesWon = reader.GetInt32(4)
                            };
                        }
                    }
                }

                var insertSql = @"INSERT INTO users (username, created_at, total_games_played, games_won) 
                                  VALUES (@username, @created_at, 0, 0) 
                                  RETURNING id";

                using (var cmd = new NpgsqlCommand(insertSql, connection))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("created_at", DateTime.UtcNow);

                    var newId = (int)cmd.ExecuteScalar();

                    return new User
                    {
                        Id = newId,
                        Username = username,
                        CreatedAt = DateTime.UtcNow,
                        TotalGamesPlayed = 0,
                        GamesWon = 0
                    };
                }
            }
        }

        public void UpdateUserStats(int userId, bool gameWon)
        {
            using (var connection = ConnectionManager.GetConnection())
            {
                connection.Open();

                var sql = @"UPDATE users 
                           SET total_games_played = total_games_played + 1, 
                               games_won = games_won + @wonIncrement
                           WHERE id = @userId";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("userId", userId);
                    cmd.Parameters.AddWithValue("wonIncrement", gameWon ? 1 : 0);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}