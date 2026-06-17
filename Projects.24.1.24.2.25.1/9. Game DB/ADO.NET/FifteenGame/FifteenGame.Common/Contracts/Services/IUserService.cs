using FifteenGame.Common.BusinessModels;
using System.Collections.Generic;

namespace FifteenGame.Common.Contracts.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetOrCreateUser(string userName);
        UserModel GetUserByName(string userName);
    }
}