using System.Security.Cryptography;
using System.Text;
using gg_web_business.Data;  
using gg_web_business.Models;
using Microsoft.EntityFrameworkCore;

namespace gg_web_business.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (existingUser != null) return false;

            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password)
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            var passwordHash = HashPassword(password);
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == passwordHash);
        }

        private string HashPassword(string password)
        {
            password = password.Trim();

            using var sha256 = System.Security.Cryptography.SHA256.Create();

            var bytes = System.Text.Encoding.UTF8.GetBytes(password);

            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
