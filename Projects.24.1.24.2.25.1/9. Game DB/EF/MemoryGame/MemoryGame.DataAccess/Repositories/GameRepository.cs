using MemoryGame.Common.Contracts.Repositories;
using MemoryGame.Common.Dtos;
using Npgsql;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MemoryGame.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        private const string SelectGamesQuery = @"
select
    g.""Id"",
    g.""UserId"",
    g.""MoveCount"",
    c.""Row"",
    c.""Column"",
    c.""Value"",
    c.""State""
from
    ""Games"" g
    join ""Cells"" c on g.""Id"" = c.""GameId""
";

        public GameDto GetByGameId(int gameId)
        {
            var selectQuery = SelectGamesQuery + @"
where
    g.""Id"" = @gameId
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(selectQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("gameId", gameId);
                    return ReadGames(command).FirstOrDefault();
                }
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            var selectQuery = SelectGamesQuery + @"
where
    g.""UserId"" = @userId
order by
    g.""Id""
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(selectQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("userId", userId);
                    return ReadGames(command);
                }
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

        private List<GameDto> ReadGames(NpgsqlCommand command)
        {
            var result = new List<GameDto>();

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

                    var row = reader.GetInt32(3);
                    var column = reader.GetInt32(4);
                    gameDto.Cells[row, column] = reader.GetInt32(5);
                    gameDto.States[row, column] = reader.GetInt32(6);
                }
            }

            return result;
        }

        private int Create(GameDto gameDto)
        {
            var insertGameQuery = @"
insert into ""Games"" (""UserId"", ""MoveCount"")
values (@userId, @moveCount)
returning ""Id""
";

            var insertCellQuery = @"
insert into ""Cells"" (""GameId"", ""Row"", ""Column"", ""Value"", ""State"")
values (@gameId, @row, @column, @value, @state)
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    int gameId;

                    using (var command = new NpgsqlCommand(insertGameQuery, connection, transaction) { CommandType = System.Data.CommandType.Text })
                    {
                        command.Parameters.AddWithValue("userId", gameDto.UserId);
                        command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                        gameId = (int)command.ExecuteScalar();
                    }

                    for (int row = 0; row < gameDto.Cells.GetLength(0); row++)
                    {
                        for (int column = 0; column < gameDto.Cells.GetLength(1); column++)
                        {
                            using (var command = new NpgsqlCommand(insertCellQuery, connection, transaction) { CommandType = System.Data.CommandType.Text })
                            {
                                command.Parameters.AddWithValue("gameId", gameId);
                                command.Parameters.AddWithValue("row", row);
                                command.Parameters.AddWithValue("column", column);
                                command.Parameters.AddWithValue("value", gameDto.Cells[row, column]);
                                command.Parameters.AddWithValue("state", gameDto.States[row, column]);
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                    return gameId;
                }
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
    ""Value"" = @value,
    ""State"" = @state
where
    ""GameId"" = @gameId
    and ""Row"" = @row
    and ""Column"" = @column
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = new NpgsqlCommand(updateGameQuery, connection, transaction) { CommandType = System.Data.CommandType.Text })
                    {
                        command.Parameters.AddWithValue("gameId", gameDto.Id);
                        command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                        command.ExecuteNonQuery();
                    }

                    for (int row = 0; row < gameDto.Cells.GetLength(0); row++)
                    {
                        for (int column = 0; column < gameDto.Cells.GetLength(1); column++)
                        {
                            using (var command = new NpgsqlCommand(updateCellQuery, connection, transaction) { CommandType = System.Data.CommandType.Text })
                            {
                                command.Parameters.AddWithValue("gameId", gameDto.Id);
                                command.Parameters.AddWithValue("row", row);
                                command.Parameters.AddWithValue("column", column);
                                command.Parameters.AddWithValue("value", gameDto.Cells[row, column]);
                                command.Parameters.AddWithValue("state", gameDto.States[row, column]);
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                    return gameDto.Id;
                }
            }
        }
    }
}
