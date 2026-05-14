using MinesweeperEF.Contracts.Games;

namespace MinesweeperEF.Client.BusinessProxy.Services;

public sealed class GameServiceProxy(ApiClient api) {
    public async Task<GameSnapshotDto> NewGameAsync(int rows, int cols, int mines, string? name) {
        var req = new NewGameRequest(rows, cols, mines, name);
        return await api.PostAsync<GameSnapshotDto>("/api/games", req);
    }

    public async Task<GameSnapshotDto> ActionAsync(Guid gameId, GameActionType type, int row, int col, bool debugMode = false) {
        var req = new GameActionRequest(type, row, col, debugMode);
        return await api.PostAsync<GameSnapshotDto>($"/api/games/{gameId}/action", req);
    }

    public async Task<List<SavedGameInfoDto>> ListAsync() =>
        await api.GetAsync<List<SavedGameInfoDto>>("/api/games");

    public async Task<GameSnapshotDto> GetAsync(Guid gameId) =>
        await api.GetAsync<GameSnapshotDto>($"/api/games/{gameId}");
}
