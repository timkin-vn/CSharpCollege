using FifteenGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.Contracts.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();

        UserModel GetOrCreateUser(string userName);

        UserModel GetUserByName(string userName);
    }
}
