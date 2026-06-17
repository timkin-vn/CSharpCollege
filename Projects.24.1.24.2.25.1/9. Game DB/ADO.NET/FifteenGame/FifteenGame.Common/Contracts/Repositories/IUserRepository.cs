using FifteenGame.Common.Dtos;
using System.Collections.Generic;

namespace FifteenGame.Common.Contracts.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserDto> GetAll();
        UserDto Create(string userName);
        UserDto GetById(int userId);
        UserDto GetByName(string userName);
    }
}