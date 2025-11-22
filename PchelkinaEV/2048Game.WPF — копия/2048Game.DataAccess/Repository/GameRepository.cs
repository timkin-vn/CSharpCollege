using _2048Game.Common.Definitions;
using _2048Game.Common.Dto;
using _2048Game.Common.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game.DataAccess.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
        public GameDto GetByGameId(int gameId)
        {
            var selectQuery = @"
            select 
                g. ""Id"",
                g. ""UserId"",
                c. ""Row"",
                c. ""Column"",
                c. ""Value""
            from
                ""Games"" g
                join ""GameCells"" c on g. ""Id"" = c.""GameId""
            where
                g.""Id"" = @gameId
            ";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                GameDto result = null;
                connection.Open();

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (result == null)
                            {
                                result = new GameDto();
                                result.Id = reader.GetInt32(0);
                                result.UserId = reader.GetInt32(1);

                                for (int row = 0; row < Constants.RowCount; row++)
                                {
                                    for (int column = 0; column < Constants.ColumnCount; column++)
                                    {
                                        result.Cells[row, column] = Constants.FreeCellValue;
                                    }
                                }
                            }

                            var rowVal = reader.GetInt32(2);
                            var columnVal = reader.GetInt32(3);
                            var value = reader.GetInt32(4);

                            result.Cells[rowVal, columnVal] = value;
                        }
                    }
                }
                return result;
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            var selectQuery = @"
            select 
                g. ""Id"",
                g. ""UserId"",
                c. ""Row"",
                c. ""Column"",
                c. ""Value""
            from
                ""Games"" g
                join ""GameCells"" c on g. ""Id"" = c. ""GameId""
            where
                g. ""UserId"" = @userId
            order by
                g. ""Id""
            ";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                List<GameDto> result = new List<GameDto>();
                connection.Open();

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
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
                                gameDto = new GameDto();
                                result.Add(gameDto);
                                gameDto.Id = id;
                                gameDto.UserId = reader.GetInt32(1);

                                for (int row = 0; row < Constants.RowCount; row++)
                                {
                                    for (int column = 0; column < Constants.ColumnCount; column++)
                                    {
                                        gameDto.Cells[row, column] = Constants.FreeCellValue;
                                    }
                                }
                            }
                            var rowVal = reader.GetInt32(2);
                            var columnVal = reader.GetInt32(3);
                            var value = reader.GetInt32(4);

                            gameDto.Cells[rowVal, columnVal] = value;
                        }
                    }
                }
                return result;
            }
        }

        public void Remove(int gameId)
        {
            var cellsRemoveQuery = @"
            delete from ""GameCells""
            where ""GameId"" = @gameId
            ";

            var gameRemoveQuery = @"
            delete from ""Games""
            where ""Id"" = @gameId
            ";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(cellsRemoveQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);

                    var result = command.ExecuteNonQuery();
                }

                using (var command = new NpgsqlCommand(gameRemoveQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);

                    var result = command.ExecuteNonQuery();
                }
            }
        }

        public int Save(GameDto gameDto)
        {
            if (gameDto.Id == 0)
            {
                return Create(gameDto);
            }
            return Update(gameDto);
        }

        private int Update(GameDto gameDto)
        {
            var deleteCellsQuery = @"
            delete from ""GameCells""
            where ""GameId"" = @gameId
            ";

            var insertCellQuery = @"
            insert into ""GameCells"" (""GameId"", ""Row"", ""Column"", ""Value"")
            values (@gameId, @row, @column, @value)
            ";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(deleteCellsQuery, connection))
                {
                    command.Parameters.AddWithValue("gameId", gameDto.Id);
                    command.ExecuteNonQuery();
                }

                for (int r = 0; r < Constants.RowCount; r++)
                {
                    for (int c = 0; c < Constants.ColumnCount; c++)
                    {
                        int value = gameDto.Cells[r, c];
                        if (value == Constants.FreeCellValue)
                        {
                            continue;
                        }

                        using (var command = new NpgsqlCommand(insertCellQuery, connection))
                        {
                            command.Parameters.AddWithValue("gameId", gameDto.Id);
                            command.Parameters.AddWithValue("row", r);
                            command.Parameters.AddWithValue("column", c);
                            command.Parameters.AddWithValue("value", value);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                return gameDto.Id;
            }
        }

        private int Create(GameDto gameDto)
        {
            var insertGameQuery = @"
            insert into ""Games"" (""UserId"")
            values (@userId)
            returning ""Id""
            ";

            var insertCellsQuery = @"
            insert into ""GameCells"" (""GameId"", ""Row"", ""Column"", ""Value"")
            values (@gameId, @row, @column, @value)
            ";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                int gameId;
                connection.Open();

                using (var command = new NpgsqlCommand(insertGameQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("userId", gameDto.UserId);

                    var insertResult = command.ExecuteScalar();
                    gameId = (int)insertResult;
                }
                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        if (gameDto.Cells[row, column] == Constants.FreeCellValue)
                        {
                            continue;
                        }
                        using (var command = new NpgsqlCommand(insertCellsQuery, connection))
                        {
                            command.CommandType = System.Data.CommandType.Text;
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
    }
    class Cell
    {
        public int Id { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public int Value { get; set; }
    }
}