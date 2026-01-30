using Microsoft.VisualBasic;
using Nonogram.Common.Definitions;
using Nonogram.Common.Dtos;
using Nonogram.Common.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Nonogram.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private const string ConnectionString =
            @"Server=localhost;Port=5432;Database=NonogramDb;User Id=postgres;Password=postgresql;";

        public GameDto GetByGameId(int gameId)
        {
            var selectQuery = @"
select
    g.""Id"",
    g.""UserId"",
    g.""MistakesCount"",
    c.""Row"",
    c.""Column"",
    c.""Value""
from 
    ""Games"" g
    join ""GameCells"" c on g.""Id"" = c.""GameId""
where
    g.""Id"" = @gameId
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                GameDto result = null;

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = CommandType.Text;
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
                                result.MistakesCount = reader.GetInt32(2);

                                for (int row = 0; row < Common.Definitions.Constants.RowCount; row++)
                                {
                                    for (int column = 0; column < Common.Definitions.Constants.ColumnCount; column++)
                                    {
                                        result.Cells[row, column] = Common.Definitions.Constants.EmptyCell;
                                    }
                                }
                            }

                            var rowVal = reader.GetInt32(3);
                            var columnVal = reader.GetInt32(4);
                            var value = reader.GetInt32(5);

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
    g.""Id"",
    g.""UserId"",
    g.""MistakesCount"",
    c.""Row"",
    c.""Column"",
    c.""Value""
from 
    ""Games"" g
    join ""GameCells"" c on g.""Id"" = c.""GameId""
where
    g.""UserId"" = @userId
order by
    g.""Id"" desc
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                List<GameDto> result = new List<GameDto>();

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = CommandType.Text;
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
                                gameDto.MistakesCount = reader.GetInt32(2);

                                for (int row = 0; row < Common.Definitions.Constants.RowCount; row++)
                                {
                                    for (int column = 0; column < Common.Definitions.Constants.ColumnCount; column++)
                                    {
                                        gameDto.Cells[row, column] = Common.Definitions.Constants.EmptyCell;
                                    }
                                }
                            }

                            var rowVal = reader.GetInt32(3);
                            var columnVal = reader.GetInt32(4);
                            var value = reader.GetInt32(5);

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
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.ExecuteNonQuery();
                }

                using (var command = new NpgsqlCommand(gameRemoveQuery, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int Save(GameDto gameDto)
        {
            Console.WriteLine($"Saving game: Id={gameDto.Id}, UserId={gameDto.UserId}, Mistakes={gameDto.MistakesCount}");

            if (gameDto.Id == 0)
            {
                Console.WriteLine("Creating new game");
                return Create(gameDto);
            }

            Console.WriteLine("Updating existing game");
            return Update(gameDto);
        }

        private int Create(GameDto gameDto)
        {
            var insertGameQuery = @"
insert into ""Games"" (""UserId"", ""MistakesCount"")
values (@userId, @mistakesCount)
returning ""Id""
";

            var insertCellQuery = @"
insert into ""GameCells"" (""GameId"", ""Row"", ""Column"", ""Value"")
values (@gameId, @row, @column, @value)
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                int gameId;
                connection.Open();

                using (var command = new NpgsqlCommand(insertGameQuery, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("userId", gameDto.UserId);
                    command.Parameters.AddWithValue("mistakesCount", gameDto.MistakesCount);

                    var insertResult = command.ExecuteScalar();
                    gameId = (int)insertResult;
                }

                for (int row = 0; row < Common.Definitions.Constants.RowCount; row++)
                {
                    for (int column = 0; column < Common.Definitions.Constants.ColumnCount; column++)
                    {
                        using (var command = new NpgsqlCommand(insertCellQuery, connection))
                        {
                            command.CommandType = CommandType.Text;
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
            var updateGameQuery = @"
update ""Games""
set
    ""MistakesCount"" = @mistakesCount
where
    ""Id"" = @gameId
";

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
                int gameId = gameDto.Id;
                connection.Open();

                using (var command = new NpgsqlCommand(updateGameQuery, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.Parameters.AddWithValue("mistakesCount", gameDto.MistakesCount);
                    command.ExecuteNonQuery();
                }

                using (var command = new NpgsqlCommand(deleteCellsQuery, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.ExecuteNonQuery();
                }

                for (int row = 0; row < Common.Definitions.Constants.RowCount; row++)
                {
                    for (int column = 0; column < Common.Definitions.Constants.ColumnCount; column++)
                    {
                        using (var command = new NpgsqlCommand(insertCellQuery, connection))
                        {
                            command.CommandType = CommandType.Text;
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
}