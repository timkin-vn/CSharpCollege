using Minesweeper.BusinessProxy.Services;
using Minesweeper.Common.Dto;
using System.Threading.Tasks;

namespace Minesweeper.WpfClient.Services
{
    public class UserServiceProxy : HttpConnection, IUserServiceProxy
    {
        public async Task<UserResponse> GetOrCreateUser(string username)
        {
            var request = new { Username = username };
            var response = await PostAsync<ApiResponse<UserResponse>>("users/get-or-create", request);
            return response.Data;
        }

        public async Task<UserResponse> GetUser(int userId)
        {
            var response = await GetAsync<ApiResponse<UserResponse>>($"users/{userId}");
            return response.Data;
        }
    }
}