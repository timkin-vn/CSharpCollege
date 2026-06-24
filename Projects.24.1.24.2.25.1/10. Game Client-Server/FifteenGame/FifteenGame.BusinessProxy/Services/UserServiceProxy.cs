using FifteenGame.BusinessProxy.Infastructure;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace FifteenGame.BusinessProxy.Services
{
    public class UserServiceProxy : IUserService
    {
        public IEnumerable<UserModel> GetAllUsers()
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync("api/users/get-all").Result;
                response.EnsureSuccessStatusCode();

                var reply = JsonSerializer.Deserialize<UsersReply>(response.Content.ReadAsStringAsync().Result);
                return reply.Users.Select(FromDto);
            }
        }

        public UserModel GetOrCreateUser(string userName)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var httpContent = JsonContent.Create(new UserNameRequest { UserName = userName });
                var response = httpClient.PostAsync("api/users/get-or-create", httpContent).Result;
                response.EnsureSuccessStatusCode();

                var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStringAsync().Result);
                return FromDto(reply);
            }
        }

        public UserModel GetUserByName(string userName)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/users/get-by-name/{userName}").Result;
                response.EnsureSuccessStatusCode();

                var reply = JsonSerializer.Deserialize<UserReply>(response.Content.ReadAsStringAsync().Result);
                return FromDto(reply);
            }
        }

        private UserModel FromDto(UserReply reply)
        {
            if (reply == null) return null;
            return new UserModel { Id = reply.Id, Name = reply.Name };
        }
    }
}