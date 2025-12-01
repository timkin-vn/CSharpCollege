using FifteenGame.BusinessProxy.Infrastructure;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FifteenGame.BusinessProxy.Services
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
            var httpContent = JsonContent.Create(new UserNameRequest { UserName = userName, });
            var response = HttpConnection.HttpClient.PostAsync("api/user/get-or-create", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStringAsync().Result);
            return FromDto(reply);
        }

        public UserModel GetUserById(int userId)
        {
            var httpContent = JsonContent.Create(new UserIdRequest { UserId = userId, });
            var response = HttpConnection.HttpClient.PostAsync("api/user/get-by-id", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStringAsync().Result);
            return FromDto(reply);
        }

        public UserModel GetUserByName(string userName)
        {
            var httpContent = JsonContent.Create(new UserNameRequest { UserName = userName, });
            var response = HttpConnection.HttpClient.PostAsync("api/user/get-by-name", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStringAsync().Result);
            return FromDto(reply);
        }

        private UserModel FromDto(UserReply user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.Id, Name = user.Name, };
        }
    }
}
