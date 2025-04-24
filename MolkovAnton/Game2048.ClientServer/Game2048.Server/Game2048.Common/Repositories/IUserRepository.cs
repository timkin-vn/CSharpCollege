using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Common.Dtos;

namespace Game2048.Common.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserDto> GetAll();

        UserDto GetByName(string userName);

        UserDto Create(string userName);
    }
}
