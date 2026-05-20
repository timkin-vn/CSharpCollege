using FifteenGame.BusinessProxy.Infrastructure;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Services;
using System.Net.Http.Json;

namespace FifteenGame.BusinessProxy.Services
{
    public class UserServiceProxy : IUserService
    {
        public UserDto Login(string u, string p)
        {
            var res = HttpConnection.Client.PostAsJsonAsync("api/user/login", new LoginRequest { Username = u, Password = p }).Result;
            if (!res.IsSuccessStatusCode) return null;
            return res.Content.ReadFromJsonAsync<UserDto>().Result;
        }

        public UserDto Register(string u, string p)
        {
            var res = HttpConnection.Client.PostAsJsonAsync("api/user/register", new LoginRequest { Username = u, Password = p }).Result;
            if (!res.IsSuccessStatusCode) return null;
            return res.Content.ReadFromJsonAsync<UserDto>().Result;
        }

        public UserDto GetGlobalTopPlayer()
        {
            return HttpConnection.Client.GetFromJsonAsync<UserDto>("api/user/top").Result;
        }
    }
}