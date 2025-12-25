using Game2048.Common.Repositories;
using Npgsql;
using System;

namespace Game2048.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string ConnectionString =
            @"Server=localhost;Port=5432;Database=Game2048DB;User Id=postgres;Password=postgres;";

        public int CreateUser(string username)
        {
            var insertQuery = @"
INSERT INTO ""Users"" (""Username"")
VALUES (@username)
RETURNING ""Id""";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                
                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("username", username);

                    var userId = command.ExecuteScalar();
                    return Convert.ToInt32(userId);
                }
            }
        }

        public int GetUserIdByUsername(string username)
        {
            var selectQuery = @"SELECT ""Id"" FROM ""Users"" WHERE ""Username"" = @username";

            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                
                using (var command = new NpgsqlCommand(selectQuery, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("username", username);

                    var result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }
    }
}
