using Nonogram.BusinessProxy.Infrastructure;
using Nonogram.Common.BusinessDtos;
using Nonogram.Common.BusinessModels;
using Nonogram.Common.Services;
using Newtonsoft.Json; // Используем Newtonsoft.Json
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Nonogram.BusinessProxy.Services
{
    public class UserServiceProxy : IUserService
    {
        public IEnumerable<UserModel> GetAllUsers()
        {
            Console.WriteLine("=== UserServiceProxy.GetAllUsers() ===");

            try
            {
                var response = HttpConnection.HttpClient.GetAsync("api/user/get-all").Result;
                Console.WriteLine($"Response Status: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.ReasonPhrase}");
                    return Enumerable.Empty<UserModel>();
                }

                var responseString = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Response: {responseString}");

                var reply = JsonConvert.DeserializeObject<AllUsersReply>(responseString);

                if (reply?.Users == null)
                {
                    Console.WriteLine("ERROR: Users list is null or empty");
                    return Enumerable.Empty<UserModel>();
                }

                Console.WriteLine($"SUCCESS: Retrieved {reply.Users.Count} users");
                return reply.Users.Select(u => new UserModel { Id = u.Id, Name = u.Name });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXCEPTION in GetAllUsers: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return Enumerable.Empty<UserModel>();
            }
        }

        public UserModel GetOrCreateUser(string userName)
        {
            Console.WriteLine($"=== UserServiceProxy.GetOrCreateUser({userName}) ===");

            try
            {
                var request = new UserNameRequest { Name = userName };
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine($"Sending request to: api/user/get-or-create");
                Console.WriteLine($"Request body: {json}");

                var response = HttpConnection.HttpClient.PostAsync("api/user/get-or-create", content).Result;
                Console.WriteLine($"Response Status: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"Error: {error}");
                    return null;
                }

                var responseString = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Response: {responseString}");

                if (string.IsNullOrEmpty(responseString))
                {
                    Console.WriteLine("ERROR: Response is empty.");
                    return null;
                }

                var reply = JsonConvert.DeserializeObject<UserReply>(responseString);

                if (reply == null)
                {
                    Console.WriteLine("ERROR: Failed to deserialize UserReply.");
                    return null;
                }

                Console.WriteLine($"SUCCESS: User Id={reply.Id}, Name={reply.Name}");
                return new UserModel { Id = reply.Id, Name = reply.Name };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXCEPTION in GetOrCreateUser: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public UserModel GetUserByName(string userName)
        {
            Console.WriteLine($"=== UserServiceProxy.GetUserByName({userName}) ===");

            try
            {
                var request = new UserNameRequest { Name = userName };
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine($"Sending request to: api/user/get-by-name");
                Console.WriteLine($"Request body: {json}");

                var response = HttpConnection.HttpClient.PostAsync("api/user/get-by-name", content).Result;
                Console.WriteLine($"Response Status: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"Error: {error}");
                    return null;
                }

                var responseString = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Response: {responseString}");

                if (string.IsNullOrEmpty(responseString))
                {
                    Console.WriteLine("ERROR: Response is empty.");
                    return null;
                }

                var reply = JsonConvert.DeserializeObject<UserReply>(responseString);

                if (reply == null)
                {
                    Console.WriteLine("ERROR: Failed to deserialize UserReply.");
                    return null;
                }

                Console.WriteLine($"SUCCESS: User Id={reply.Id}, Name={reply.Name}");
                return new UserModel { Id = reply.Id, Name = reply.Name };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXCEPTION in GetUserByName: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
    }
}