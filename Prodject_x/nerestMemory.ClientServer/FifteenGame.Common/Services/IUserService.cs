using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.Services
{
    public interface IUserService
    {

        IEnumerable<UserModel> GetAllUsers();

        UserModel GetUserByName(string userName);

        UserModel GetOrCreateUser(string userName);

    }
}
