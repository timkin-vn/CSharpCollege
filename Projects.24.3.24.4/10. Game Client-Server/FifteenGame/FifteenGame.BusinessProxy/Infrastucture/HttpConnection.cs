using System;
using System.Configuration;
using System.Net.Http;

namespace FifteenGame.BusinessProxy.Infrastucture
{
    internal static class HttpConnection
    {
        public static HttpClient HttpClient =>
            new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerConnection"]) };
    }
}