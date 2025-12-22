using Pacman.BusinessProxy.Infrastructure;
using Pacman.Common.Dtos;
using Pacman.Common.Services;
using System.Net.Http.Json;
using System.Text.Json;

namespace Pacman.BusinessProxy.Services
{
    public class UserServiceProxy : IUserService
    {
        private readonly JsonSerializerOptions _options;

        public UserServiceProxy()
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public UserDto Login(string username)
        {
            
            var response = HttpConnection.Client.PostAsJsonAsync("api/user/login", username).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<UserDto>(json, _options);
        }

        public void UpdateScore(ScoreRequest request)
        {
            
            var response = HttpConnection.Client.PostAsJsonAsync("api/user/score", request).Result;
            
        }
    }
}