using LightsOutGame.Common.Dtos;
using System.Collections.Generic;

namespace LightsOutGame.Common.Contracts.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserDto> GetAll();

        UserDto Create(string userName);

        UserDto GetByName(string userName);

        UserDto GetById(int userId);
    }
}
