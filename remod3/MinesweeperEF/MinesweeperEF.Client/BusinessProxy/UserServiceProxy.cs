using MinesweeperEF.Contracts.Auth;

namespace MinesweeperEF.Client.BusinessProxy;

public sealed class UserServiceProxy {
    private readonly ApiClient _api;

    public UserServiceProxy(ApiClient api) {
        _api = api;
    }

    public async Task RegisterAsync(string userName, string password) {
        var req = new RegisterRequest(userName, password);
        await _api.PostAsync("/api/auth/register", req);
    }

    public async Task LoginAsync(string userName, string password) {
        var req = new LoginRequest(userName, password);
        var resp = await _api.PostAsync<AuthResponse>("/api/auth/login", req);

        _api.SetBearer(resp.Token);
    }
}
