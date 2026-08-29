using FifteenGame.Data;
using FifteenGame.Data.Entities;
using System;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class AuthService
    {
        public User Register(string username, string password)
        {
            using (var context = new GameDbContext())
            {
                if (context.Users.Any(u => u.Username == username))
                    throw new Exception("Пользователь уже существует.");

                var newUser = new User
                {
                    Username = username,
                    PasswordHash = password,
                    BestScore = 0
                };

                context.Users.Add(newUser);
                context.SaveChanges();
                return newUser;
            }
        }

        public User Login(string username, string password)
        {
            using (var context = new GameDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);

                if (user == null)
                    throw new Exception("Неверный логин или пароль.");

                return user;
            }
        }

        public void UpdateBestScore(int userId, int newScore)
        {
            using (var context = new GameDbContext())
            {
                var user = context.Users.Find(userId);
                if (user != null && newScore > user.BestScore)
                {
                    user.BestScore = newScore;
                    context.SaveChanges();
                }
            }
        }

        public User GetGlobalTopPlayer()
        {
            using (var context = new GameDbContext())
            {
                return context.Users.OrderByDescending(u => u.BestScore).FirstOrDefault();
            }
        }
    }
}