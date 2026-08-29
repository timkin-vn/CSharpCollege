using FifteenGame.Common.BusinessDtos;
using FifteenGame.DataAccess.DataContext;
using FifteenGame.DataAccess.Entities;
using System.Linq;

namespace FifteenGame.DataAccess.Repositories
{
    public class UserRepository
    {
        public UserDto Create(string username, string password)
        {
            using (var db = new GameDbContext())
            {
                if (db.Users.Any(u => u.Username == username))
                    return null;

                var user = new UserEntity
                {
                    Username = username,
                    Password = password,
                    BestScore = 0
                };

                db.Users.Add(user);
                db.SaveChanges();

                return new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    BestScore = user.BestScore
                };
            }
        }

        public UserDto GetByLogin(string username, string password)
        {
            using (var db = new GameDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user == null)
                    return null;

                return new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    BestScore = user.BestScore
                };
            }
        }

        public UserDto GetTopPlayer()
        {
            using (var db = new GameDbContext())
            {
                var user = db.Users.OrderByDescending(u => u.BestScore).FirstOrDefault();

                if (user == null)
                    return null;

                return new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    BestScore = user.BestScore
                };
            }
        }

        public void UpdateScore(int userId, int newScore)
        {
            using (var db = new GameDbContext())
            {
                var user = db.Users.Find(userId);
                if (user != null && newScore > user.BestScore)
                {
                    user.BestScore = newScore;
                    db.SaveChanges();
                }
            }
        }
    }
}