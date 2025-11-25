using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private const string ConnectionString =
            @"Server=localhost;Port=5432;Database=FifteenGame1Dev23.1.24.1;User Id=postgres;Password=Qwerty123;";

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
                                result.MoveCount = reader.GetInt32(2);

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
    join ""GameCells"" c on g.""Id"" = c.""GameId""
where
    g.""UserId"" = @userId
order by
    g.""Id""
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                List<GameDto> result = new List<GameDto>();

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
                                gameDto.MoveCount = reader.GetInt32(2);

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

        private int Create(GameDto gameDto)
        {
            var insertGameQuery = @"
insert into ""Games"" (""UserId"", ""MoveCount"")
values (@userId, @moveCount)
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
                    command.CommandType = System.Data.CommandType.Text;
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

                        using (var command = new NpgsqlCommand(insertCellQuery, connection))
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
from ""GameCells""
where ""GameId"" = @gameId
";

            var updateCellQuery = @"
update ""GameCells""
set
    ""Row"" = @row,
    ""Column"" = @column
where
    ""Id"" = @cellId
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                int gameId = gameDto.Id;
                connection.Open();

                using (var command = new NpgsqlCommand(updateGameQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);

                    var insertResult = command.ExecuteNonQuery();
                }

                var cells = new List<Cell>();
                using (var command = new NpgsqlCommand(selectCellsQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameDto.Id);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var newCell = new Cell
                            {
                                Id = reader.GetInt32(0),
                                Row = reader.GetInt32(1),
                                Column = reader.GetInt32(2),
                                Value = reader.GetInt32(3),
                            };

                            cells.Add(newCell);
                        }
                    }
                }

                int row = -1;
                int column = -1;
                Cell selectedCell = null;

                for (row = 0; row < Constants.RowCount; row++)
                {
                    for (column = 0; column < Constants.ColumnCount; column++)
                    {
                        if (gameDto.Cells[row, column] == Constants.FreeCellValue)
                        {
                            continue;
                        }

                        selectedCell = cells.First(c => c.Value == gameDto.Cells[row, column]);
                        if (selectedCell.Row != row || selectedCell.Column != column)
                        {
                            goto cellFound;
                        }
                    }
                }

            cellFound:
                using (var command = new NpgsqlCommand(updateCellQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("row", row);
                    command.Parameters.AddWithValue("column", column);
                    command.Parameters.AddWithValue("cellId", selectedCell.Id);

                    var insertResult = command.ExecuteNonQuery();
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
