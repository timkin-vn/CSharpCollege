using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blackjack.BusinessProxy.Infrastructure
{
    // Общее подключение клиента к серверу Blackjack.WebApi
    public static class HttpConnection
    {
        // Адрес сервера (см. Blackjack.WebApi/Properties/launchSettings.json)
        public const string ServerUrl = "http://localhost:5080/";

        private static readonly HttpClient Client = new HttpClient
        {
            BaseAddress = new Uri(ServerUrl),
            Timeout = TimeSpan.FromSeconds(10),
        };

        public static T Get<T>(string url)
        {
            return RunSync(async () =>
            {
                var response = await Client.GetAsync(url).ConfigureAwait(false);
                return await ReadResult<T>(response).ConfigureAwait(false);
            });
        }

        public static T Post<T>(string url, object body = null)
        {
            return RunSync(async () =>
            {
                var response = body == null
                    ? await Client.PostAsync(url, null).ConfigureAwait(false)
                    : await Client.PostAsJsonAsync(url, body).ConfigureAwait(false);
                return await ReadResult<T>(response).ConfigureAwait(false);
            });
        }

        private static async Task<T> ReadResult<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new InvalidOperationException($"Сервер вернул ошибку {(int)response.StatusCode}: {text}");
            }

            return await response.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
        }

        // Выполняем запрос в фоновом потоке, чтобы не было взаимной блокировки с потоком окна WPF
        private static T RunSync<T>(Func<Task<T>> request)
        {
            try
            {
                return Task.Run(request).GetAwaiter().GetResult();
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException(
                    $"Нет связи с сервером {ServerUrl}. Запустите Blackjack.WebApi. ({ex.Message})", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new InvalidOperationException($"Сервер {ServerUrl} не отвечает.", ex);
            }
        }
    }
}
