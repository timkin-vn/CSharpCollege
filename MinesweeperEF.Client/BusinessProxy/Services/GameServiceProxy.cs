using MinesweeperEF.Contracts.Games;

namespace MinesweeperEF.Client.BusinessProxy.Services;

public sealed class GameServiceProxy {
    private readonly ApiClient _api;

    public GameServiceProxy(ApiClient api) => _api = api;

    public async Task<GameSnapshotDto> NewGameAsync(int rows, int cols, int mines, string? name) {
        var req = new NewGameRequest(rows, cols, mines, name);
        return await _api.PostAsync<GameSnapshotDto>("/api/games", req);
    }

    public async Task<GameSnapshotDto> ActionAsync(Guid gameId, GameActionType type, int row, int col, bool debugMode = false) {
        var req = new GameActionRequest(type, row, col, debugMode);
        return await _api.PostAsync<GameSnapshotDto>($"/api/games/{gameId}/action", req);
    }

    public async Task<List<SavedGameInfoDto>> ListAsync() =>
        await _api.GetAsync<List<SavedGameInfoDto>>("/api/games");

    public async Task<GameSnapshotDto> GetAsync(Guid gameId) =>
        await _api.GetAsync<GameSnapshotDto>($"/api/games/{gameId}");
}
