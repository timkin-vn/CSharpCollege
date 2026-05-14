using System;
using System.Net.Http;

namespace Minesweeper.WpfClient.Services
{
    public partial class HttpConnection : IDisposable
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _baseUrl;

        public HttpConnection() : this("https://localhost:44359/api/")
        {
        }

        public HttpConnection(string baseUrl)
        {
            _baseUrl = baseUrl;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl),
                Timeout = TimeSpan.FromSeconds(30)
            };

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}