using PacmanGame.DataAccess.Entities;
using System.Collections.Generic;

namespace PacmanGame.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<GameUserEntity>
    {
        GameUserEntity GetByUsername(string username);
        GameUserEntity GetByCredentials(string username, string password);
        IEnumerable<GameUserEntity> GetTopPlayers(int count);
        bool UsernameExists(string username);
        IEnumerable<GameUserEntity> SearchUsers(string searchTerm);
        int GetUserGameCount(int userId);
        int GetUserTotalScore(int userId);
    }
}