using System.Net.Http.Json;
using Game2048.BusinessProxy.Infrastructure;
using Game2048.Common.BusinessDtos;
using Game2048.Common.BusinessModels;
using Game2048.Common.Contracts.Services;

namespace Game2048.BusinessProxy.Services;

public class UserServiceProxy : IUserService
{
    public IEnumerable<UserModel> GetAllUsers()
    {
        using var http = HttpConnection.CreateClient();
        var reply = http.GetFromJsonAsync<UsersReply>("api/users/get-all").Result
                    ?? new UsersReply();
        return reply.Users.Select(u => new UserModel { Id = u.Id, Name = u.Name }).ToList();
    }

    public UserModel GetOrCreateUser(string userName)
    {
        using var http = HttpConnection.CreateClient();
        var response = http.PostAsJsonAsync("api/users/get-or-create",
                           new UserNameRequest(userName)).Result;
        response.EnsureSuccessStatusCode();
        var reply = response.Content.ReadFromJsonAsync<UserReply>().Result!;
        return new UserModel { Id = reply.Id, Name = reply.Name };
    }

    public UserModel? GetUserByName(string userName)
    {
        using var http = HttpConnection.CreateClient();
        var response = http.GetAsync($"api/users/get-by-name/{Uri.EscapeDataString(userName)}").Result;
        if (!response.IsSuccessStatusCode) return null;
        var reply = response.Content.ReadFromJsonAsync<UserReply>().Result;
        return reply == null ? null : new UserModel { Id = reply.Id, Name = reply.Name };
    }
}
