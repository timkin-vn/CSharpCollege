using LightsOutGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.Common.Contracts.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();

        UserModel GetOrCreateUser(string userName);

        UserModel GetUserByName(string userName);
    }
}
