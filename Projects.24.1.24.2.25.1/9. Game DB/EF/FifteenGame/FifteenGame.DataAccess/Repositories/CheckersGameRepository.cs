using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Npgsql;
using NpgsqlTypes;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Dtos;

namespace FifteenGame.DataAccess.Repositories
{
    public class CheckersGameRepository : ICheckersGameRepository
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        public CheckersGameDto GetById(int gameId)
        {
            CheckersGameDto game = null;
            const string selectGame = @"
                SELECT ""Id"", ""UserId"", ""StartDate"", ""CurrentPlayer"", ""IsFinished"", ""Winner"", ""GameStateJson""
                FROM ""CheckersGames""
                WHERE ""Id"" = @id";

            const string selectMoves = @"
                SELECT ""Id"", ""GameId"", ""MoveNumber"", ""FromRow"", ""FromCol"", ""ToRow"", ""ToCol"",
                       ""IsCapture"", ""CapturedRow"", ""CapturedCol"", ""PromotedToKing"", ""MoveTime""
                FROM ""CheckersMoves""
                WHERE ""GameId"" = @gameId
                ORDER BY ""MoveNumber""";

            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(selectGame, conn))
                {
                    cmd.Parameters.AddWithValue("id", gameId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            game = new CheckersGameDto
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                StartDate = reader.GetDateTime(2),
                                CurrentPlayer = reader.GetInt32(3),
                                IsFinished = reader.GetBoolean(4),
                                Winner = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                                GameStateJson = reader.IsDBNull(6) ? null : reader.GetString(6)
                            };
                        }
                    }
                }

                if (game != null)
                {
                    using (var cmd = new NpgsqlCommand(selectMoves, conn))
                    {
                        cmd.Parameters.AddWithValue("gameId", gameId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                game.Moves.Add(new CheckersMoveDto
                                {
                                    Id = reader.GetInt32(0),
                                    GameId = reader.GetInt32(1),
                                    MoveNumber = reader.GetInt32(2),
                                    FromRow = reader.GetInt32(3),
                                    FromCol = reader.GetInt32(4),
                                    ToRow = reader.GetInt32(5),
                                    ToCol = reader.GetInt32(6),
                                    IsCapture = reader.GetBoolean(7),
                                    CapturedRow = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                    CapturedCol = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9),
                                    PromotedToKing = reader.GetBoolean(10),
                                    MoveTime = reader.GetDateTime(11)
                                });
                            }
                        }
                    }
                }
            }

            return game;
        }

        public IEnumerable<CheckersGameDto> GetByUserId(int userId)
        {
            var games = new List<CheckersGameDto>();
            const string selectGames = @"
                SELECT ""Id"", ""UserId"", ""StartDate"", ""CurrentPlayer"", ""IsFinished"", ""Winner"", ""GameStateJson""
                FROM ""CheckersGames""
                WHERE ""UserId"" = @userId
                ORDER BY ""StartDate"" DESC";

            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(selectGames, conn))
                {
                    cmd.Parameters.AddWithValue("userId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            games.Add(new CheckersGameDto
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                StartDate = reader.GetDateTime(2),
                                CurrentPlayer = reader.GetInt32(3),
                                IsFinished = reader.GetBoolean(4),
                                Winner = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                                GameStateJson = reader.IsDBNull(6) ? null : reader.GetString(6)
                            });
                        }
                    }
                }

                if (games.Count > 0)
                {
                    const string selectMoves = @"
                        SELECT ""Id"", ""GameId"", ""MoveNumber"", ""FromRow"", ""FromCol"", ""ToRow"", ""ToCol"",
                               ""IsCapture"", ""CapturedRow"", ""CapturedCol"", ""PromotedToKing"", ""MoveTime""
                        FROM ""CheckersMoves""
                        WHERE ""GameId"" = ANY(@gameIds)
                        ORDER BY ""MoveNumber""";

                    var ids = games.ConvertAll(g => g.Id).ToArray();
                    using (var cmd = new NpgsqlCommand(selectMoves, conn))
                    {

                        var param = new NpgsqlParameter("gameIds", NpgsqlDbType.Array | NpgsqlDbType.Integer);
                        param.Value = ids;
                        cmd.Parameters.Add(param);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int gameId = reader.GetInt32(1);
                                var move = new CheckersMoveDto
                                {
                                    Id = reader.GetInt32(0),
                                    GameId = gameId,
                                    MoveNumber = reader.GetInt32(2),
                                    FromRow = reader.GetInt32(3),
                                    FromCol = reader.GetInt32(4),
                                    ToRow = reader.GetInt32(5),
                                    ToCol = reader.GetInt32(6),
                                    IsCapture = reader.GetBoolean(7),
                                    CapturedRow = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                                    CapturedCol = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9),
                                    PromotedToKing = reader.GetBoolean(10),
                                    MoveTime = reader.GetDateTime(11)
                                };
                                var game = games.Find(g => g.Id == gameId);
                                game?.Moves.Add(move);
                            }
                        }
                    }
                }
            }

            return games;
        }

        public int Save(CheckersGameDto dto)
        {
            if (dto.Id == 0)
            {
                const string insertGame = @"
                    INSERT INTO ""CheckersGames"" (""UserId"", ""CurrentPlayer"", ""IsFinished"", ""Winner"", ""GameStateJson"")
                    VALUES (@userId, @currentPlayer, @isFinished, @winner, @stateJson)
                    RETURNING ""Id""";

                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(insertGame, conn))
                    {
                        cmd.Parameters.AddWithValue("userId", dto.UserId);
                        cmd.Parameters.AddWithValue("currentPlayer", dto.CurrentPlayer);
                        cmd.Parameters.AddWithValue("isFinished", dto.IsFinished);
                        cmd.Parameters.AddWithValue("winner", (object)dto.Winner ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("stateJson", (object)dto.GameStateJson ?? DBNull.Value);
                        dto.Id = (int)cmd.ExecuteScalar();
                    }
                }
            }
            else
            {
                const string updateGame = @"
                    UPDATE ""CheckersGames""
                    SET ""CurrentPlayer"" = @currentPlayer,
                        ""IsFinished"" = @isFinished,
                        ""Winner"" = @winner,
                        ""GameStateJson"" = @stateJson
                    WHERE ""Id"" = @id";

                using (var conn = new NpgsqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(updateGame, conn))
                    {
                        cmd.Parameters.AddWithValue("currentPlayer", dto.CurrentPlayer);
                        cmd.Parameters.AddWithValue("isFinished", dto.IsFinished);
                        cmd.Parameters.AddWithValue("winner", (object)dto.Winner ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("stateJson", (object)dto.GameStateJson ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("id", dto.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            return dto.Id;
        }

        public void Delete(int gameId)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(@"DELETE FROM ""CheckersMoves"" WHERE ""GameId"" = @gameId", conn))
                {
                    cmd.Parameters.AddWithValue("gameId", gameId);
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = new NpgsqlCommand(@"DELETE FROM ""CheckersGames"" WHERE ""Id"" = @gameId", conn))
                {
                    cmd.Parameters.AddWithValue("gameId", gameId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddMove(CheckersMoveDto move)
        {
            const string insertMove = @"
                INSERT INTO ""CheckersMoves"" (""GameId"", ""MoveNumber"", ""FromRow"", ""FromCol"", ""ToRow"", ""ToCol"",
                          ""IsCapture"", ""CapturedRow"", ""CapturedCol"", ""PromotedToKing"")
                VALUES (@gameId, @moveNum, @fromRow, @fromCol, @toRow, @toCol,
                        @isCapture, @capRow, @capCol, @promoted)";

            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(insertMove, conn))
                {
                    cmd.Parameters.AddWithValue("gameId", move.GameId);
                    cmd.Parameters.AddWithValue("moveNum", move.MoveNumber);
                    cmd.Parameters.AddWithValue("fromRow", move.FromRow);
                    cmd.Parameters.AddWithValue("fromCol", move.FromCol);
                    cmd.Parameters.AddWithValue("toRow", move.ToRow);
                    cmd.Parameters.AddWithValue("toCol", move.ToCol);
                    cmd.Parameters.AddWithValue("isCapture", move.IsCapture);
                    cmd.Parameters.AddWithValue("capRow", (object)move.CapturedRow ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("capCol", (object)move.CapturedCol ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("promoted", move.PromotedToKing);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}