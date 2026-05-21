using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FifteenGames.Common.Dtos;
using Npgsql;

namespace FifteenGames.DataAccess.Repositories
{
    public class UserRepository
    {
        private readonly string ConnectionString = ConfigurationManager.ConnectionStrings["main"].ConnectionString;
        public IEnumerable<UserDto> GetAll()
        {
            using (var connection = new Npgsql.NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new Npgsql.NpgsqlCommand("select * from users", connection)
                {
                    CommandType = System.Data.CommandType.Text
                })
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new UserDto
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                        }
                    }
                }
            }
        }
        public UserDto Create(string name)
        {
            var insertQueue = @"insert into users (name) values (@name) returning id";
            var selectUsers = "select * from users";
            using (var connection = new NpgsqlConnection(insertQueue))
            {
                connection.Open();
                int userId;
                using (var command = new Npgsql.NpgsqlCommand("select * from users", connection)
                {
                    CommandType = System.Data.CommandType.Text
                })
                {
                    command.Parameters.AddWithValue("name", name);
                    var insertResult = command.ExecuteScalar();
                    userId = (int)insertResult;
                }
                using (var command = new Npgsql.NpgsqlCommand(selectUsers, connection)
                {
                    CommandType = System.Data.CommandType.Text
                })
                {
                    command.Parameters.AddWithValue("userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserDto
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                        }

                        return null;
                    }
                }
            }
        }
        public UserDto GetById(int userId)
        {
            var selectUser = @"select * from users where id = @userId";
            using (var connection = new Npgsql.NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new Npgsql.NpgsqlCommand(selectUser, connection)
                {
                    CommandType = System.Data.CommandType.Text
                })
                {
                    command.Parameters.AddWithValue("userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserDto
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                        }
                        return null;
                    }
                }
            }
        }
        public UserDto GetByName(string name)
        {
            var selectUser = @"select * from users where name = @name";
            using (var connection = new Npgsql.NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new Npgsql.NpgsqlCommand(selectUser, connection)
                {
                    CommandType = System.Data.CommandType.Text
                })
                {
                    command.Parameters.AddWithValue("name", name);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserDto
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                        }
                        return null;
                    }
                }
            }
        }
    }
}
