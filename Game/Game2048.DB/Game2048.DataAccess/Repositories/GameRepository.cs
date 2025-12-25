using Game2048.Common.Dtos;
using Game2048.Common.Repositories;
using Game2048.Common.Models;
using Game2048.Common.Services;
using Npgsql;
using System;

namespace Game2048.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private const string ConnectionString =
            @"Server=localhost;Port=5432;Database=Game2048DB;User Id=postgres;Password=postgres;";

        private readonly GameService _gameService = new GameService();

        public GameDto GetByGameId(int gameId)
        {
            var selectQuery = @"
SELECT 
    ""Id"", 
    ""UserId"", 
    ""Score"", 
    ""IsGameOver"", 
    ""IsWon"", 
    ""BoardState""
FROM ""Games"" 
WHERE ""Id"" = @gameId";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                
                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new GameDto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                Score = reader.GetInt32(reader.GetOrdinal("Score")),
                                IsGameOver = reader.GetBoolean(reader.GetOrdinal("IsGameOver")),
                                IsWon = reader.GetBoolean(reader.GetOrdinal("IsWon")),
                                BoardState = reader.GetString(reader.GetOrdinal("BoardState"))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public int CreateGame(GameModel game)
        {
            var insertQuery = @"
INSERT INTO ""Games"" (""UserId"", ""Score"", ""IsGameOver"", ""IsWon"", ""BoardState"")
VALUES (@userId, @score, @isGameOver, @isWon, @boardState)
RETURNING ""Id""";

            var boardState = _gameService.SerializeBoard(game);

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                
                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("userId", 1); // Default user
                    command.Parameters.AddWithValue("score", game.Score);
                    command.Parameters.AddWithValue("isGameOver", game.IsGameOver);
                    command.Parameters.AddWithValue("isWon", game.IsWon);
                    command.Parameters.AddWithValue("boardState", boardState);

                    var gameId = command.ExecuteScalar();
                    return Convert.ToInt32(gameId);
                }
            }
        }

        public void UpdateGame(int gameId, GameModel game)
        {
            var updateQuery = @"
UPDATE ""Games"" 
SET ""Score"" = @score, 
    ""IsGameOver"" = @isGameOver, 
    ""IsWon"" = @isWon, 
    ""BoardState"" = @boardState
WHERE ""Id"" = @gameId";

            var boardState = _gameService.SerializeBoard(game);

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                
                using (var command = new NpgsqlCommand(updateQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.Parameters.AddWithValue("score", game.Score);
                    command.Parameters.AddWithValue("isGameOver", game.IsGameOver);
                    command.Parameters.AddWithValue("isWon", game.IsWon);
                    command.Parameters.AddWithValue("boardState", boardState);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteGame(int gameId)
        {
            var deleteQuery = @"DELETE FROM ""Games"" WHERE ""Id"" = @gameId";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                
                using (var command = new NpgsqlCommand(deleteQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public GameDto GetLatestGame(int userId)
        {
            var selectQuery = @"
SELECT 
    ""Id"", 
    ""UserId"", 
    ""Score"", 
    ""IsGameOver"", 
    ""IsWon"", 
    ""BoardState""
FROM ""Games"" 
WHERE ""UserId"" = @userId
ORDER BY ""Id"" DESC
LIMIT 1";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                
                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new GameDto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                Score = reader.GetInt32(reader.GetOrdinal("Score")),
                                IsGameOver = reader.GetBoolean(reader.GetOrdinal("IsGameOver")),
                                IsWon = reader.GetBoolean(reader.GetOrdinal("IsWon")),
                                BoardState = reader.GetString(reader.GetOrdinal("BoardState"))
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
