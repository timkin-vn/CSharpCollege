using Minesweeper.Common.BusinessDtos;
using Minesweeper.Common.BusinessModels;
using System.Collections.Generic;

namespace Minesweeper.Common.Services
{
    public interface IUserService
    {
        UserResponse GetOrCreateUser(string username);
        UserResponse GetUser(int userId);
        UserResponse GetUserByUsername(string username);
        IEnumerable<UserResponse> GetAllUsers();
        IEnumerable<UserResponse> GetTopPlayers(int limit = 10);

        void UpdateUserStats(int userId, bool gameWon);

        bool ValidateUsername(string username);
    }
}