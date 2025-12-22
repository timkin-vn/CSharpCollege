using Pacman.Common.Dtos;
using Pacman.Data.DataContext; 
using Pacman.Data.Entities;    
using System.Linq;

namespace Pacman.Data.Repositories
{
    public class UserRepository
    {
        public UserDto GetOrCreate(string username)
        {
            using (var db = new PacmanContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    user = new User { Username = username, HighScore = 0 };
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return new UserDto { Id = user.Id, Username = user.Username, HighScore = user.HighScore };
            }
        }

        public void UpdateScore(int userId, int score)
        {
            using (var db = new PacmanContext())
            {
                var user = db.Users.Find(userId);
                if (user != null && score > user.HighScore)
                {
                    user.HighScore = score;
                    db.SaveChanges();
                }
            }
        }
    }
}