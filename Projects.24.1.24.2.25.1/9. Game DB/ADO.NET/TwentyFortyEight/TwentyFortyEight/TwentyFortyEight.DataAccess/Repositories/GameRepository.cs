using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Npgsql;
using TwentyFortyEight.Common.Contracts.Repositories;
using TwentyFortyEight.Common.Definitions;
using TwentyFortyEight.Common.Dtos;

namespace TwentyFortyEight.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        // ─────────────────────────────────────────────────────────────────
        //  GetByGameId
        // ─────────────────────────────────────────────────────────────────
        public GameDto GetByGameId(int gameId)
        {
            const string sql = @"
select
    g.""Id"",
    g.""UserId"",
    g.""Score"",
    g.""BestTile"",
    g.""IsWon"",
    c.""Row"",
    c.""Column"",
    c.""Value""
from ""Games"" g
join ""Cells"" c on g.""Id"" = c.""GameId""
where g.""Id"" = @gameId
";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                GameDto result = null;

                using (var cmd = new NpgsqlCommand(sql, connection)
                       { CommandType = System.Data.CommandType.Text })
                {
                    cmd.Parameters.AddWithValue("gameId", gameId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (result == null)
                            {
                                result = new GameDto
                                {
                                    Id      = reader.GetInt32(0),
                                    UserId  = reader.GetInt32(1),
                                    Score   = reader.GetInt32(2),
                                    BestTile = reader.GetInt32(3),
                                    IsWon   = reader.GetBoolean(4)
                                };
                            }

                            result.Cells[reader.GetInt32(5), reader.GetInt32(6)]
                                = reader.GetInt32(7);
                        }
                    }
                }

                return result;
            }
        }

        // ─────────────────────────────────────────────────────────────────
        //  GetByUserId
        // ─────────────────────────────────────────────────────────────────
        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            const string sql = @"
select
    g.""Id"",
    g.""UserId"",
    g.""Score"",
    g.""BestTile"",
    g.""IsWon"",
    c.""Row"",
    c.""Column"",
    c.""Value""
from ""Games"" g
join ""Cells"" c on g.""Id"" = c.""GameId""
where g.""UserId"" = @userId
order by g.""Id""
";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                var result = new List<GameDto>();

                using (var cmd = new NpgsqlCommand(sql, connection)
                       { CommandType = System.Data.CommandType.Text })
                {
                    cmd.Parameters.AddWithValue("userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            var dto = result.FirstOrDefault(g => g.Id == id);

                            if (dto == null)
                            {
                                dto = new GameDto
                                {
                                    Id      = id,
                                    UserId  = reader.GetInt32(1),
                                    Score   = reader.GetInt32(2),
                                    BestTile = reader.GetInt32(3),
                                    IsWon   = reader.GetBoolean(4)
                                };
                                result.Add(dto);
                            }

                            dto.Cells[reader.GetInt32(5), reader.GetInt32(6)]
                                = reader.GetInt32(7);
                        }
                    }
                }

                return result;
            }
        }

        // ─────────────────────────────────────────────────────────────────
        //  Remove
        // ─────────────────────────────────────────────────────────────────
        public void Remove(int gameId)
        {
            const string deleteCells = @"delete from ""Cells"" where ""GameId"" = @gameId";
            const string deleteGame  = @"delete from ""Games"" where ""Id""     = @gameId";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                Execute(connection, deleteCells, ("gameId", gameId));
                Execute(connection, deleteGame,  ("gameId", gameId));
            }
        }

        // ─────────────────────────────────────────────────────────────────
        //  Save  (insert if Id == 0, otherwise update)
        // ─────────────────────────────────────────────────────────────────
        public int Save(GameDto gameDto)
        {
            return gameDto.Id == 0 ? Create(gameDto) : Update(gameDto);
        }

        // ── private helpers ───────────────────────────────────────────────

        private int Create(GameDto dto)
        {
            const string insertGame = @"
insert into ""Games"" (""UserId"", ""Score"", ""BestTile"", ""IsWon"")
values (@userId, @score, @bestTile, @isWon)
returning ""Id""
";
            const string insertCell = @"
insert into ""Cells"" (""GameId"", ""Row"", ""Column"", ""Value"")
values (@gameId, @row, @column, @value)
";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                int gameId;

                using (var cmd = new NpgsqlCommand(insertGame, connection)
                       { CommandType = System.Data.CommandType.Text })
                {
                    cmd.Parameters.AddWithValue("userId",  dto.UserId);
                    cmd.Parameters.AddWithValue("score",   dto.Score);
                    cmd.Parameters.AddWithValue("bestTile", dto.BestTile);
                    cmd.Parameters.AddWithValue("isWon",   dto.IsWon);
                    gameId = (int)cmd.ExecuteScalar();
                }

                for (int r = 0; r < Constants.RowCount; r++)
                {
                    for (int c = 0; c < Constants.ColumnCount; c++)
                    {
                        int val = dto.Cells[r, c];
                        if (val == 0) continue;

                        using (var cmd = new NpgsqlCommand(insertCell, connection)
                               { CommandType = System.Data.CommandType.Text })
                        {
                            cmd.Parameters.AddWithValue("gameId", gameId);
                            cmd.Parameters.AddWithValue("row",    r);
                            cmd.Parameters.AddWithValue("column", c);
                            cmd.Parameters.AddWithValue("value",  val);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                return gameId;
            }
        }

        private int Update(GameDto dto)
        {
            const string updateGame = @"
update ""Games""
set ""Score""    = @score,
    ""BestTile"" = @bestTile,
    ""IsWon""    = @isWon
where ""Id"" = @gameId
";
            const string deleteCells = @"delete from ""Cells"" where ""GameId"" = @gameId";

            const string insertCell = @"
insert into ""Cells"" (""GameId"", ""Row"", ""Column"", ""Value"")
values (@gameId, @row, @column, @value)
";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand(updateGame, connection)
                       { CommandType = System.Data.CommandType.Text })
                {
                    cmd.Parameters.AddWithValue("gameId",  dto.Id);
                    cmd.Parameters.AddWithValue("score",   dto.Score);
                    cmd.Parameters.AddWithValue("bestTile", dto.BestTile);
                    cmd.Parameters.AddWithValue("isWon",   dto.IsWon);
                    cmd.ExecuteNonQuery();
                }

                Execute(connection, deleteCells, ("gameId", dto.Id));

                for (int r = 0; r < Constants.RowCount; r++)
                {
                    for (int c = 0; c < Constants.ColumnCount; c++)
                    {
                        int val = dto.Cells[r, c];
                        if (val == 0) continue;

                        using (var cmd = new NpgsqlCommand(insertCell, connection)
                               { CommandType = System.Data.CommandType.Text })
                        {
                            cmd.Parameters.AddWithValue("gameId", dto.Id);
                            cmd.Parameters.AddWithValue("row",    r);
                            cmd.Parameters.AddWithValue("column", c);
                            cmd.Parameters.AddWithValue("value",  val);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                return dto.Id;
            }
        }

        private static void Execute(NpgsqlConnection conn, string sql,
            params (string name, object value)[] parameters)
        {
            using (var cmd = new NpgsqlCommand(sql, conn)
                   { CommandType = System.Data.CommandType.Text })
            {
                foreach (var (name, value) in parameters)
                    cmd.Parameters.AddWithValue(name, value);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
