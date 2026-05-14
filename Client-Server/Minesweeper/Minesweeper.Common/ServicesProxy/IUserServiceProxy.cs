using Minesweeper.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.BusinessProxy.Services
{
    public interface IUserServiceProxy
    {
        Task<UserResponse> GetOrCreateUser(string username);
        Task<UserResponse> GetUser(int userId);
    }
}
