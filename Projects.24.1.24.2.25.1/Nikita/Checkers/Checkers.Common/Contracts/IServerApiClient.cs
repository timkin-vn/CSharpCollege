using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Common.Contracts
{
    public interface IServerApiClient
    {
        Task<object> RegisterUserAsync(string username);
    }
}
