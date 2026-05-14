using Minesweeper.Common.Dto;
using System.Collections.Generic;

namespace Minesweeper.Common.Repositories
{
    public interface IUserRepository
    {
        UserDto GetById(int userId);
        UserDto GetByUsername(string username);
        UserDto Create(UserDto user);
        UserDto Update(UserDto user);

        IEnumerable<UserDto> GetAll();
        IEnumerable<UserDto> GetTopPlayers(int limit = 10);

        void UpdateStats(int userId, bool gameWon);
        void UpdateLastPlayed(int userId);
    }
}