using _2048Game.Common.Dto;
using _2048Game.Common.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _2048Game.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
        public UserDto Create(string userName, string password)
        {
            var insertQuery = @"
            insert into ""Users"" (""Name"", ""Password"")
            values (@userName, @password)
            returning ""Id""
            ";

            var selectQuery = @"
            select
                ""Id"",
                ""Name"",
                ""Password""
            from ""Users""
            where
                ""Id"" = @userId
            ";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                int userId;

                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.Add("userName", NpgsqlTypes.NpgsqlDbType.Text).Value = userName ?? (object)DBNull.Value;
                    command.Parameters.Add("password", NpgsqlTypes.NpgsqlDbType.Text).Value = password ?? (object)DBNull.Value;

                    var insertResult = command.ExecuteScalar();
                    userId = (int)insertResult;
                }

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("userId", userId);

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var userDto = new UserDto();
                        userDto.Id = reader.GetInt32(0);
                        userDto.Name = reader.GetString(1);
                        userDto.Password = reader.GetString(2);
                        return userDto;
                    }
                    return null;
                }
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            var selectQuery = @"
            select
                ""Id"",
                ""Name"",
                ""Password""
            from ""Users""
            ";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                var result = new List<UserDto>();
                connection.Open();

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var userDto = new UserDto();
                        userDto.Id = reader.GetInt32(0);
                        userDto.Name = reader.GetString(1);
                        userDto.Password = reader.GetString(2);
                        result.Add(userDto);
                    }

                    return result;
                }
            }
        }

        public UserDto GetById(int userId)
        {
            var selectQuery = @"
            select
                ""Id"",
                ""Name"",
                ""Password""
            from ""Users""
            where
                ""Id"" = @userId
            ";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("userId", userId);

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var userDto = new UserDto();
                        userDto.Id = reader.GetInt32(0);
                        userDto.Name = reader.GetString(1);
                        userDto.Password = reader.GetString(2);
                        return userDto;
                    }

                    return null;
                }
            }
        }

        public UserDto GetByName(string userName, string password)
        {
            var selectQuery = @"
            select
                ""Id"",
                ""Name"",
                ""Password""
            from ""Users""
            where
                ""Name"" = @userName AND
                ""Password"" = @password
            ";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.Add("userName", NpgsqlTypes.NpgsqlDbType.Text).Value = userName ?? (object)DBNull.Value;
                    command.Parameters.Add("password", NpgsqlTypes.NpgsqlDbType.Text).Value = password ?? (object)DBNull.Value;

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var userDto = new UserDto();
                        userDto.Id = reader.GetInt32(0);
                        userDto.Name = reader.GetString(1);
                        userDto.Password = reader.GetString(2);
                        return userDto;
                    }

                    return null;
                }
            }
        }

        public UserDto GetByUserNameOnly(string userName)
        {
            var selectQuery = @"
            select ""Id"", ""Name"", ""Password""
            from ""Users""
            where ""Name"" = @userName
            ";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.Parameters.Add("userName", NpgsqlTypes.NpgsqlDbType.Text).Value = userName ?? (object)DBNull.Value;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserDto
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Password = reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}