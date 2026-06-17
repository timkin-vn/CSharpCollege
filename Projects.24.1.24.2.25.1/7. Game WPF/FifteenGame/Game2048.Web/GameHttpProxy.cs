using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Game2048.Business.Models;

namespace Game2048.Web.Services;

public class GameHttpProxy
{
    private readonly HttpClient _httpClient;

    public GameHttpProxy(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GameModel> RestartGameAsync()
    {
        var response = await _httpClient.PostAsync("api/game/restart", null);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<GameModel>())!;
    }

    public async Task<GameModel> ExecuteMoveAsync(MoveDirection direction)
    {
        var response = await _httpClient.PostAsync($"api/game/move?direction={direction}", null);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        return System.Text.Json.JsonSerializer.Deserialize<GameModel>(responseString, new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}
