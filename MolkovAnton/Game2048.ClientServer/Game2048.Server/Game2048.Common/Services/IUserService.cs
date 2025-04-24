using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Common.BusinessModels;

namespace Game2048.Common.Services
{
    public interface IUserService
    {
        UserModel GetByName(string userName);

        UserModel Create(string userName);

        IEnumerable<UserModel> GetAll();
    }
}
