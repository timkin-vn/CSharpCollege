using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.Repoistories
{
    public class GameRepository : IGameRepository
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
        //private const string ConnectionString =
        //   @"Server=127.0.0.1;Port=5432;Database=FifteenGame1Dev23;User Id=postgres;Password=Qwerty123;";

        public GameDto GetByGameId(int gameId)
        {
            var selectGameQuery = @"
select
    g.""Id"",
    g.""UserId"",
    g.""PlayerCount"",
    g.""GameBegin"",
    c.""Row"",
    c.""Column"",
    c.""Value""
from 
    ""Games"" g
    join ""Cells"" c on g.""Id"" = c.""GameId""
where 
    g.""Id"" = @gameId
";

            GameDto game = null;
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(selectGameQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (game == null)
                            {
                                game = new GameDto();
                                game.Id = reader.GetInt32(0);
                                game.UserId = reader.GetInt32(1);
                                game.PlayerCount = reader.GetInt32(2);
                                game.GameStart = reader.GetDateTime(3);

                                for (int row = 0; row < Constants.RowCount; row++)
                                {
                                    for (int column = 0; column < Constants.ColumnCount; column++)
                                    {
                                        game.Cells[row, column] = Constants.PlayerVal;
                                    }
                                }
                            }

                            var cellRow = reader.GetInt32(4);
                            var cellColumn = reader.GetInt32(5);
                            var value = reader.GetInt32(6);

                            game.Cells[cellRow, cellColumn] = value;
                        }
                    }
                }

                return game;
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            var selectGameQuery = @"
select
    g.""Id"",
    g.""UserId"",
    g.""PlayerCount"",
    g.""GameBegin"",
    c.""Row"",
    c.""Column"",
    c.""Value""
from 
    ""Games"" g
    join ""Cells"" c on g.""Id"" = c.""GameId""
where 
    g.""UserId"" = @userId
order by g.""Id""
";

            var result = new List<GameDto>();
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(selectGameQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var gameId = reader.GetInt32(0);
                            var game = result.FirstOrDefault(g => g.Id == gameId);

                            if (game == null)
                            {
                                game = new GameDto();
                                result.Add(game);

                                game.Id = gameId;
                                game.UserId = reader.GetInt32(1);
                                game.PlayerCount = reader.GetInt32(2);
                                game.GameStart = reader.GetDateTime(3);

                                for (int row = 0; row < Constants.RowCount; row++)
                                {
                                    for (int column = 0; column < Constants.ColumnCount; column++)
                                    {
                                        game.Cells[row, column] = Constants.PlayerVal;
                                    }
                                }
                            }

                            var cellRow = reader.GetInt32(4);
                            var cellColumn = reader.GetInt32(5);
                            var value = reader.GetInt32(6);

                            game.Cells[cellRow, cellColumn] = value;
                        }
                    }
                }

                return result;
            }
        }

        public void Remove(int gameId)
        {
            var cellsRemoveQuery = @"
delete from ""Cells""
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
            if (gameDto.Id  == 0)
            {
                return Create(gameDto);
            }
            else
            {
                return Update(gameDto);
            }
        }

        private int Create(GameDto gameDto)
        {
            var gameCreateQuery = @"
insert into ""Games"" (""UserId"", ""PlayerCount"", ""GameBegin"")
values (@userId, @PlayerCount, @gameBegin)
returning ""Id""
";

            var cellCreateQuery = @"
insert into ""Cells"" (""GameId"", ""Row"", ""Column"", ""Value"")
values (@gameId, @row, @column, @value)
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                int gameId;
                using (var command = new NpgsqlCommand(gameCreateQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("userId", gameDto.UserId);
                    command.Parameters.AddWithValue("PlayerCount", gameDto.PlayerCount);
                    command.Parameters.AddWithValue("gameBegin", gameDto.GameStart);

                    var result = command.ExecuteScalar();
                    gameId = (int)result;
                }

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        if (gameDto.Cells[row, column] == Constants.PlayerVal)
                        {
                            continue;
                        }

                        using (var command = new NpgsqlCommand(cellCreateQuery, connection))
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
            var gameUpdateQuery = @"
update ""Games""
set ""PlayerCount"" = @PlayerCount
where
    ""Id"" = @gameId
";

            var cellsRemoveQuery = @"
delete from ""Cells""
where ""GameId"" = @gameId
";

            var cellCreateQuery = @"
insert into ""Cells"" (""GameId"", ""Row"", ""Column"", ""Value"")
values (@gameId, @row, @column, @value)
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(gameUpdateQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameDto.Id);
                    command.Parameters.AddWithValue("PlayerCount", gameDto.PlayerCount);

                    var result = command.ExecuteNonQuery();
                }

                using (var command = new NpgsqlCommand(cellsRemoveQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("gameId", gameDto.Id);

                    var result = command.ExecuteNonQuery();
                }

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        if (gameDto.Cells[row, column] == Constants.PlayerVal)
                        {
                            continue;
                        }

                        using (var command = new NpgsqlCommand(cellCreateQuery, connection))
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
