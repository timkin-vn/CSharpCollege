using System;
using System.Configuration;
using System.Net.Http;

namespace FifteenGame.BusinessProxy.Infrastructure
{
    internal static class HttpConnection
    {
        public static HttpClient HttpClient { get; }

        static HttpConnection()
        {
            string url = ConfigurationManager.AppSettings["serverConnection"];

            if (string.IsNullOrEmpty(url))
                throw new Exception("Ключ serverConnection не найден в App.config");

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            HttpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(url),
                Timeout = TimeSpan.FromSeconds(10)
            };
        }
    }
}
