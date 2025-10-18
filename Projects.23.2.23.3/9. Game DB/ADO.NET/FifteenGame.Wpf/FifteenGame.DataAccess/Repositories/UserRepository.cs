using FifteenGame.Common.Dto;
using FifteenGame.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserDto Create(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserDto GetById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto GetByName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
