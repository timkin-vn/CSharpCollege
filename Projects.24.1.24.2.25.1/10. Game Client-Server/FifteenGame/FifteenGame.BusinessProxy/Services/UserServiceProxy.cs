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
        private static readonly JsonSerializerOptions JsonOptions =
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

        public IEnumerable<UserModel> GetAllUsers()
        {
            using (var response = HttpConnection.HttpClient
                .GetAsync("api/users/get-all")
                .GetAwaiter().GetResult())
            {
                EnsureSuccess(response);

                string json = response.Content
                    .ReadAsStringAsync()
                    .GetAwaiter().GetResult();

                var reply = JsonSerializer.Deserialize<UsersReply>(
                    json, JsonOptions);

                if (reply?.Users == null)
                {
                    throw new InvalidOperationException(
                        "Сервер вернул некорректный список пользователей.");
                }

                return reply.Users.Select(FromDto).ToList();
            }
        }

        public UserModel GetOrCreateUser(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException(
                    "Введите имя пользователя.",
                    nameof(userName));

            var content = JsonContent.Create(
                new UserNameRequest
                {
                    UserName = userName.Trim()
                });

            using (content)
            using (var response = HttpConnection.HttpClient
                .PostAsync("api/users/get-or-create", content)
                .GetAwaiter().GetResult())
            {
                EnsureSuccess(response);
                return ReadUser(response);
            }
        }

        public UserModel GetUserByName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;

            string encodedName = Uri.EscapeDataString(userName.Trim());

            using (var response = HttpConnection.HttpClient
                .GetAsync($"api/users/get-by-name/{encodedName}")
                .GetAwaiter().GetResult())
            {
                EnsureSuccess(response);
                return ReadUser(response);
            }
        }

        private static UserModel ReadUser(HttpResponseMessage response)
        {
            string json = response.Content
                .ReadAsStringAsync()
                .GetAwaiter().GetResult();

            var reply = JsonSerializer.Deserialize<UserReply>(
                json, JsonOptions);

            return FromDto(reply);
        }

        private static UserModel FromDto(UserReply reply)
        {
            if (reply == null)
                return null;

            return new UserModel
            {
                Id = reply.Id,
                Name = reply.Name
            };
        }

        private static void EnsureSuccess(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            string details = response.Content == null
                ? string.Empty
                : response.Content.ReadAsStringAsync()
                    .GetAwaiter().GetResult();

            throw new HttpRequestException(
                $"Сервер вернул HTTP {(int)response.StatusCode} " +
                $"({response.ReasonPhrase}). {details}");
        }
        public void DeleteUser(int userId)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            using (var response = HttpConnection.HttpClient
                .DeleteAsync($"api/users/delete/{userId}")
                .GetAwaiter()
                .GetResult())
            {
                response.EnsureSuccessStatusCode();
            }
        }
    }
}