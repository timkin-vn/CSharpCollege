using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{
    public class UserService : IUserService
    {
        public IEnumerable<UserModel> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserModel GetOrCreateUser(string userName)
        {
            throw new NotImplementedException();
        }

        public UserModel GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserModel GetUserByName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
