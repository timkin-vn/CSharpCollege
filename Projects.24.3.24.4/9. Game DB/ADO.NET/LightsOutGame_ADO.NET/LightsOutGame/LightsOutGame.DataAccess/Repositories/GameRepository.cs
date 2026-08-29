using LightsOutGame.Common.Contracts.Repositories;
using LightsOutGame.Common.Definitions;
using LightsOutGame.Common.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.DataAccess.Repositories
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
    c.""IsOn""
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
                            }

                            var rowVal = reader.GetInt32(3);
                            var columnVal = reader.GetInt32(4);
                            var isOn = reader.GetBoolean(5);

                            result.Cells[rowVal, columnVal] = isOn;
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
    c.""IsOn""
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
                var result = new List<GameDto>();

                using (var command = new NpgsqlCommand(selectQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            var gameDto = result.FirstOrDefault(g => g.Id == id);

                            if (gameDto == null)
                            {
                                gameDto = new GameDto
                                {
                                    Id = id,
                                    UserId = reader.GetInt32(1),
                                    MoveCount = reader.GetInt32(2),
                                };

                                result.Add(gameDto);
                            }

                            var rowVal = reader.GetInt32(3);
                            var columnVal = reader.GetInt32(4);
                            var isOn = reader.GetBoolean(5);

                            gameDto.Cells[rowVal, columnVal] = isOn;
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

            var deleteGameQuery = @"
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

                using (var command = new NpgsqlCommand(deleteGameQuery, connection) { CommandType = System.Data.CommandType.Text })
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
insert into ""Cells"" (""GameId"", ""Row"", ""Column"", ""IsOn"")
values (@gameId, @row, @column, @isOn)
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
                        using (var command = new NpgsqlCommand(insertCellQuery, connection) { CommandType = System.Data.CommandType.Text })
                        {
                            command.Parameters.AddWithValue("gameId", gameId);
                            command.Parameters.AddWithValue("row", row);
                            command.Parameters.AddWithValue("column", column);
                            command.Parameters.AddWithValue("isOn", gameDto.Cells[row, column]);

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

            var updateCellQuery = @"
update ""Cells""
set
    ""IsOn"" = @isOn
where
    ""GameId"" = @gameId
    and ""Row"" = @row
    and ""Column"" = @column
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

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        using (var command = new NpgsqlCommand(updateCellQuery, connection) { CommandType = System.Data.CommandType.Text })
                        {
                            command.Parameters.AddWithValue("isOn", gameDto.Cells[row, column]);
                            command.Parameters.AddWithValue("gameId", gameId);
                            command.Parameters.AddWithValue("row", row);
                            command.Parameters.AddWithValue("column", column);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                return gameId;
            }
        }
    }
}
