using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using Npgsql;
using System.Collections.Generic;
using System.Linq;   



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
    g.""Score"",
    g.""IsWin"",
    g.""IsLose"",
    c.""Row"",
    c.""Column"",
    c.""Value""
from ""Games"" g
left join ""GameCells"" c on g.""Id"" = c.""GameId""
where g.""Id"" = @gameId;
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                GameDto result = null;

                using (var command = new NpgsqlCommand(selectQuery, connection))
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
                                    Score = reader.GetInt32(3),
                                    IsWin = reader.GetBoolean(4),
                                    IsLose = reader.GetBoolean(5),
                                    Cells = new int[Constants.RowCount, Constants.ColumnCount]
                                };

                                for (int r = 0; r < Constants.RowCount; r++)
                                    for (int c = 0; c < Constants.ColumnCount; c++)
                                        result.Cells[r, c] = 0;
                            }

                            if (reader.IsDBNull(6))
                                continue;

                            int rowVal = reader.GetInt32(6);
                            int colVal = reader.GetInt32(7);
                            int value = reader.GetInt32(8);

                            result.Cells[rowVal, colVal] = value;
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
    g.""Score"",
    g.""IsWin"",
    g.""IsLose"",
    c.""Row"",
    c.""Column"",
    c.""Value""
from ""Games"" g
left join ""GameCells"" c on g.""Id"" = c.""GameId""
where g.""UserId"" = @userId
order by g.""Id"";
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                var result = new List<GameDto>();

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            var gameDto = result.FirstOrDefault(x => x.Id == id);

                            if (gameDto == null)
                            {
                                gameDto = new GameDto
                                {
                                    Id = id,
                                    UserId = reader.GetInt32(1),
                                    MoveCount = reader.GetInt32(2),

                                   
Score = reader.GetInt32(3),
                                    IsWin = reader.GetBoolean(4),
                                    IsLose = reader.GetBoolean(5),
                                    Cells = new int[Constants.RowCount, Constants.ColumnCount]
                                };

                                for (int r = 0; r < Constants.RowCount; r++)
                                    for (int c = 0; c < Constants.ColumnCount; c++)
                                        gameDto.Cells[r, c] = 0;

                                result.Add(gameDto);
                            }

                            if (reader.IsDBNull(6))
                                continue;

                            int rowVal = reader.GetInt32(6);
                            int colVal = reader.GetInt32(7);
                            int value = reader.GetInt32(8);

                            gameDto.Cells[rowVal, colVal] = value;
                        }
                    }
                }

                return result;
            }
        }

        public void Remove(int gameId)
        {
            var cellsRemoveQuery = @"delete from ""GameCells"" where ""GameId"" = @gameId;";
            var gameRemoveQuery = @"delete from ""Games"" where ""Id"" = @gameId;";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(cellsRemoveQuery, connection))
                {
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.ExecuteNonQuery();
                }

                using (var command = new NpgsqlCommand(gameRemoveQuery, connection))
                {
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int Save(GameDto gameDto)
        {
            if (gameDto.Id == 0)
                return Create(gameDto);

            return Update(gameDto);
        }

        private int Create(GameDto gameDto)
        {
            var insertGameQuery = @"
insert into ""Games"" (""UserId"", ""MoveCount"", ""Score"", ""IsWin"", ""IsLose"")
values (@userId, @moveCount, @score, @isWin, @isLose)
returning ""Id"";
";

            var insertCellQuery = @"
insert into ""GameCells"" (""GameId"", ""Row"", ""Column"", ""Value"")
values (@gameId, @row, @column, @value);
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                int gameId;
                using (var command = new NpgsqlCommand(insertGameQuery, connection))
                {
                    command.Parameters.AddWithValue("userId", gameDto.UserId);
                    command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                    command.Parameters.AddWithValue("score", gameDto.Score);
                    command.Parameters.AddWithValue("isWin", gameDto.IsWin);
                    command.Parameters.AddWithValue("isLose", gameDto.IsLose);
                    gameId = (int)command.ExecuteScalar();
                }

                for (int r = 0; r < Constants.RowCount; r++)
                {
                    for (int c = 0; c < Constants.ColumnCount; c++)
                    {
                        int value = gameDto.Cells[r, c];
                        if (value == 0) continue;

                        using (var command = new NpgsqlCommand(insertCellQuery, connection))
                        {
                            command.Parameters.AddWithValue("gameId", gameId);
                            command.Parameters.AddWithValue("row", r);
                            command.Parameters.AddWithValue("column", c);
                            command.Parameters.AddWithValue("value", value);
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
set ""MoveCount"" = @moveCount,
    ""Score"" = @score,
    ""IsWin"" = @isWin,
    ""IsLose"" = @isLose
where ""Id"" = @gameId;
";

            var deleteCellsQuery = @"delete from ""GameCells"" where ""GameId"" = @gameId;";

            var insertCellQuery = @"
insert into ""GameCells"" (""GameId"", ""Row"", ""Column"", ""Value"")
values (@gameId, @row, @column, @value);
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(updateGameQuery, connection))
                {
                    command.Parameters.AddWithValue("gameId", gameDto.Id);
                    command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                    command.Parameters.AddWithValue("score", gameDto.Score);
                    command.Parameters.AddWithValue("isWin", gameDto.IsWin);
                    command.Parameters.AddWithValue("isLose", gameDto.IsLose);
                    command.ExecuteNonQuery();
                }

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
                        if (value == 0) continue;

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
    }
}