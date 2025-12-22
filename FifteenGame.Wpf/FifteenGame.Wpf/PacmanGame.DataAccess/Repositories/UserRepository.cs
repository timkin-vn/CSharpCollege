using PacmanGame.DataAccess.Entities;
using PacmanGame.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PacmanGame.DataAccess.Repositories
{
    public class UserRepository : Repository<GameUserEntity>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public GameUserEntity GetByUsername(string username)
        {
            return DbSet
                .Include(u => u.GameStates)
                .FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
        }

        public GameUserEntity GetByCredentials(string username, string password)
        {
            return DbSet
                .FirstOrDefault(u =>
                    u.Username.ToLower() == username.ToLower() &&
                    u.Password == password);
        }

        public IEnumerable<GameUserEntity> GetTopPlayers(int count)
        {
            return DbSet
                .Select(u => new
                {
                    User = u,
                    
                    MaxScore = u.GameStates.Any() ? u.GameStates.Max(gs => gs.Score) : 0
                })
                .OrderByDescending(x => x.MaxScore)
                .Take(count)
                .Select(x => x.User)
                .Include(u => u.GameStates)
                .ToList();
        }

        public bool UsernameExists(string username)
        {
            return DbSet.Any(u => u.Username.ToLower() == username.ToLower());
        }

        public IEnumerable<GameUserEntity> SearchUsers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return GetAll();

            return DbSet
                .Where(u => u.Username.ToLower().Contains(searchTerm.ToLower()))
                .OrderBy(u => u.Username)
                .ToList();
        }

        public int GetUserGameCount(int userId)
        {
            return DbSet
                
                .Where(u => u.Id == userId)
                .SelectMany(u => u.GameStates)
                .Count();
        }

        public int GetUserTotalScore(int userId)
        {
            return DbSet
                
                .Where(u => u.Id == userId)
                .SelectMany(u => u.GameStates)
                .Sum(gs => gs.Score);
        }

        public override GameUserEntity GetById(int id)
        {
            return DbSet
                .Include(u => u.GameStates)
                
                .FirstOrDefault(u => u.Id == id);
        }

        public override IEnumerable<GameUserEntity> GetAll()
        {
            return DbSet
                .Include(u => u.GameStates)
                .ToList();
        }
    }
}