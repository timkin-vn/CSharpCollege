using Game2048.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Game2048.DataAccess.Repositories
{
    public class ScoreRepository
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Game2048DB;Integrated Security=True";

        public void AddScore(string playerName, int score)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Scores (PlayerName, Score, DatePlayed) VALUES (@name, @score, @date)";
                
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", playerName);
                    command.Parameters.AddWithValue("@score", score);
                    command.Parameters.AddWithValue("@date", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<PlayerScore> GetTopScores()
        {
            var list = new List<PlayerScore>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                string sql = "SELECT TOP 10 PlayerName, Score, DatePlayed FROM Scores ORDER BY Score DESC";
                
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PlayerScore
                            {
                                Name = reader["PlayerName"].ToString(),
                                Score = (int)reader["Score"],
                                Date = (DateTime)reader["DatePlayed"]
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
