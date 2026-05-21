using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FifteenGames.Common.Definitions;
using FifteenGames.Common.Dtos;
using Npgsql;
using static FifteenGames.Common.Definitions.Constants;

namespace FifteenGames.DataAccess.Repositories
{
    public class GameRepository
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["main"].ConnectionString;
        public GameDto GetNameById(int gameId)
        {
            var selectQueue = @"select g.id, g.userId, g.moveCount, c.row_pos, c.col_pos, c.tile_value from join games g join c on g.id = c.gameId where g.id = @gameId";
            using (var connection = new Npgsql.NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                GameDto result = null;
                using (var command = new Npgsql.NpgsqlCommand(selectQueue, connection)
                {
                    CommandType = System.Data.CommandType.Text
                })
                {
                    command.Parameters.AddWithValue("gameId", gameId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (result == null)
                            {
                                result = new GameDto
                                {
                                    Id = reader.GetInt32(0),
                                    UserId = reader.GetInt32(1),
                                    MoveCount = reader.GetInt32(2)
                                };
                                for (int row = 0; row < RowCount; row++)
                                {
                                    for (int column = 0; column < ColumnCount; column++)
                                    {
                                        result.Cells[row, column] = FreeCellValue;
                                    }
                                }
                            }
                            var rowVal = reader.GetInt32(3);
                            var columnVal = reader.GetInt32(4);
                            var valueVal = reader.GetInt32(5);

                            result.Cells[rowVal, columnVal] = valueVal;
                        }
                    }
                }
                return result;
            }
        }

        public IEnumerable<GameDto> GetGameById(int userId)
        {
            var selectQueue = @"select g.id, g.userId, g.moveCount, c.row_pos, c.col_pos, c.tile_value from join games g join c on g.id = c.gameId where g.userId = @userId order by g.id";
            using (var connection = new Npgsql.NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                List<GameDto> result = new List<GameDto>();
                using (var command = new Npgsql.NpgsqlCommand(selectQueue, connection)
                {
                    CommandType = System.Data.CommandType.Text
                })
                {
                    command.Parameters.AddWithValue("userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        GameDto gameDto = null;
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            gameDto = result.FirstOrDefault(g => g.Id == id);
                            if (gameDto == null)
                            {
                                gameDto = new GameDto
                                {
                                    Id = id,
                                    UserId = reader.GetInt32(1),
                                    MoveCount = reader.GetInt32(2)
                                };
                                for (int row = 0; row < RowCount; row++)
                                {
                                    for (int column = 0; column < ColumnCount; column++)
                                    {
                                        gameDto.Cells[row, column] = FreeCellValue;
                                    }
                                }
                                result.Add(gameDto);
                            }
                        }
                    }
                }
                return result;
            }
        }

        public void Remove(int gameId)
        {
            var deleteGameQueue = @"delete from games where id = @gameId";
            var deleteCellQueue = @"delete from cells where game_id = @gameId";

            using (var connection = new Npgsql.NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using(var command = new Npgsql.NpgsqlCommand(deleteCellQueue, connection)
                {
                    CommandType = System.Data.CommandType.Text
                })
                {
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.ExecuteNonQuery();
                }
                using (var command = new Npgsql.NpgsqlCommand(deleteGameQueue, connection)
                {
                    CommandType = System.Data.CommandType.Text
                })
                {
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.ExecuteNonQuery();
                }
            }
        }
        public int Save(GameDto gameDto)
        {
            if (gameDto.Id == 0)
            {
                return Create(gameDto);
            }
            else
            {
                return Update(gameDto);
            }
        }
        private int Create(GameDto gameDto)
        {
            var insertGameQueue = @"insert into games (userId, moveCount) values (@userId, @moveCount) returning id";
            var insertCellQueue = @"insert into cells (game_id, row_pos, col_pos, tile_value) values (@gameId, @row, @column, @value)";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                int gameId;
                using (var command = new NpgsqlCommand(insertGameQueue, connection)
                {
                    CommandType = System.Data.CommandType.Text
                })
                {
                    command.Parameters.AddWithValue("userId", gameDto.UserId);
                    command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                    var insertResult = command.ExecuteScalar();
                    gameId = (int)insertResult;
                }
                for (int row = 0; row < RowCount; row++)
                {
                    for (int column = 0; column < ColumnCount; column++)
                    {
                        using (var command = new NpgsqlCommand(insertCellQueue, connection)
                        {
                            CommandType = System.Data.CommandType.Text
                        })
                        {
                            if (gameDto.Cells[row, column] == FreeCellValue) continue;
                            command.Parameters.AddWithValue("gameId", gameId);
                            command.Parameters.AddWithValue("row", row);
                            command.Parameters.AddWithValue("column", column);
                            command.Parameters.AddWithValue("value", gameDto.Cells[row, column]);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                return gameId;
            }
        }
        private int Update(GameDto gameDto)
        {
            var updateGameQueue = @"update games set moveCount = @moveCount where id = @gameId";
            var selectQuery = @"select id, row_pos, col_pos, tile_value from cells where game_id = @gameId";
            var updateCellQuery = @"update cells set row = @row, column = @column where id = @cellId";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                int gameId = gameDto.Id;
                using (var command = new NpgsqlCommand(updateGameQueue, connection)
                {
                    CommandType = System.Data.CommandType.Text
                })
                {
                    command.Parameters.AddWithValue("userId", gameId);
                    command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                    command.ExecuteNonQuery();
                }
                var cells = new List<Cell>();
                using (var command = new NpgsqlCommand(selectQuery, connection)
                {
                    CommandType = System.Data.CommandType.Text
                })
                {
                    command.Parameters.AddWithValue("gameId", gameId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cells.Add(new Cell
                            {
                                Id = reader.GetInt32(0),
                                Row = reader.GetInt32(1),
                                Column = reader.GetInt32(2),
                                Value = reader.GetInt32(3)
                            });
                        }
                    }
                }
                for (int row = 0; row < RowCount; row++)
                {
                    for (int column = 0; column < ColumnCount; column++)
                    {
                        var value = gameDto.Cells[row, column];
                        if (value == FreeCellValue) continue;
                        var selectedCell = cells.First(c => c.Value == gameDto.Cells[row, column]);
                        if(selectedCell.Row != row || selectedCell.Column != column)
                        {
                            using (var command = new NpgsqlCommand(updateCellQuery, connection)
                            {
                                CommandType = System.Data.CommandType.Text
                            })
                            {
                                command.Parameters.AddWithValue("row", row);
                                command.Parameters.AddWithValue("column", column);
                                command.Parameters.AddWithValue("cellId", selectedCell.Id);
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
                return gameId;
            }
        }
    }
    class Cell
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int Value { get; set; }
    }
}
