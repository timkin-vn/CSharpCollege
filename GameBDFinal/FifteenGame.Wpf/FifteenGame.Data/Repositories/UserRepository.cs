using FifteenGame.Data.Entities;
using System.Linq;

namespace FifteenGame.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User GetOrCreate(string username)
        {
            using (var db = new GameDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    user = new User
                    {
                        Username = username,
                        BestTimeSeconds = null,
                        SavedGameJson = null
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return user;
            }
        }

        public void UpdateBestTime(string username, double timeInSeconds)
        {
            using (var db = new GameDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    
                    if (user.BestTimeSeconds == null || timeInSeconds < user.BestTimeSeconds)
                    {
                        user.BestTimeSeconds = timeInSeconds;
                        db.SaveChanges();
                    }
                }
            }
        }

        public void SaveGameState(string username, string json)
        {
            using (var db = new GameDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    user.SavedGameJson = json;
                    db.SaveChanges();
                }
            }
        }

        public void ClearSavedGame(string username)
        {
            using (var db = new GameDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    user.SavedGameJson = null;
                    db.SaveChanges();
                }
            }
        }
    }
}