using _2048Game.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game.Common.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserDto> GetAll();

        UserDto GetByName(string userName, string password);

        UserDto GetById(int userId);

        UserDto Create(string userName, string password);

        UserDto GetByUserNameOnly(string userName);
    }
}
