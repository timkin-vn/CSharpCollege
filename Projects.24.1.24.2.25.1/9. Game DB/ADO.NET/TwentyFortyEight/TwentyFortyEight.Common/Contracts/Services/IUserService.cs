using System.Collections.Generic;
using TwentyFortyEight.Common.BusinessModels;

namespace TwentyFortyEight.Common.Contracts.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetOrCreateUser(string userName);
        UserModel GetUserByName(string userName);
    }
}
