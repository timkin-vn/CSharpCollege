using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.BusinessProxy.Infrastructure
{
    internal static class HttpConnection
    {
        public static HttpClient HttpClient { get; }

        static HttpConnection()
        {
            HttpClient=new HttpClient();
            HttpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["serverConnection"]);
        }
    }
}
