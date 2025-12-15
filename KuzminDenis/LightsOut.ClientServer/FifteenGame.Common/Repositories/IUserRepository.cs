using FifteenGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserDto> GetAll();
        UserDto GetByName(string userName);
        UserDto Create(string userName);
    }
}