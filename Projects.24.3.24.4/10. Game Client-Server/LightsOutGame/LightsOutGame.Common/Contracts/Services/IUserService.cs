using LightsOutGame.Common.BusinessModels;
using System.Collections.Generic;

namespace LightsOutGame.Common.Contracts.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();

        UserModel GetOrCreateUser(string userName);

        UserModel GetUserByName(string userName);
    }
}
