using System.Net.Http.Json;
using MinesweeperEF.Contracts.Games;

namespace MinesweeperEF.Client.BusinessProxy.Services;

public sealed class GameServiceProxy {
    private readonly ApiClient _api;

    public GameServiceProxy(ApiClient api) => _api = api;

    public async Task<GameSnapshotDto> NewGameAsync(int rows, int cols, int mines, string? name) {
        var req = new NewGameRequest(rows, cols, mines, name);
        return await _api._http.PostAsJsonAsync("api/games", req)
            .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<GameSnapshotDto>())
            .Unwrap() ?? throw new Exception("Empty response");
    }

    public async Task<GameSnapshotDto> ActionAsync(Guid gameId, GameActionType type, int row, int col) {
        var req = new GameActionRequest(type, row, col);
        return await _api._http.PostAsJsonAsync($"api/games/{gameId}/action", req)
            .ContinueWith(t => t.Result.Content.ReadFromJsonAsync<GameSnapshotDto>())
            .Unwrap() ?? throw new Exception("Empty response");
    }

    public async Task<List<SavedGameInfoDto>> ListAsync()
        => await _api._http.GetFromJsonAsync<List<SavedGameInfoDto>>("api/games") ?? new List<SavedGameInfoDto>();
}
