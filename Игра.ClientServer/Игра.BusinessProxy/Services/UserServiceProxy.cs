using Игра.BusinessProxy.Infrastructure;
using Игра.Common.BusinessDtos;
using Игра.Common.BusinessModels;
using Игра.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Игра.BusinessProxy.Services
{
    public class UserServiceProxy : IUserService
    {
        public IEnumerable<UserModel> GetAllUsers()
        {
            var response = HttpConnection.HttpClient.GetAsync("api/user/get-all").Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<AllUsersReply>(response.Content.ReadAsStringAsync().Result);
            return reply.Users.Select(FromDto);
        }

        public UserModel GetOrCreateUser(string userName)
        {
            var httpContent = JsonContent.Create(new UserNameRequest { Name = userName, });
            var response = HttpConnection.HttpClient.PostAsync("api/user/get-or-create", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStringAsync().Result);
            return FromDto(reply);
        }

        public UserModel GetUserByName(string userName)
        {
            var httpContent = JsonContent.Create(new UserNameRequest { Name = userName, });
            var response = HttpConnection.HttpClient.PostAsync("api/user/get-by-name", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStringAsync().Result);
            return FromDto(reply);
        }

        private UserModel FromDto(UserReply reply)
        {
            if (reply == null)
            {
                return null;
            }

            return new UserModel { Id = reply.Id, Name = reply.Name, };
        }
    }
}
