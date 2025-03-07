using FifteenGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.Services
{
    public interface IUserService
    {
        UserModel GetByName(string userName);

        UserModel Create(string userName);

        IEnumerable<UserModel> GetAll();
    }
}
