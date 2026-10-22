namespace Game2048.BusinessProxy.Infrastructure;

/// <summary>
/// Фабрика HttpClient (аналог HttpConnection из FifteenGame.BusinessProxy)
/// </summary>
internal static class HttpConnection
{
    private static readonly string _baseUrl =
        Environment.GetEnvironmentVariable("SERVER_URL") ?? "http://localhost:5000/";

    public static HttpClient CreateClient()
        => new() { BaseAddress = new Uri(_baseUrl) };
}
