using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace FifteenGame.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        public GameDto GetByGameId(int gameId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return LoadGames(connection, null, gameId).FirstOrDefault();
            }
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return LoadGames(connection, userId, null);
            }
        }

        public void Remove(int gameId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(
                    @"DELETE FROM public.""Games"" WHERE ""Id"" = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("id", gameId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int Save(GameDto gameDto)
        {
            if (gameDto == null)
                throw new ArgumentNullException(nameof(gameDto));

            if (gameDto.Cells == null ||
                gameDto.Cells.GetLength(0) != 4 ||
                gameDto.Cells.GetLength(1) != 4)
            {
                throw new ArgumentException(
                    "Игровое поле должно иметь размер 4x4.",
                    nameof(gameDto));
            }

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    int gameId = gameDto.Id;

                    if (gameId == 0)
                    {
                        const string insertGame = @"
INSERT INTO public.""Games""
    (""UserId"", ""MoveCount"", ""Score"", ""BestScore"")
VALUES
    (@userId, @moveCount, @score, @bestScore)
RETURNING ""Id""";

                        using (var command = new NpgsqlCommand(
                            insertGame, connection, transaction))
                        {
                            AddGameParameters(command, gameDto);

                            gameId = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }
                    else
                    {
                        const string updateGame = @"
UPDATE public.""Games""
SET ""MoveCount"" = @moveCount,
    ""Score"" = @score,
    ""BestScore"" = @bestScore
WHERE ""Id"" = @id";

                        using (var command = new NpgsqlCommand(
                            updateGame, connection, transaction))
                        {
                            command.Parameters.AddWithValue(
                                "moveCount", gameDto.MoveCount);
                            command.Parameters.AddWithValue(
                                "score", gameDto.Score);
                            command.Parameters.AddWithValue(
                                "bestScore", gameDto.BestScore);
                            command.Parameters.AddWithValue("id", gameId);

                            if (command.ExecuteNonQuery() == 0)
                            {
                                throw new InvalidOperationException(
                                    "Игра не найдена в базе данных.");
                            }
                        }
                    }

                    using (var command = new NpgsqlCommand(
                        @"DELETE FROM public.""Cells"" WHERE ""GameId"" = @id",
                        connection, transaction))
                    {
                        command.Parameters.AddWithValue("id", gameId);
                        command.ExecuteNonQuery();
                    }

                    const string insertCell = @"
INSERT INTO public.""Cells""
    (""GameId"", ""Row"", ""Column"", ""Value"")
VALUES
    (@gameId, @row, @column, @value)";

                    using (var command = new NpgsqlCommand(
                        insertCell, connection, transaction))
                    {
                        var gameIdParameter =
                            command.Parameters.Add("gameId", NpgsqlTypes.NpgsqlDbType.Integer);
                        var rowParameter =
                            command.Parameters.Add("row", NpgsqlTypes.NpgsqlDbType.Integer);
                        var columnParameter =
                            command.Parameters.Add("column", NpgsqlTypes.NpgsqlDbType.Integer);
                        var valueParameter =
                            command.Parameters.Add("value", NpgsqlTypes.NpgsqlDbType.Integer);

                        gameIdParameter.Value = gameId;

                        for (int row = 0; row < 4; row++)
                        {
                            for (int column = 0; column < 4; column++)
                            {
                                rowParameter.Value = row;
                                columnParameter.Value = column;
                                valueParameter.Value = gameDto.Cells[row, column];

                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                    return gameId;
                }
            }
        }

        private static void AddGameParameters(
            NpgsqlCommand command,
            GameDto gameDto)
        {
            command.Parameters.AddWithValue("userId", gameDto.UserId);
            command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
            command.Parameters.AddWithValue("score", gameDto.Score);
            command.Parameters.AddWithValue("bestScore", gameDto.BestScore);
        }

        private static List<GameDto> LoadGames(
    NpgsqlConnection connection,
    int? userId,
    int? gameId)
        {
            var result = new Dictionary<int, GameDto>();

            const string query = @"
SELECT g.""Id"", g.""UserId"", g.""MoveCount"",
       g.""Score"", g.""BestScore"",
       c.""Row"", c.""Column"", c.""Value""
FROM public.""Games"" g
LEFT JOIN public.""Cells"" c ON c.""GameId"" = g.""Id""
WHERE (@userId::integer IS NULL OR g.""UserId"" = @userId)
  AND (@gameId::integer IS NULL OR g.""Id"" = @gameId)
ORDER BY g.""Id"", c.""Row"", c.""Column""";

            using (var command = new NpgsqlCommand(query, connection))
            {
                var userIdParameter = command.Parameters.Add(
                    "userId", NpgsqlTypes.NpgsqlDbType.Integer);

                userIdParameter.Value = userId.HasValue
                    ? (object)userId.Value
                    : DBNull.Value;

                var gameIdParameter = command.Parameters.Add(
                    "gameId", NpgsqlTypes.NpgsqlDbType.Integer);

                gameIdParameter.Value = gameId.HasValue
                    ? (object)gameId.Value
                    : DBNull.Value;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        GameDto dto;

                        if (!result.TryGetValue(id, out dto))
                        {
                            dto = new GameDto
                            {
                                Id = id,
                                UserId = reader.GetInt32(1),
                                MoveCount = reader.GetInt32(2),
                                Score = reader.GetInt32(3),
                                BestScore = reader.GetInt32(4)
                            };

                            result.Add(id, dto);
                        }

                        if (!reader.IsDBNull(5))
                        {
                            int row = reader.GetInt32(5);
                            int column = reader.GetInt32(6);
                            dto.Cells[row, column] = reader.GetInt32(7);
                        }
                    }
                }
            }

            return result.Values.ToList();
        }
    }
}