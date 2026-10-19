using System;
using System.Configuration;
using System.Net.Http;

namespace FifteenGame.BusinessProxy.Infastructure
{
    internal static class HttpConnection
    {
        private static readonly Lazy<HttpClient> ClientFactory =
            new Lazy<HttpClient>(CreateClient);

        public static HttpClient HttpClient => ClientFactory.Value;

        private static HttpClient CreateClient()
        {
            string address =
                ConfigurationManager.AppSettings["ServerConnection"];

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ConfigurationErrorsException(
                    "В App.config не задан параметр ServerConnection.");
            }

            if (!Uri.TryCreate(address, UriKind.Absolute, out Uri baseAddress) ||
                (baseAddress.Scheme != Uri.UriSchemeHttp &&
                 baseAddress.Scheme != Uri.UriSchemeHttps))
            {
                throw new ConfigurationErrorsException(
                    "ServerConnection должен содержать корректный HTTP- или HTTPS-адрес.");
            }

            return new HttpClient
            {
                BaseAddress = baseAddress,
                Timeout = TimeSpan.FromSeconds(10)
            };
        }
    }
}