using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string ConnectionString =
            @"Server=localhost;Port=5432;Database=FifteenGame1Dev23.1.24.1;User Id=postgres;Password=Qwerty123;";

        public UserDto Create(string userName)
        {
            var insertQuery = @"
insert into ""Users"" (""Name"", ""BestScore"")
values (@userName, 0)
returning ""Id""
";

            var selectQuery = @"
select ""Id"", ""Name"", ""BestScore""
from ""Users""
where ""Id"" = @userId
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                int userId;

                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("userName", userName);
                    userId = (int)command.ExecuteScalar();
                }

                using (var command = new NpgsqlCommand(selectQuery, connection))
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
                                BestScore = reader.GetInt32(2)
                            };
                        }
                    }
                }

                return null;
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            var selectQuery = @"
select ""Id"", ""Name"", ""BestScore""
from ""Users""
order by ""Id""
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                var result = new List<UserDto>();

                using (var command = new NpgsqlCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new UserDto
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            BestScore = reader.GetInt32(2)
                        });
                    }
                }

                return result;
            }
        }

        public UserDto GetByName(string userName)
        {
            var selectQuery = @"
select ""Id"", ""Name"", ""BestScore""
from ""Users""
where ""Name"" = @userName
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(selectQuery, connection))
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
                                BestScore = reader.GetInt32(2)
                            };
                        }
                    }
                }

                return null;
            }
        }

        public void UpdateBestScore(int userId, int bestScore)
        {
            var updateQuery = @"
update ""Users""
set ""BestScore"" = @bestScore
where ""Id"" = @userId
";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("userId", userId);
                    command.Parameters.AddWithValue("bestScore", bestScore);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
