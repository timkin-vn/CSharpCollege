using Игра.Common.Dtos;
using System.Collections.Generic;

namespace Игра.Common.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserDto> GetAll();

        UserDto GetByName(string userName);

        UserDto Create(string userName);
    }
}