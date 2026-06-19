using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.BusinessProxy.Infrastucture
{
    internal static class HttpConnection
    {
        public static HttpClient HttpClient => 
            new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["ServerConnection"]) };
    }
}
