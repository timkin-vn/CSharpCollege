using _2048Game.BusinessProxy.Infractructure;
using _2048Game.Common.BusinessDtos;
using _2048Game.Common.BusinessModels;
using _2048Game.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _2048Game.BusinessProxy.Services
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

        public UserModel GetByUserNameOnly(string userName)
        {
            var httpContent = JsonContent.Create(new UserAndPasswordRequest { UserName = userName });
            var response = HttpConnection.HttpClient.PostAsync("api/user/get-by-username-only", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStringAsync().Result);
            return FromDto(reply);
        }

        public UserModel GetOrCreateUser(string userName, string password)
        {
            var httpContent = JsonContent.Create(new UserAndPasswordRequest { UserName = userName, Password = password });
            var response = HttpConnection.HttpClient.PostAsync("api/user/get-or-create", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStringAsync().Result);
            return FromDto(reply);
        }

        public UserModel GetUserById(int userId)
        {
            var httpContent = JsonContent.Create(new UserIdRequest { UserId = userId });
            var response = HttpConnection.HttpClient.PostAsync("api/user/get-by-id", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStringAsync().Result);
            return FromDto(reply);
        }

        public UserModel GetUserByName(string userName, string password)
        {
            return GetUserByNameAndPassword(userName, password);
        }

        public UserModel GetUserByNameAndPassword(string userName, string password)
        {
            var httpContent = JsonContent.Create(new UserAndPasswordRequest { UserName = userName, Password = password });
            var response = HttpConnection.HttpClient.PostAsync("api/user/get-by-name-password", httpContent).Result;
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

            return new UserModel 
            { 
                Id = user.Id, 
                Name = user.Name,
                Password = user.Password,
            };
        }
    }
}
