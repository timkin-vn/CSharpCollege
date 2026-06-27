using System.Collections.Generic;
using System.Configuration;
using Npgsql;
using TwentyFortyEight.Common.Contracts.Repositories;
using TwentyFortyEight.Common.Dtos;

namespace TwentyFortyEight.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _dbConnectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

        public IEnumerable<UserDto> GetAll()
        {
            const string queryText = @"SELECT ""Id"", ""Name"" FROM ""Users""";

            using (var dbConnection = new NpgsqlConnection(_dbConnectionString))
            {
                dbConnection.Open();
                var profiles = new List<UserDto>();

                using (var dbCommand = new NpgsqlCommand(queryText, dbConnection))
                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var fetchedUser = new UserDto
                        {
                            Id = dataReader.GetInt32(0),
                            Name = dataReader.GetString(1)
                        };
                        profiles.Add(fetchedUser);
                    }
                }

                return profiles;
            }
        }

        public UserDto GetByName(string userName)
        {
            const string queryText = @"SELECT ""Id"", ""Name"" FROM ""Users"" WHERE ""Name"" = @userName";

            using (var dbConnection = new NpgsqlConnection(_dbConnectionString))
            {
                dbConnection.Open();

                using (var dbCommand = new NpgsqlCommand(queryText, dbConnection))
                {
                    dbCommand.Parameters.AddWithValue("userName", userName);

                    using (var dataReader = dbCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            return new UserDto
                            {
                                Id = dataReader.GetInt32(0),
                                Name = dataReader.GetString(1)
                            };
                        }
                    }
                }

                return null;
            }
        }

        public UserDto Create(string userName)
        {
            const string queryText = @"INSERT INTO ""Users"" (""Name"") VALUES (@userName) RETURNING ""Id""";

            using (var dbConnection = new NpgsqlConnection(_dbConnectionString))
            {
                dbConnection.Open();
                int generatedId;

                using (var dbCommand = new NpgsqlCommand(queryText, dbConnection))
                {
                    dbCommand.Parameters.AddWithValue("userName", userName);
                    generatedId = (int)dbCommand.ExecuteScalar();
                }

                return new UserDto
                {
                    Id = generatedId,
                    Name = userName
                };
            }
        }
    }
}