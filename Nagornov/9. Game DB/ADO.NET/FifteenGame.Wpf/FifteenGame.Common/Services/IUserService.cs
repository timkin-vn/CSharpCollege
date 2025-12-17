using FifteenGame.Common.BusinessModels;
using System.Collections.Generic;

namespace FifteenGame.Common.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetUserByName(string userName);
        UserModel GetOrCreateUser(string userName);
    }
}