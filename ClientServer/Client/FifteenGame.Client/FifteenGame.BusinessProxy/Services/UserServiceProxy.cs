using FifteenGame.BusinessProxy.Infrastructure;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Services;
using System.Net.Http.Json;
using System.Text.Json;

namespace FifteenGame.BusinessProxy.Services
{
    public class UserServiceProxy : IUserService
    {
        private readonly JsonSerializerOptions _options;

        public UserServiceProxy()
        {
            
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public UserReply Login(UserNameRequest request)
        {
           
            var httpContent = JsonContent.Create(request);
            var response = HttpConnection.HttpClient.PostAsync("api/user/login", httpContent).Result;

            
            response.EnsureSuccessStatusCode();

            
            var json = response.Content.ReadAsStringAsync().Result;
            var reply = JsonSerializer.Deserialize<UserReply>(json, _options);

            return reply;
        }

        public void UpdateBestTime(Common.Dto.UserDto user)
        {
            var httpContent = JsonContent.Create(user);
            var response = HttpConnection.HttpClient.PostAsync("api/user/update-best-time", httpContent).Result;
            response.EnsureSuccessStatusCode();
            
        }
    }
}