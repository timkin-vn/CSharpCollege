using FifteenGame.Business.Models;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;

namespace FifteenGame.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private const string ConnectionString =
            @"Server=localhost;Port=5432;Database=FifteenGame1Dev23.1.24.1;User Id=postgres;Password=greyratto1346COOL;";

        public GameDto GetByGameId(int gameId)
        {
            var selectQuery = @"
select
    g.""Id"",
    g.""UserId"",
    g.""MatchesCount"",
    g.""IsFinished"",
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
                                result.MatchesCount = reader.GetInt32(2);
                                result.IsFinished = reader.GetBoolean(3);


                                for (int row = 0; row < GameModel.RowCount; row++)
                                {
                                    for (int column = 0; column < GameModel.ColumnCount; column++)
                                    {
                                        result.Cells[row, column] = 0;
                                    }
                                }

                            }

                            var rowVal = reader.GetInt32(4);
                            var columnVal = reader.GetInt32(5);
                            var value = reader.GetInt32(6);

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
    g.""MatchesCount"",
    g.""IsFinished"",
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
                                gameDto.MatchesCount = reader.GetInt32(2);
                                gameDto.IsFinished = reader.GetBoolean(3);


                                for (int row = 0; row < GameModel.RowCount; row++)
                                {
                                    for (int column = 0; column < GameModel.ColumnCount; column++)
                                    {
                                        gameDto.Cells[row, column] = 0;
                                    }
                                }
                            }

                            var rowVal = reader.GetInt32(4);
                            var columnVal = reader.GetInt32(5);
                            var value = reader.GetInt32(6);

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
INSERT INTO ""Games"" (""UserId"", ""MatchesCount"", ""IsFinished"")
VALUES (@userId, @matchesCount, @isFinished)
RETURNING ""Id""
";

            var insertCellQuery = @"
INSERT INTO ""GameCells"" (""GameId"", ""Row"", ""Column"", ""Value"")
VALUES (@gameId, @row, @column, @value)
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                int gameId;
                connection.Open();

                using (var command = new NpgsqlCommand(insertGameQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("userId", gameDto.UserId);
                    command.Parameters.AddWithValue("matchesCount", gameDto.MatchesCount);
                    command.Parameters.AddWithValue("isFinished", gameDto.IsFinished);

                    var insertResult = command.ExecuteScalar();
                    gameId = (int)insertResult;
                }

                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    for (int column = 0; column < GameModel.ColumnCount; column++)
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
    ""MatchesCount"" = @matchesCount,
    ""IsFinished"" = @isFinished
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
    ""Value"" = @value
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
                    command.Parameters.AddWithValue("matchesCount", gameDto.MatchesCount);
                    command.Parameters.AddWithValue("isFinished", gameDto.IsFinished);

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

                foreach (var cell in cells)
                {
                    int r = cell.Row;
                    int c = cell.Column;

                    if (r < 0 || r >= GameModel.RowCount || c < 0 || c >= GameModel.ColumnCount)
                        continue;

                    int newValue = gameDto.Cells[r, c];

                    using (var command = new NpgsqlCommand(updateCellQuery, connection))
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.Parameters.AddWithValue("value", newValue);
                        command.Parameters.AddWithValue("cellId", cell.Id);
                        command.ExecuteNonQuery();
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
