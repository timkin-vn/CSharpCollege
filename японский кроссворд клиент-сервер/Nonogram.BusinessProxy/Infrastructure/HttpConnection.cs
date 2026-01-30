using System;
using System.Net.Http;

namespace Nonogram.BusinessProxy.Infrastructure
{
    internal static class HttpConnection
    {
        public static HttpClient HttpClient { get; }
        static HttpConnection()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5001/"), // Изменили на 5001
                Timeout = TimeSpan.FromSeconds(30)
            };

            // Добавьте заголовки для правильной работы с JSON
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine($"HttpClient initialized with BaseAddress: {HttpClient.BaseAddress}");
        }
    }
}