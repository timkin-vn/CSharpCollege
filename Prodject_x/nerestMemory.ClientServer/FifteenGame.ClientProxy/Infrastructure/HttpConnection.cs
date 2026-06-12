using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.ClientProxy.Infrastructure
{
    internal static class HttpConnection
    {
        public static readonly HttpClient HttpClient;

        static HttpConnection()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerAddress"]);
        }
    }
}
