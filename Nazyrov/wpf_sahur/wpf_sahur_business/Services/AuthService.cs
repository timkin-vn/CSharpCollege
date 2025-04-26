using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Security.Cryptography;
using System.Text;

namespace wpf_sahur_business.Services
{
    public class AuthService
    {
        private readonly string _connectionString;

        public AuthService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AuthenticateUser(string username, string password)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT password_hash FROM users WHERE username = @username", conn);
            cmd.Parameters.AddWithValue("username", username);

            var dbPasswordHash = cmd.ExecuteScalar() as string;

            if (dbPasswordHash == null)
                return false;

            var inputPasswordHash = HashPassword(password);

            return dbPasswordHash == inputPasswordHash;
        }

        private string HashPassword(string password)
        {
            password = password.Trim();
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
