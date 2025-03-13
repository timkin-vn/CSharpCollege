using Npgsql;
using System;
using System.Security.Cryptography;
using System.Text;

namespace MemoryGame
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool RegisterUser(string username, string password)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();

                    // Проверяем, существует ли пользователь с таким именем
                    string checkQuery = "SELECT COUNT(*) FROM \"user\" WHERE name = @username"; // Escaping user может быть необходимым
                    using (NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@username", username);
                        long userCount = (long)checkCommand.ExecuteScalar();

                        if (userCount > 0)
                        {
                            // Пользователь с таким именем уже существует
                            return false;
                        }
                    }

                    // Хешируем пароль
                    string hashedPassword = HashPassword(password);

                    // Вставляем пользователя
                    string insertQuery = "INSERT INTO \"user\" (name, password) VALUES (@username, @password)"; // Escaping user может быть необходимым
                    Console.WriteLine($"SQL Query: {insertQuery}");
                    using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", hashedPassword);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок подключения/запроса к БД
                Console.WriteLine($"Ошибка регистрации пользователя: {ex.Message}");
                return false;
            }
        }

        public string AuthenticateUser(string username, string password)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();

                    // Выбираем хешированный пароль
                    string query = "SELECT password FROM \"user\" WHERE name = @username"; // Escaping user может быть необходимым

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            string hashedPasswordFromDb = result.ToString();
                            string hashedPasswordAttempt = HashPassword(password);  // Хешируем введенный пароль

                            // Сравниваем хешированные пароли
                            if (hashedPasswordFromDb.Equals(hashedPasswordAttempt, StringComparison.OrdinalIgnoreCase))
                            {
                                return username; // Аутентификация успешна, возвращаем имя пользователя
                            }
                            else
                            {
                                return null; // Пароль не совпадает
                            }
                        }
                        else
                        {
                            return null; // Пользователь не найден
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка аутентификации пользователя: {ex.Message}");
                return null;
            }
        }

        // Метод для хеширования пароля
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - возвращает массив байтов
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Преобразуем массив байтов в строку
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // "x2" преобразует в шестнадцатеричный формат
                }
                return builder.ToString();
            }
        }
    }
}
