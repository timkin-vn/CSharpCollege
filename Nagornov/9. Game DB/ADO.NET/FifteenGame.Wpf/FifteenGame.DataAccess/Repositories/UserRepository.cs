using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = "Host=localhost;Port=5432;Database=fifteengame;Username=postgres;Password=sOQA1337";
            Console.WriteLine($"UserRepository: Строка подключения: {_connectionString}");
        }

        public bool UserExists(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT COUNT(*) FROM users WHERE name = @name";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("name", userName);
                        var count = Convert.ToInt64(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка проверки пользователя: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            var result = new List<UserDto>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT id, name FROM users ORDER BY name";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new UserDto
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Name = reader["name"].ToString()
                            });
                        }
                    }
                }
                Console.WriteLine($"Загружено {result.Count} пользователей из PostgreSQL");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки пользователей: {ex.Message}");
            }

            return result;
        }

        public UserDto GetByName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;

            Console.WriteLine($"Поиск пользователя по имени: '{userName}'");

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT id, name FROM users WHERE name = @name";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("name", userName);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var user = new UserDto
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Name = reader["name"].ToString()
                                };
                                Console.WriteLine($"Пользователь '{userName}' найден в PostgreSQL. ID: {user.Id}");
                                return user;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка поиска в PostgreSQL: {ex.Message}");
            }

            Console.WriteLine($"Пользователь '{userName}' не найден");
            return null;
        }

        public UserDto Create(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Имя пользователя не может быть пустым");

            Console.WriteLine($"Создание нового пользователя: '{userName}'");

            if (UserExists(userName))
                throw new InvalidOperationException($"Пользователь '{userName}' уже существует");

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "INSERT INTO users (name) VALUES (@name) RETURNING id";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("name", userName);

                        var id = Convert.ToInt32(cmd.ExecuteScalar());
                        var user = new UserDto
                        {
                            Id = id,
                            Name = userName
                        };

                        Console.WriteLine($"Пользователь '{userName}' создан в PostgreSQL. ID: {id}");
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка создания пользователя: {ex.Message}");
                throw new Exception($"Не удалось создать пользователя '{userName}': {ex.Message}");
            }
        }
    }
}