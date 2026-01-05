using Nonogram.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Common.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetUserByName(string userName);
        UserModel GetOrCreateUser(string userName);
    }
}
