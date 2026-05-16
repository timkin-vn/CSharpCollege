using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FifteenGames.Common.Dtos;

namespace FifteenGames.DataAccess.Repositories
{
    public class UserRepository
    {
        private const string ConnectionString = @"Server=localhost;Port=5432;Database=FifteenGame;User Id=postgres;Password=2ylT8W08;";
        public IEnumerable<UserDto> GetAll()
        {
            using(var connection = new Npgsql.NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new Npgsql.NpgsqlCommand("select * from users", connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
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
    }
}
