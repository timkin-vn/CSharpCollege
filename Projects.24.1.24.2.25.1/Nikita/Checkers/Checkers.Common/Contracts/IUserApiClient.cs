using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace Checkers.Common.Contracts
{
    public interface IUserApiClient
    {
        [Post("/api/register")]
        Task<bool> RegisterAsync(string username);

        
    }
}
