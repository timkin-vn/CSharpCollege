using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Minesweeper.BusinessProxy
{

    public abstract class ApiClient
    {
        protected readonly HttpClient Http;

        protected ApiClient()
        {
            string baseUrl = ConfigurationManager.AppSettings["ServerUrl"];
            if (string.IsNullOrEmpty(baseUrl)) baseUrl = "http://localhost:5000/";
            Http = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        protected T Get<T>(string path)
        {
            var response = Http.GetAsync(path).Result;
            EnsureOk(response);
            var json = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(json);
        }

        protected T Post<T>(string path, object body)
        {
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var response = Http.PostAsync(path, content).Result;
            EnsureOk(response);
            var json = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(json);
        }

        protected void Delete(string path)
        {
            var response = Http.DeleteAsync(path).Result;
            EnsureOk(response);
        }

        private static void EnsureOk(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return;
            string body = response.Content.ReadAsStringAsync().Result;
            throw new HttpRequestException((int)response.StatusCode + " " + response.ReasonPhrase + "\n\n" + body);
        }
    }
}
