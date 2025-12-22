using System;
using System.Configuration;
using System.Net.Http;

namespace Pacman.BusinessProxy.Infrastructure
{
    internal static class HttpConnection
    {
        public static HttpClient Client { get; }

        static HttpConnection()
        {
            
            string url = ConfigurationManager.AppSettings["serverConnection"];

            if (string.IsNullOrEmpty(url))
                url = "https://localhost:44317/"; 

            var handler = new HttpClientHandler
            {
                
                ServerCertificateCustomValidationCallback = (s, c, ch, e) => true
            };

            Client = new HttpClient(handler) { BaseAddress = new Uri(url) };
        }
    }
}