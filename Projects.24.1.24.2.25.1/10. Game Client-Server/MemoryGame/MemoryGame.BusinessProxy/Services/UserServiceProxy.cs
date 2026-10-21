using MemoryGame.BusinessProxy.Infastructure;
using MemoryGame.Common.BusinessDtos;
using MemoryGame.Common.BusinessModels;
using MemoryGame.Common.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MemoryGame.BusinessProxy.Services
{
    public class UserServiceProxy : IUserService
    {
        public IEnumerable<UserModel> GetAllUsers()
        {
            return Task.Run(() => GetAllUsersAsync()).Result;
        }

        private async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = await httpClient.GetAsync("api/users/get-all").ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var reply = JsonSerializer.Deserialize<UsersReply>(json);
                return reply.Users.Select(FromDto);
            }
        }

        public UserModel GetOrCreateUser(string userName)
        {
            return Task.Run(() => GetOrCreateUserAsync(userName)).Result;
        }

        private async Task<UserModel> GetOrCreateUserAsync(string userName)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var json = JsonSerializer.Serialize(new UserNameRequest { UserName = userName });
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("api/users/get-or-create", httpContent).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var replyJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var reply = JsonSerializer.Deserialize<UserReply>(replyJson);
                return FromDto(reply);
            }
        }

        public UserModel GetUserByName(string userName)
        {
            return Task.Run(() => GetUserByNameAsync(userName)).Result;
        }

        private async Task<UserModel> GetUserByNameAsync(string userName)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = await httpClient.GetAsync($"api/users/get-by-name/{userName}").ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var reply = JsonSerializer.Deserialize<UserReply>(json);
                return FromDto(reply);
            }
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
