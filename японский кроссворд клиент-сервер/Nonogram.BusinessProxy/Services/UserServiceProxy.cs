using Nonogram.BusinessProxy.Infrastructure;
using Nonogram.Common.BusinessDtos;
using Nonogram.Common.BusinessModels;
using Nonogram.Common.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Nonogram.BusinessProxy.Services
{
    public class UserServiceProxy : IUserService
    {
        public IEnumerable<UserModel> GetAllUsers()
        {
            var response = HttpConnection.HttpClient.GetAsync("api/user/get-all").Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<AllUsersReply>(response.Content.ReadAsStringAsync().Result);
            return reply.Users.Select(u => new UserModel { Id = u.Id, Name = u.Name });
        }

        public UserModel GetOrCreateUser(string userName)
        {
            var httpContent = JsonContent.Create(new UserNameRequest { Name = userName });
            var response = HttpConnection.HttpClient.PostAsync("api/user/get-or-create", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStringAsync().Result);
            return new UserModel { Id = reply.Id, Name = reply.Name };
        }

        public UserModel GetUserByName(string userName)
        {
            var httpContent = JsonContent.Create(new UserNameRequest { Name = userName });
            var response = HttpConnection.HttpClient.PostAsync("api/user/get-by-name", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStringAsync().Result);
            return new UserModel { Id = reply.Id, Name = reply.Name };
        }
    }
}