using System.Linq;
using FifteenGame.Data; 
using FifteenGame.Data.Entities;

namespace FifteenGame.Business.Services
{
    public class AuthService
    {
        private readonly MazeDbContext _context;

        public AuthService()
        {
            _context = new MazeDbContext(); 
        }

        public User Register(string login, string password)
        {

            var existingUser = _context.Users.FirstOrDefault(u => u.Username == login);
            if (existingUser != null)
            {
                return null; 
            }


            var newUser = new User
            {
                Username = login,
                Password = password
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        public User Login(string login, string password)
        {
            // Ищем пользователя по логину и паролю
            var user = _context.Users
                .FirstOrDefault(u => u.Username == login && u.Password == password);

            return user; 
        }
    }
}