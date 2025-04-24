using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Game2048.ClientProxy.Infrastructure;
using Game2048.Common.BusinessDtos;
using Game2048.Common.BusinessModels;
using Game2048.Common.Services;

namespace Game2048.ClientProxy.Services
{
    public class UserServiceProxy : IUserService
    {
        public UserModel Create(string userName)
        {
            var httpContent = JsonContent.Create(new UserNameRequest { UserName = userName });
            var response = HttpConnection.HttpClient.PostAsync("api/users/create", httpContent).Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStreamAsync().Result);
            return FromDto(reply);
        }

        public IEnumerable<UserModel> GetAll()
        {
            var response = HttpConnection.HttpClient.GetAsync("api/users/get-all").Result;
            response.EnsureSuccessStatusCode();

            var reply = JsonSerializer.Deserialize<AllUsersReply>(response.Content.ReadAsStreamAsync().Result);
            return reply.Users.Select(FromDto).ToList();
        }

        public UserModel GetByName(string userName)
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
