using System.Collections.Generic;
using System.Configuration;
using Npgsql;
using TwentyFortyEight.Common.Contracts.Repositories;
using TwentyFortyEight.Common.Dtos;

namespace TwentyFortyEight.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        public IEnumerable<UserDto> GetAll()
        {
            const string sql = @"
select ""Id"", ""Name""
from ""Users""
";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                var result = new List<UserDto>();

                using (var cmd = new NpgsqlCommand(sql, connection)
                       { CommandType = System.Data.CommandType.Text })
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new UserDto
                        {
                            Id   = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }

                return result;
            }
        }

        public UserDto GetByName(string userName)
        {
            const string sql = @"
select ""Id"", ""Name""
from ""Users""
where ""Name"" = @userName
";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand(sql, connection)
                       { CommandType = System.Data.CommandType.Text })
                {
                    cmd.Parameters.AddWithValue("userName", userName);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return new UserDto { Id = reader.GetInt32(0), Name = reader.GetString(1) };
                    }
                }

                return null;
            }
        }

        public UserDto Create(string userName)
        {
            const string insertSql = @"
insert into ""Users"" (""Name"")
values (@userName)
returning ""Id""
";
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                int userId;

                using (var cmd = new NpgsqlCommand(insertSql, connection)
                       { CommandType = System.Data.CommandType.Text })
                {
                    cmd.Parameters.AddWithValue("userName", userName);
                    userId = (int)cmd.ExecuteScalar();
                }

                return new UserDto { Id = userId, Name = userName };
            }
        }
    }
}
