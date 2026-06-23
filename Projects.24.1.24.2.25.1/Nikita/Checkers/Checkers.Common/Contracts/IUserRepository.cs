using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Business.Models;

namespace Checkers.Common.Contracts
{
    public interface IUserRepository
    {
        Task<bool> RegisterUserAsync(string username);
        Task<bool> ValidateUserAsync(string username);

        Task<UserSession> GetUserId(int userId);

        Task<bool> UserExistsAsync(string username);
    }
}
