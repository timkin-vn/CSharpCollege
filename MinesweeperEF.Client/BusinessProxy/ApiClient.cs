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

    public async Task<T> PostAsync<T>(string url, object body) =>
        await SendAsync<T>(() => _http.PostAsJsonAsync(url, body));

    public async Task<T> GetAsync<T>(string url) =>
        await SendAsync<T>(() => _http.GetAsync(url));

    private static async Task<T> SendAsync<T>(Func<Task<HttpResponseMessage>> call) {
        var response = await call();
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>()
               ?? throw new InvalidOperationException("Пустой ответ сервера");
    }
}
