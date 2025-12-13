using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MinesweeperEF.Client.BusinessProxy;

public sealed class ApiClient {
    public readonly HttpClient _http;

    public ApiClient(string baseUrl) {
        _http = new HttpClient {
            BaseAddress = new Uri(baseUrl)
        };
    }

    public void SetBearer(string token) {
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<T> PostAsync<T>(string url, object body) {
        var response = await _http.PostAsJsonAsync(url, body);
        response.EnsureSuccessStatusCode();

        return (await response.Content.ReadFromJsonAsync<T>())!;
    }
}
