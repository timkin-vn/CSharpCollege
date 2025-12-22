using System;
using System.Text.Json;
using Minesweeper.Business.Core;
using Minesweeper.Common.Data;
using Minesweeper.Common.Dto;
using Npgsql;

namespace Minesweeper.Common.Repositories
{
    public class GameStateRepository
    {
        public GameState GetLastGameState(int userId)
        {
            using (var connection = ConnectionManager.GetConnection())
            {
                connection.Open();

                var sql = @"SELECT id, user_id, game_data, is_game_over, is_game_won, play_time 
                   FROM game_states 
                   WHERE user_id = @userId AND is_game_over = false 
                   ORDER BY id DESC  -- Сортируем по ID (последняя созданная запись)
                   LIMIT 1";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new GameState
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                GameData = reader.GetString(2),
                                IsGameOver = reader.GetBoolean(3),
                                IsGameWon = reader.GetBoolean(4),
                                PlayTime = reader.GetTimeSpan(5)
                            }; 
                        }
                    }
                }

                return null;
            }
        }

        public void SaveGameState(int userId, Field field, TimeSpan playTime, bool isGameOver = false, bool isGameWon = false)
        {
            using (var connection = ConnectionManager.GetConnection())
            {
                connection.Open();

                var gameStateDto = new Models.Field
                {
                    Size = field.Size,
                    Mines = ConvertArray(field.Mines),
                    Revealed = ConvertArray(field.Revealed),
                    Flag = ConvertArray(field.Flag),
                    Num = ConvertToJaggedArray(field.Num),
                    MineCount = field.MineCount,
                    GameOver = field.GameOver,
                    GameWon = field.GameWon
                };

                var gameData = JsonSerializer.Serialize(gameStateDto);

                var checkSql = "SELECT id FROM game_states WHERE user_id = @userId AND is_game_over = false";
                int? existingId = null;

                using (var cmd = new NpgsqlCommand(checkSql, connection))
                {
                    cmd.Parameters.AddWithValue("userId", userId);

                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        existingId = Convert.ToInt32(result);
                    }
                }

                if (existingId.HasValue)
                {
                    var updateSql = @"UPDATE game_states 
                             SET game_data = @gameData,
                                 is_game_over = @isGameOver,
                                 is_game_won = @isGameWon,
                                 play_time = @playTime
                             WHERE id = @id";

                    using (var cmd = new NpgsqlCommand(updateSql, connection))
                    {
                        cmd.Parameters.AddWithValue("id", existingId.Value);
                        cmd.Parameters.AddWithValue("gameData", gameData);
                        cmd.Parameters.AddWithValue("isGameOver", isGameOver);
                        cmd.Parameters.AddWithValue("isGameWon", isGameWon);
                        cmd.Parameters.AddWithValue("playTime", playTime);

                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    var insertSql = @"INSERT INTO game_states 
                             (user_id, game_data, is_game_over, is_game_won, play_time) 
                             VALUES (@userId, @gameData, @isGameOver, @isGameWon, @playTime)";

                    using (var cmd = new NpgsqlCommand(insertSql, connection))
                    {
                        cmd.Parameters.AddWithValue("userId", userId);
                        cmd.Parameters.AddWithValue("gameData", gameData);
                        cmd.Parameters.AddWithValue("isGameOver", isGameOver);
                        cmd.Parameters.AddWithValue("isGameWon", isGameWon);
                        cmd.Parameters.AddWithValue("playTime", playTime);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public Field LoadGameStateFromDto(string gameData)
        {
            var dto = JsonSerializer.Deserialize<Models.Field>(gameData);

            var field = new Field(dto.Size, dto.MineCount);

            for (int i = 0; i < dto.Size; i++)
            {
                for (int j = 0; j < dto.Size; j++)
                {
                    field.Mines[i, j] = dto.Mines[i][j];
                    field.Revealed[i, j] = dto.Revealed[i][j];
                    field.Flag[i, j] = dto.Flag[i][j];
                    field.Num[i, j] = dto.Num[i][j];
                }
            }

            field.GameOver = dto.GameOver;
            field.GameWon = dto.GameWon;

            return field;
        }

        private bool[][] ConvertArray(bool[,] multiArray)
        {
            int size = multiArray.GetLength(0);
            var jaggedArray = new bool[size][];

            for (int i = 0; i < size; i++)
            {
                jaggedArray[i] = new bool[size];
                for (int j = 0; j < size; j++)
                {
                    jaggedArray[i][j] = multiArray[i, j];
                }
            }

            return jaggedArray;
        }

        private int[][] ConvertToJaggedArray(int[,] multiArray)
        {
            int size = multiArray.GetLength(0);
            var jaggedArray = new int[size][];

            for (int i = 0; i < size; i++)
            {
                jaggedArray[i] = new int[size];
                for (int j = 0; j < size; j++)
                {
                    jaggedArray[i][j] = multiArray[i, j];
                }
            }

            return jaggedArray;
        }
    }
}