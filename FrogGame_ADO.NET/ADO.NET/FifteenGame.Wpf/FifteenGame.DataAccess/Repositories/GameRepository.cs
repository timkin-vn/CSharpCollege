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
        private const string ConnectionString =
    @"Server=localhost;Port=5432;Database=FrogGameDB;User Id=myPostgres;Password=martNadia;";

        public GameDto GetByGameId(int gameId)
        {
            var selectQuery = @"
select
    g.""Id"",
    g.""UserId"",
    g.""MoveCount"",
    g.""FrogRow"",
    g.""FrogColumn"",
    g.""HomeRow"",
    g.""HomeColumn"",
    g.""IsGameOver"",
    g.""IsWin"",
    g.""SelectedLilyPadRow"",
    g.""SelectedLilyPadColumn"",
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
                                result.FrogRow = reader.GetInt32(3);
                                result.FrogColumn = reader.GetInt32(4);
                                result.HomeRow = reader.GetInt32(5);
                                result.HomeColumn = reader.GetInt32(6);
                                result.IsGameOver = reader.GetBoolean(7);
                                result.IsWin = reader.GetBoolean(8);
                                result.SelectedLilyPadRow = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9);
                                result.SelectedLilyPadColumn = reader.IsDBNull(10) ? (int?)null : reader.GetInt32(10);

                                for (int row = 0; row < 8; row++)
                                {
                                    for (int column = 0; column < 8; column++)
                                    {
                                        result.Cells[row, column] = 0;
                                    }
                                }
                            }

                            var rowVal = reader.GetInt32(11);
                            var columnVal = reader.GetInt32(12);
                            var value = reader.GetInt32(13);

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
    g.""FrogRow"",
    g.""FrogColumn"",
    g.""HomeRow"",
    g.""HomeColumn"",
    g.""IsGameOver"",
    g.""IsWin"",
    g.""SelectedLilyPadRow"",
    g.""SelectedLilyPadColumn"",
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
                                gameDto.FrogRow = reader.GetInt32(3);
                                gameDto.FrogColumn = reader.GetInt32(4);
                                gameDto.HomeRow = reader.GetInt32(5);
                                gameDto.HomeColumn = reader.GetInt32(6);
                                gameDto.IsGameOver = reader.GetBoolean(7);
                                gameDto.IsWin = reader.GetBoolean(8);
                                command.Parameters.AddWithValue("selectedLilyPadRow", gameDto.SelectedLilyPadRow.HasValue ? (object)gameDto.SelectedLilyPadRow.Value : DBNull.Value);
                                command.Parameters.AddWithValue("selectedLilyPadColumn", gameDto.SelectedLilyPadColumn.HasValue ? (object)gameDto.SelectedLilyPadColumn.Value : DBNull.Value);

                                for (int row = 0; row < 8; row++)
                                {
                                    for (int column = 0; column < 8; column++)
                                    {
                                        gameDto.Cells[row, column] = 0;
                                    }
                                }
                            }

                            var rowVal = reader.GetInt32(11);
                            var columnVal = reader.GetInt32(12);
                            var value = reader.GetInt32(13);

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
                    command.ExecuteNonQuery();
                }

                using (var command = new NpgsqlCommand(gameRemoveQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
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
insert into ""Games"" (""UserId"", ""MoveCount"", ""FrogRow"", ""FrogColumn"", ""HomeRow"", ""HomeColumn"", ""IsGameOver"", ""IsWin"", ""SelectedLilyPadRow"", ""SelectedLilyPadColumn"")
values (@userId, @moveCount, @frogRow, @frogColumn, @homeRow, @homeColumn, @isGameOver, @isWin, @selectedLilyPadRow, @selectedLilyPadColumn)
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
                    command.Parameters.AddWithValue("frogRow", gameDto.FrogRow);
                    command.Parameters.AddWithValue("frogColumn", gameDto.FrogColumn);
                    command.Parameters.AddWithValue("homeRow", gameDto.HomeRow);
                    command.Parameters.AddWithValue("homeColumn", gameDto.HomeColumn);
                    command.Parameters.AddWithValue("isGameOver", gameDto.IsGameOver);
                    command.Parameters.AddWithValue("isWin", gameDto.IsWin);
                    command.Parameters.AddWithValue("selectedLilyPadRow", (object)gameDto.SelectedLilyPadRow ?? DBNull.Value);
                    command.Parameters.AddWithValue("selectedLilyPadColumn", (object)gameDto.SelectedLilyPadColumn ?? DBNull.Value);

                    var insertResult = command.ExecuteScalar();
                    gameId = (int)insertResult;
                }

                for (int row = 0; row < 8; row++)
                {
                    for (int column = 0; column < 8; column++)
                    {
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
    ""MoveCount"" = @moveCount,
    ""FrogRow"" = @frogRow,
    ""FrogColumn"" = @frogColumn,
    ""HomeRow"" = @homeRow,
    ""HomeColumn"" = @homeColumn,
    ""IsGameOver"" = @isGameOver,
    ""IsWin"" = @isWin,
    ""SelectedLilyPadRow"" = @selectedLilyPadRow,
    ""SelectedLilyPadColumn"" = @selectedLilyPadColumn
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
                connection.Open();

                using (var command = new NpgsqlCommand(updateGameQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameDto.Id);
                    command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                    command.Parameters.AddWithValue("frogRow", gameDto.FrogRow);
                    command.Parameters.AddWithValue("frogColumn", gameDto.FrogColumn);
                    command.Parameters.AddWithValue("homeRow", gameDto.HomeRow);
                    command.Parameters.AddWithValue("homeColumn", gameDto.HomeColumn);
                    command.Parameters.AddWithValue("isGameOver", gameDto.IsGameOver);
                    command.Parameters.AddWithValue("isWin", gameDto.IsWin);
                    command.Parameters.AddWithValue("selectedLilyPadRow", (object)gameDto.SelectedLilyPadRow ?? DBNull.Value);
                    command.Parameters.AddWithValue("selectedLilyPadColumn", (object)gameDto.SelectedLilyPadColumn ?? DBNull.Value);

                    command.ExecuteNonQuery();
                }
                using (var command = new NpgsqlCommand(deleteCellsQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameDto.Id);
                    command.ExecuteNonQuery();
                }

                for (int row = 0; row < 8; row++)
                {
                    for (int column = 0; column < 8; column++)
                    {
                        using (var command = new NpgsqlCommand(insertCellQuery, connection))
                        {
                            command.CommandType = System.Data.CommandType.Text;
                            command.Parameters.AddWithValue("gameId", gameDto.Id);
                            command.Parameters.AddWithValue("row", row);
                            command.Parameters.AddWithValue("column", column);
                            command.Parameters.AddWithValue("value", gameDto.Cells[row, column]);

                            command.ExecuteNonQuery();
                        }
                    }
                }

                return gameDto.Id;
            }
        }
    }
}