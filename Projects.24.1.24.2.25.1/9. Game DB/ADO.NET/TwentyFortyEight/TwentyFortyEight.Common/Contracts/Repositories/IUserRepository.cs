using System.Collections.Generic;
using TwentyFortyEight.Common.Dtos;

namespace TwentyFortyEight.Common.Contracts.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserDto> GetAll();
        UserDto GetByName(string name);
        UserDto Create(string name);
    }
}
