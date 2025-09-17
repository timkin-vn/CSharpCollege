using FifteenGame.ClientProxy.Infrastructure;
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

namespace FifteenGame.ClientProxy.Services
{
    public class UserServiceProxy : IUserService
    {
        public UserModel GetOrCreateUser(string userName)
        {
            var httpContent = JsonContent.Create(new UserNameRequest { UserName = userName });
            var response = HttpConnection.HttpClient.PostAsync("api/users/create", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStreamAsync().Result);
            return FromDto(reply);
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var response = HttpConnection.HttpClient.GetAsync("api/users/get-all").Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<AllUsersReply>(response.Content.ReadAsStreamAsync().Result);
            return reply.Users.Select(FromDto).ToList();
        }

        public UserModel GetUserByName(string userName)
        {
            var httpContent = JsonContent.Create(new UserNameRequest { UserName = userName });
            var response = HttpConnection.HttpClient.PostAsync("api/users/get-by-name", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStreamAsync().Result);
            return FromDto(reply);
        }

        private UserModel FromDto(UserReply dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new UserModel
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
    }
}
