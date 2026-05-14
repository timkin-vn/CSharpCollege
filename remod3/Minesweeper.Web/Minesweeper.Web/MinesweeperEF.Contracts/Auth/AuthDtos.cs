namespace MinesweeperEF.Contracts.Auth;

public sealed record RegisterRequest(string UserName, string Password);
public sealed record LoginRequest(string UserName, string Password);
public sealed record AuthResponse(string Token);
