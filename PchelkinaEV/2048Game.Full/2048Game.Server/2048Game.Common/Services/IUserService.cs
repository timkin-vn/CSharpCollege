using _2048Game.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game.Common.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetUserByNameAndPassword(string userName, string password);
        UserModel GetUserById(int userId);
        UserModel GetOrCreateUser(string userName, string password);
        UserModel GetByUserNameOnly(string userName);
    }
}
