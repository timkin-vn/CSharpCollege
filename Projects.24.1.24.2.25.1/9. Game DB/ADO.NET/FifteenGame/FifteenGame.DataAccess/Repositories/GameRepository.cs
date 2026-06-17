using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

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
    g.""Score"",
    g.""MoveCount"",
    g.""IsWin"",
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
                                    Score = reader.GetInt32(2),
                                    MoveCount = reader.GetInt32(3),
                                    IsWin = reader.GetBoolean(4),
                                };

                                // Инициализируем пустые клетки
                                for (int row = 0; row < Constants.RowCount; row++)
                                    for (int col = 0; col < Constants.ColumnCount; col++)
                                        result.Cells[row, col] = 0;
                            }

                            var rowVal = reader.GetInt32(5);
                            var columnVal = reader.GetInt32(6);
                            var value = reader.GetInt32(7);

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
    g.""Score"",
    g.""MoveCount"",
    g.""IsWin"",
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
                var result = new List<GameDto>();

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
                                    Score = reader.GetInt32(2),
                                    MoveCount = reader.GetInt32(3),
                                    IsWin = reader.GetBoolean(4),
                                };

                                for (int row = 0; row < Constants.RowCount; row++)
                                    for (int col = 0; col < Constants.ColumnCount; col++)
                                        gameDto.Cells[row, col] = 0;

                                result.Add(gameDto);
                            }

                            var rowVal = reader.GetInt32(5);
                            var columnVal = reader.GetInt32(6);
                            var value = reader.GetInt32(7);

                            gameDto.Cells[rowVal, columnVal] = value;
                        }
                    }
                }

                return result;
            }
        }

        public void Remove(int gameId)
        {
            var deleteCellsQuery = @"delete from ""Cells"" where ""GameId"" = @gameId";
            var deleteGameQuery = @"delete from ""Games"" where ""Id"" = @gameId";

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
                return Create(gameDto);
            else
                return Update(gameDto);
        }

        private int Create(GameDto gameDto)
        {
            var insertGameQuery = @"
insert into ""Games"" (""UserId"", ""Score"", ""MoveCount"", ""IsWin"")
values (@userId, @score, @moveCount, @isWin)
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
                    command.Parameters.AddWithValue("score", gameDto.Score);
                    command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                    command.Parameters.AddWithValue("isWin", gameDto.IsWin);
                    gameId = (int)command.ExecuteScalar();
                }

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int col = 0; col < Constants.ColumnCount; col++)
                    {
                        if (gameDto.Cells[row, col] == 0) continue;

                        using (var command = new NpgsqlCommand(insertCellQuery, connection) { CommandType = System.Data.CommandType.Text })
                        {
                            command.Parameters.AddWithValue("gameId", gameId);
                            command.Parameters.AddWithValue("row", row);
                            command.Parameters.AddWithValue("column", col);
                            command.Parameters.AddWithValue("value", gameDto.Cells[row, col]);
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
    ""Score"" = @score,
    ""MoveCount"" = @moveCount,
    ""IsWin"" = @isWin
where
    ""Id"" = @gameId
";

            // Удаляем все старые клетки и вставляем новые
            var deleteCellsQuery = @"delete from ""Cells"" where ""GameId"" = @gameId";
            var insertCellQuery = @"
insert into ""Cells"" (""GameId"", ""Row"", ""Column"", ""Value"")
values (@gameId, @row, @column, @value)
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                int gameId = gameDto.Id;

                // Обновляем игру
                using (var command = new NpgsqlCommand(updateGameQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.Parameters.AddWithValue("score", gameDto.Score);
                    command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                    command.Parameters.AddWithValue("isWin", gameDto.IsWin);
                    command.ExecuteNonQuery();
                }

                // Удаляем все клетки
                using (var command = new NpgsqlCommand(deleteCellsQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.ExecuteNonQuery();
                }

                // Вставляем новые клетки
                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int col = 0; col < Constants.ColumnCount; col++)
                    {
                        if (gameDto.Cells[row, col] == 0) continue;

                        using (var command = new NpgsqlCommand(insertCellQuery, connection) { CommandType = System.Data.CommandType.Text })
                        {
                            command.Parameters.AddWithValue("gameId", gameId);
                            command.Parameters.AddWithValue("row", row);
                            command.Parameters.AddWithValue("column", col);
                            command.Parameters.AddWithValue("value", gameDto.Cells[row, col]);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                return gameId;
            }
        }
    }
}