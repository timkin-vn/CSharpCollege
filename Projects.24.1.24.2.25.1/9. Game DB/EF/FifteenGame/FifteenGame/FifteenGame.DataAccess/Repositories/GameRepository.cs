using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FifteenGame.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        public GameDto GetByGameId(int gameId)
        {
            var selectQuery = @"
select
    g.""Id"",
    g.""UserId"",
    g.""MoveCount"",
    c.""Row"",
    c.""Column"",
    c.""Value""
from
    ""Games"" g
    join ""Cells"" c on g.""Id"" = c.""GameId""
where
    g.""Id"" = @gameId
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                GameDto result = null;

                using (var command = new NpgsqlCommand(selectQuery, connection) { CommandType = System.Data.CommandType.Text })
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
                                    MoveCount = reader.GetInt32(2),
                                };

                                for (int row = 0; row < Constants.RowCount; row++)
                                {
                                    for (int column = 0; column < Constants.ColumnCount; column++)
                                    {
                                        result.Cells[row, column] = Constants.FreeCellValue;
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
    g.""MoveCount"",
    c.""Row"",
    c.""Column"",
    c.""Value""
from
    ""Games"" g
    join ""Cells"" c on g.""Id"" = c.""GameId""
where
    g.""UserId"" = @userId
order by
    g.""Id""
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                List<GameDto> result = new List<GameDto>();

                using (var command = new NpgsqlCommand(selectQuery, connection) { CommandType = System.Data.CommandType.Text })
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
                                    MoveCount = reader.GetInt32(2),
                                };

                                result.Add(gameDto);

                                for (int row = 0; row < Constants.RowCount; row++)
                                {
                                    for (int column = 0; column < Constants.ColumnCount; column++)
                                    {
                                        gameDto.Cells[row, column] = Constants.FreeCellValue;
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
            var deleteCellsQuery = @"
delete from ""Cells""
where ""GameId"" = @gameId
";

            var deleteGameQuey = @"
delete from ""Games""
where ""Id"" = @gameId
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(deleteCellsQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.ExecuteNonQuery();
                }

                using (var command = new NpgsqlCommand(deleteGameQuey, connection) { CommandType = System.Data.CommandType.Text })
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

            return Update(gameDto);
        }

        private int Create(GameDto gameDto)
        {
            var insertGameQuery = @"
insert into ""Games"" (""UserId"", ""MoveCount"")
values (@userId, @moveCount)
returning ""Id""
";

            var insertCellQuery = @"
insert into ""Cells"" (""GameId"", ""Row"", ""Column"", ""Value"")
values (@gameId, @row, @column, @value)
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                int gameId;

                using (var command = new NpgsqlCommand(insertGameQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("userId", gameDto.UserId);
                    command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
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

                        using (var command = new NpgsqlCommand(insertCellQuery, connection) { CommandType = System.Data.CommandType.Text })
                        {
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
    ""MoveCount"" = @moveCount
where
    ""Id"" = @gameId
";

            var selectCellsQuery = @"
select
    ""Id"",
    ""Row"",
    ""Column"",
    ""Value""
from ""Cells""
where ""GameId"" = @gameId
";

            var updateCellQuery = @"
update ""Cells""
set
    ""Row"" = @row,
    ""Column"" = @column
where
    ""Id"" = @cellId
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                int gameId = gameDto.Id;

                using (var command = new NpgsqlCommand(updateGameQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                    command.ExecuteNonQuery();
                }

                var cells = new List<Cell>();
                using (var command = new NpgsqlCommand(selectCellsQuery, connection) { CommandType = System.Data.CommandType.Text })
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
                                Value = reader.GetInt32(3),
                            });
                        }
                    }
                }

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        var value = gameDto.Cells[row, column];
                        if (value == Constants.FreeCellValue)
                        {
                            continue;
                        }

                        var selectedCell = cells.First(c => c.Value == value);
                        if (selectedCell.Row != row || selectedCell.Column != column)
                        {
                            using (var command = new NpgsqlCommand(updateCellQuery, connection) { CommandType = System.Data.CommandType.Text })
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

        class Cell
        {
            public int Id { get; set; }

            public int Row { get; set; }

            public int Column { get; set; }

            public int Value { get; set; }
        }
    }
}
