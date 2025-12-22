using Pacman.BusinessProxy.Infrastructure;
using Pacman.Common.Dtos;
using Pacman.Common.Services;
using System.Net.Http.Json;
using System.Text.Json; 

namespace Pacman.BusinessProxy.Services
{
    public class GameServiceProxy : IGameService
    {
        private readonly JsonSerializerOptions _options;

        public GameServiceProxy()
        {
            
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public MapDto GetLevel(int level)
        {
            
            var response = HttpConnection.Client.GetAsync($"api/game/level/{level}").Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<MapDto>(json, _options);
        }
    }
}