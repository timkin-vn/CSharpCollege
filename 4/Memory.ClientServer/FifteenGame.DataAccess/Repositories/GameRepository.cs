using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace FifteenGame.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        public int Save(GameDto gameDto)
        {
            if (gameDto.Id == 0)
                return Create(gameDto);
            return Update(gameDto);
        }

        private int Create(GameDto gameDto)
        {
            var insertGameQuery = @"
insert into ""Games"" (""UserId"", ""MoveCount"", ""GameBegin"", ""Field"", ""Score"")
values (@userId, @moveCount, @gameBegin, @field, @score)
returning ""Id"";";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(insertGameQuery, connection))
                {
                    command.Parameters.AddWithValue("userId", gameDto.UserId);
                    command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                    command.Parameters.AddWithValue("gameBegin", gameDto.GameStart);
                    command.Parameters.AddWithValue("field", FieldSerializer.Serialize(gameDto.Field));
                    command.Parameters.AddWithValue("score", gameDto.Score);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private int Update(GameDto gameDto)
        {
            var updateGameQuery = @"
update ""Games""
set ""MoveCount"" = @moveCount,
    ""Field"" = @field,
    ""Score"" = @score
where ""Id"" = @gameId;";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(updateGameQuery, connection))
                {
                    command.Parameters.AddWithValue("gameId", gameDto.Id);
                    command.Parameters.AddWithValue("moveCount", gameDto.MoveCount);
                    command.Parameters.AddWithValue("field", FieldSerializer.Serialize(gameDto.Field));
                    command.Parameters.AddWithValue("score", gameDto.Score);

                    command.ExecuteNonQuery();
                }
            }
            return gameDto.Id;
        }

        public GameDto GetByGameId(int gameId)
        {
            var selectQuery = @"
select ""Id"", ""UserId"", ""MoveCount"", ""GameBegin"", ""Field"", ""Score""
from ""Games""
where ""Id"" = @gameId;";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("gameId", gameId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new GameDto
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                MoveCount = reader.GetInt32(2),
                                GameStart = reader.GetDateTime(3),
                                Field = FieldSerializer.Deserialize(reader.GetString(4)),
                                Score = reader.GetInt32(5)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public IEnumerable<GameDto> GetByUserId(int userId)
        {
            var selectQuery = @"
select ""Id"", ""UserId"", ""MoveCount"", ""GameBegin"", ""Field"", ""Score""
from ""Games""
where ""UserId"" = @userId
order by ""Id"";";

            var result = new List<GameDto>();

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new GameDto
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                MoveCount = reader.GetInt32(2),
                                GameStart = reader.GetDateTime(3),
                                Field = FieldSerializer.Deserialize(reader.GetString(4)),
                                Score = reader.GetInt32(5)
                            });
                        }
                    }
                }
            }
            return result;
        }

        public void Remove(int gameId)
        {
            var removeGameQuery = @"delete from ""Games"" where ""Id"" = @gameId;";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(removeGameQuery, connection))
                {
                    command.Parameters.AddWithValue("gameId", gameId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
