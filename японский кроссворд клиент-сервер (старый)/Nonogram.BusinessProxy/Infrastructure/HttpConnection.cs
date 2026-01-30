using System;
using System.Configuration;
using System.Net.Http;

namespace Nonogram.BusinessProxy.Infrastructure
{
    internal static class HttpConnection
    {
        public static HttpClient HttpClient { get; }

        static HttpConnection()
        {
            HttpClient = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerConnection"]) };
        }
    }
}