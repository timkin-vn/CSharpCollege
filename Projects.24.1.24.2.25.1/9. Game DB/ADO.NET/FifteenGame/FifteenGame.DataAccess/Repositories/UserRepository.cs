using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Dtos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace FifteenGame.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        public IEnumerable<UserDto> GetAll()
        {
            var selectQuery = @"select ""Id"", ""Name"" from ""Users""";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                var result = new List<UserDto>();

                using (var command = new NpgsqlCommand(selectQuery, connection) { CommandType = System.Data.CommandType.Text })
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new UserDto
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                        });
                    }
                }

                return result;
            }
        }

        public UserDto Create(string userName)
        {
            var insertQuery = @"
insert into ""Users"" (""Name"")
values (@userName)
returning ""Id""
";

            var selectQuery = @"select ""Id"", ""Name"" from ""Users"" where ""Id"" = @userId";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(insertQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("userName", userName);
                    var userId = (int)command.ExecuteScalar();

                    using (var selectCommand = new NpgsqlCommand(selectQuery, connection) { CommandType = System.Data.CommandType.Text })
                    {
                        selectCommand.Parameters.AddWithValue("userId", userId);
                        using (var reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new UserDto
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                };
                            }
                        }
                    }
                }

                return null;
            }
        }

        public UserDto GetById(int userId)
        {
            var selectQuery = @"select ""Id"", ""Name"" from ""Users"" where ""Id"" = @userId";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(selectQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserDto
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                            };
                        }
                    }
                }

                return null;
            }
        }

        public UserDto GetByName(string userName)
        {
            var selectQuery = @"select ""Id"", ""Name"" from ""Users"" where ""Name"" = @userName";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(selectQuery, connection) { CommandType = System.Data.CommandType.Text })
                {
                    command.Parameters.AddWithValue("userName", userName);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserDto
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                            };
                        }
                    }
                }

                return null;
            }
        }
    }
}