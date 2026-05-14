using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

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

    public async Task PostAsync(string url, object body) {
        var response = await _http.PostAsJsonAsync(url, body);
        await EnsureSuccessAsync(response);
    }

    public async Task<T> GetAsync<T>(string url) =>
        await SendAsync<T>(() => _http.GetAsync(url));

    private static async Task<T> SendAsync<T>(Func<Task<HttpResponseMessage>> call) {
        var response = await call();
        await EnsureSuccessAsync(response);
        
        var content = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(content)) {
            throw new InvalidOperationException("Пустой ответ сервера");
        }

        try {
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { 
                PropertyNameCaseInsensitive = true 
            }) ?? throw new InvalidOperationException("Не удалось десериализовать ответ");
        }
        catch (JsonException ex) {
            throw new InvalidOperationException($"Ошибка парсинга JSON: {content}", ex);
        }
    }

    private static async Task EnsureSuccessAsync(HttpResponseMessage response) {
        if (!response.IsSuccessStatusCode) {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Ошибка API ({response.StatusCode}): {error}"
            );
        }
    }
}