using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Common.Contracts;
using Refit;
namespace Checkers.Common.Repositories
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IServerApiClient _serverApiClient;

        public UserApiClient(IServerApiClient serverApiClient)
        {
            _serverApiClient = serverApiClient;
        }
        [Post("/api/register")]
        public async Task<bool> RegisterAsync(string username)
        {
            var request = new
            {
                Username = username
            };
            var responce = await _serverApiClient.RegisterUserAsync(request.Username);
            return responce.Success;
        }

    }
}
