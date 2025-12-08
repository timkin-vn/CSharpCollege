using Игра.Common.BusinessModels;
using System.Collections.Generic;

namespace Игра.Common.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();

        UserModel GetUserByName(string userName);

        UserModel GetOrCreateUser(string userName);
    }
}