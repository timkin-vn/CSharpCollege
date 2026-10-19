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
    public class UserRepository : IUserRepository
    {
        private readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        public IEnumerable<UserDto> GetAll()
        {
            const string query = @"
SELECT ""Id"", ""Name""
FROM ""Users""
ORDER BY ""Name"";";

            var result = new List<UserDto>();

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new UserDto
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }

            return result;
        }

        public UserDto Create(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException(
                    "Имя пользователя не может быть пустым.",
                    nameof(userName));

            userName = userName.Trim();

            var existingUser = GetByName(userName);
            if (existingUser != null)
                return existingUser;

            const string query = @"
INSERT INTO ""Users"" (""Name"")
VALUES (@userName)
RETURNING ""Id"";";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                int userId;

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("userName", userName);

                    userId = Convert.ToInt32(command.ExecuteScalar());
                }

                return GetById(userId);
            }
        }

        public UserDto GetById(int userId)
        {
            const string query = @"
SELECT ""Id"", ""Name""
FROM ""Users""
WHERE ""Id"" = @userId;";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        return new UserDto
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                    }
                }
            }
        }

        public UserDto GetByName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;

            const string query = @"
SELECT ""Id"", ""Name""
FROM ""Users""
WHERE LOWER(""Name"") = LOWER(@userName)
LIMIT 1;";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "userName", userName.Trim());

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        return new UserDto
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                    }
                }
            }
        }

        public void Delete(int userId)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        const string deleteCellsQuery = @"
DELETE FROM ""Cells""
WHERE ""GameId"" IN
(
    SELECT ""Id""
    FROM ""Games""
    WHERE ""UserId"" = @userId
);";

                        using (var command = new NpgsqlCommand(
                            deleteCellsQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("userId", userId);
                            command.ExecuteNonQuery();
                        }

                        // Затем сами партии.
                        const string deleteGamesQuery = @"
DELETE FROM ""Games""
WHERE ""UserId"" = @userId;";

                        using (var command = new NpgsqlCommand(
                            deleteGamesQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("userId", userId);
                            command.ExecuteNonQuery();
                        }

                        // И в последнюю очередь пользователя.
                        const string deleteUserQuery = @"
DELETE FROM ""Users""
WHERE ""Id"" = @userId;";

                        using (var command = new NpgsqlCommand(
                            deleteUserQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("userId", userId);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}