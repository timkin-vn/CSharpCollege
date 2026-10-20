using FifteenGame.BusinessProxy.Infastructure;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Core.BusinessLogic.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace FifteenGame.BusinessProxy.Services
{
    /// <summary>Клиентская часть: вызывает игру на сервере через Web API.</summary>
    public class GameServiceProxy : IGameService
    {
        public GameSnapshot GetByUserId(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.GetAsync($"api/games/get-by-user-id/{userId}").Result;
                return ReadSnapshot(response);
            }
        }

        public GameSnapshot NewGame(int userId)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var response = httpClient.PostAsync($"api/games/new/{userId}", new StringContent(string.Empty)).Result;
                return ReadSnapshot(response);
            }
        }

        public GameSnapshot MakeMove(int userId, int dRow, int dColumn)
        {
            using (var httpClient = HttpConnection.HttpClient)
            {
                var request = new MakeMoveRequest { UserId = userId, RowDelta = dRow, ColumnDelta = dColumn };
                var response = httpClient.PostAsync("api/games/move", JsonContent.Create(request)).Result;
                return ReadSnapshot(response);
            }
        }

        private static GameSnapshot ReadSnapshot(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<GameSnapshot>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
