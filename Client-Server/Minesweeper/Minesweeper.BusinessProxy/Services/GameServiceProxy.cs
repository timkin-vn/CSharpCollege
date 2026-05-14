using Minesweeper.BusinessProxy.Services;
using Minesweeper.Common.Dto;
using Minesweeper.Common.Request;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Minesweeper.WpfClient.Services
{
    public class GameServiceProxy : HttpConnection, IGameServiceProxy
    {
        public async Task<GameResponse> CreateGame(int userId, int size, int mines)
        {
            var request = new CreateGameRequest
            {
                UserId = userId,
                Size = size,
                MineCount = mines
            };
            var response = await PostAsync<ApiResponse<GameResponse>>("games/new", request);
            return response.Data;
        }

        public async Task<GameResponse> GetGame(int gameId)
        {
            var response = await GetAsync<ApiResponse<GameResponse>>($"games/{gameId}");
            return response.Data;
        }

        public async Task<GameResponse> OpenCell(int gameId, int row, int col)
        {
            var request = new CellPositionRequest { Row = row, Column = col };
            var response = await PostAsync<ApiResponse<GameResponse>>($"games/{gameId}/open", request);
            return response.Data;
        }

        public async Task<GameResponse> ToggleFlag(int gameId, int row, int col)
        {
            var request = new CellPositionRequest { Row = row, Column = col };
            var response = await PostAsync<ApiResponse<GameResponse>>($"games/{gameId}/toggle-flag", request);
            return response.Data;
        }

        public async Task<GameResponse> GetLastActiveGame(int userId)
        {
            try
            {
                var response = await GetAsync<ApiResponse<GameResponse>>($"games/user/{userId}/active");
                return response?.Data;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<bool> IsGameOver(int gameId)
        {
            var response = await GetAsync<ApiResponse<bool>>($"games/{gameId}/is-over");
            return response.Data;
        }

        public async Task<bool> IsGameWon(int gameId)
        {
            var response = await GetAsync<ApiResponse<bool>>($"games/{gameId}/is-won");
            return response.Data;
        }
    }
}