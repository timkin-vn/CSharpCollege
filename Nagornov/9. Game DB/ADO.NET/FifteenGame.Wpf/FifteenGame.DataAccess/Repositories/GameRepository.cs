using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly string _connectionString;
        private static List<GameDto> _inMemoryGames = new List<GameDto>();
        private static int _nextInMemoryId = 1;

        public GameRepository()
        {
            
            _connectionString = "Host=localhost;Port=5432;Database=fifteengame;Username=postgres;Password=sOQA1337";
            Console.WriteLine($"🔗 GameRepository: Используем строку подключения: {_connectionString}");
        }

        public int Save(GameDto gameDto)
        {
            Console.WriteLine($"=== 💾 СОХРАНЕНИЕ ИГРЫ В БД ===");
            Console.WriteLine($"Время: {DateTime.Now}");
            Console.WriteLine($"ID игры: {gameDto.Id}");
            Console.WriteLine($"ID пользователя: {gameDto.UserId}");
            Console.WriteLine($"Ходов: {gameDto.MoveCount}");
            Console.WriteLine($"Мин: {gameDto.MinesCount}");
            Console.WriteLine($"Флагов: {gameDto.FlagsCount}");
            Console.WriteLine($"Статус: {gameDto.GameState}");

            try
            {
                
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    Console.WriteLine($"🔌 Подключаемся к PostgreSQL...");
                    connection.Open();
                    Console.WriteLine($"✅ Подключение к PostgreSQL успешно!");

                    if (gameDto.Id == 0)
                    {
                       
                        Console.WriteLine($"📝 Вставляем новую игру в БД...");
                        var query = @"
                            INSERT INTO games 
                            (user_id, cells, revealed, mines, move_count, mines_count, flags_count, game_state) 
                            VALUES (@userId, @cells, @revealed, @mines, @moveCount, @minesCount, @flagsCount, @gameState)
                            RETURNING id";

                        using (var cmd = new NpgsqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("userId", gameDto.UserId);
                            cmd.Parameters.AddWithValue("cells", ConvertCellsToString(gameDto.Cells));
                            cmd.Parameters.AddWithValue("revealed", ConvertBoolArrayToString(gameDto.Revealed));
                            cmd.Parameters.AddWithValue("mines", ConvertBoolArrayToString(gameDto.Mines));
                            cmd.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                            cmd.Parameters.AddWithValue("minesCount", gameDto.MinesCount);
                            cmd.Parameters.AddWithValue("flagsCount", gameDto.FlagsCount);
                            cmd.Parameters.AddWithValue("gameState", gameDto.GameState);

                            var newId = Convert.ToInt32(cmd.ExecuteScalar());
                            Console.WriteLine($"✅ Игра успешно сохранена в PostgreSQL! Новый ID: {newId}");
                            return newId;
                        }
                    }
                    else
                    {
                        
                        Console.WriteLine($"✏️ Обновляем существующую игру ID={gameDto.Id}...");
                        var query = @"
                            UPDATE games 
                            SET cells = @cells, revealed = @revealed, mines = @mines, 
                                move_count = @moveCount, mines_count = @minesCount, 
                                flags_count = @flagsCount, game_state = @gameState
                            WHERE id = @id";

                        using (var cmd = new NpgsqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("id", gameDto.Id);
                            cmd.Parameters.AddWithValue("cells", ConvertCellsToString(gameDto.Cells));
                            cmd.Parameters.AddWithValue("revealed", ConvertBoolArrayToString(gameDto.Revealed));
                            cmd.Parameters.AddWithValue("mines", ConvertBoolArrayToString(gameDto.Mines));
                            cmd.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                            cmd.Parameters.AddWithValue("minesCount", gameDto.MinesCount);
                            cmd.Parameters.AddWithValue("flagsCount", gameDto.FlagsCount);
                            cmd.Parameters.AddWithValue("gameState", gameDto.GameState);

                            cmd.ExecuteNonQuery();
                            Console.WriteLine($"✅ Игра ID={gameDto.Id} обновлена в PostgreSQL!");
                            return gameDto.Id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ОШИБКА сохранения в PostgreSQL: {ex.Message}");
                Console.WriteLine($"⚠️ Работаем в режиме памяти...");

                
                if (gameDto.Id == 0)
                {
                    gameDto.Id = _nextInMemoryId++;
                    _inMemoryGames.Add(gameDto);
                    Console.WriteLine($"💾 Игра сохранена в памяти. ID: {gameDto.Id}");
                }
                else
                {
                    var existing = _inMemoryGames.FirstOrDefault(g => g.Id == gameDto.Id);
                    if (existing != null)
                    {
                        _inMemoryGames.Remove(existing);
                        _inMemoryGames.Add(gameDto);
                        Console.WriteLine($"💾 Игра обновлена в памяти. ID: {gameDto.Id}");
                    }
                }
                return gameDto.Id;
            }
        }

        public GameDto GetByGameId(int gameId)
        {
            try
            {
                
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT * FROM games WHERE id = @id";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("id", gameId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine($"✅ Игра ID={gameId} загружена из PostgreSQL");
                                return MapToDto(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка загрузки из PostgreSQL: {ex.Message}. Ищем в памяти...");
            }

            
            return _inMemoryGames.FirstOrDefault(g => g.Id == gameId);
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            var result = new List<GameDto>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT * FROM games WHERE user_id = @userId AND game_state = 'Playing'";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("userId", userId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(MapToDto(reader));
                            }
                        }
                    }
                }
                Console.WriteLine($"✅ Загружено {result.Count} активных игр для пользователя ID={userId} из PostgreSQL");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка загрузки из PostgreSQL: {ex.Message}. Используем память...");
                result = _inMemoryGames.Where(g => g.UserId == userId && g.GameState == "Playing").ToList();
            }

            return result;
        }

        public IEnumerable<GameDto> GetFinishedGamesByUserId(int userId)
        {
            var result = new List<GameDto>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT * FROM games WHERE user_id = @userId AND game_state IN ('Won', 'GameOver')";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("userId", userId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(MapToDto(reader));
                            }
                        }
                    }
                }
            }
            catch
            {
                result = _inMemoryGames.Where(g => g.UserId == userId && (g.GameState == "Won" || g.GameState == "GameOver")).ToList();
            }

            return result;
        }

        public void Remove(int gameId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "DELETE FROM games WHERE id = @id";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("id", gameId);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"🗑️ Игра ID={gameId} удалена из PostgreSQL");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка удаления из PostgreSQL: {ex.Message}. Удаляем из памяти...");
                var game = _inMemoryGames.FirstOrDefault(g => g.Id == gameId);
                if (game != null)
                {
                    _inMemoryGames.Remove(game);
                }
            }
        }

        private GameDto MapToDto(NpgsqlDataReader reader)
        {
            var dto = new GameDto
            {
                Id = Convert.ToInt32(reader["id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                MoveCount = Convert.ToInt32(reader["move_count"]),
                MinesCount = Convert.ToInt32(reader["mines_count"]),
                FlagsCount = Convert.ToInt32(reader["flags_count"]),
                GameState = reader["game_state"].ToString()
            };

            var cellsStr = reader["cells"].ToString();
            var revealedStr = reader["revealed"].ToString();
            var minesStr = reader["mines"].ToString();

            ParseStringToArray(cellsStr, dto.Cells);
            ParseStringToBoolArray(revealedStr, dto.Revealed);
            ParseStringToBoolArray(minesStr, dto.Mines);

            return dto;
        }

        private string ConvertCellsToString(int[,] cells)
        {
            var result = new System.Text.StringBuilder();
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    result.Append(cells[i, j]);
                    if (j < cells.GetLength(1) - 1) result.Append(",");
                }
                if (i < cells.GetLength(0) - 1) result.Append(";");
            }
            return result.ToString();
        }

        private string ConvertBoolArrayToString(bool[,] array)
        {
            var result = new System.Text.StringBuilder();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    result.Append(array[i, j] ? "1" : "0");
                    if (j < array.GetLength(1) - 1) result.Append(",");
                }
                if (i < array.GetLength(0) - 1) result.Append(";");
            }
            return result.ToString();
        }

        private void ParseStringToArray(string str, int[,] array)
        {
            var rows = str.Split(';');
            for (int i = 0; i < rows.Length; i++)
            {
                var cols = rows[i].Split(',');
                for (int j = 0; j < cols.Length; j++)
                {
                    array[i, j] = Convert.ToInt32(cols[j]);
                }
            }
        }

        private void ParseStringToBoolArray(string str, bool[,] array)
        {
            var rows = str.Split(';');
            for (int i = 0; i < rows.Length; i++)
            {
                var cols = rows[i].Split(',');
                for (int j = 0; j < cols.Length; j++)
                {
                    array[i, j] = cols[j] == "1";
                }
            }
        }
    }
}