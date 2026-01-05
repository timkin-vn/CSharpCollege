using Nonogram.Common.Dtos;
using Nonogram.Common.Repositories;
using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace Nonogram.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string ConnectionString =
            @"Server=localhost;Port=5432;Database=NonogramDb;User Id=postgres;Password=postgresql;";

        public UserDto Create(string userName)
        {
            var insertQuery = @"
insert into ""Users"" (""Name"")
values (@userName)
returning ""Id""
";

            var selectQuery = @"
select
    ""Id"",
    ""Name""
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
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("userName", userName);

                    var insertResult = command.ExecuteScalar();
                    userId = (int)insertResult;
                }

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("userId", userId);

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new UserDto
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                        };
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
    ""Name""
from ""Users""
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                var result = new List<UserDto>();

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = CommandType.Text;

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new UserDto
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                        });
                    }

                    return result;
                }
            }
        }

        public UserDto GetByName(string userName)
        {
            var selectQuery = @"
select
    ""Id"",
    ""Name""
from ""Users""
where
    ""Name"" = @userName
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("userName", userName);

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new UserDto
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                        };
                    }

                    return null;
                }
            }
        }
    }
}