using System;
using System.Net.Http;

namespace FifteenGame.BusinessProxy.Infrastructure
{
    internal static class HttpConnection
    {
        public static HttpClient Client { get; } = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44362/")
        };
    }
}