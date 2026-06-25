using GameDB.Business.Models;
using GameDB.Business.Services;
using GameDB.Common.Helpers;
using GameDB.Common.Models;
using GameDB.DataAccess;

namespace GameDB.Server.Services;

public class GameApiService
{
    private readonly string _connectionString;
    private readonly GameService _gameService = new();

    public GameApiService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
                            ?? throw new Exception("Connection string not found");
    }

    private UnitOfWork CreateUnitOfWork() => new UnitOfWork(_connectionString);

    // ===== АУТЕНТИФИКАЦИЯ =====
    public async Task<(bool Success, int? PlayerId, string? Message)> AuthenticateAsync(string username, string password)
    {
        using var uow = CreateUnitOfWork();
        
        var player = await uow.Players.GetByUsernameAsync(username);
        if (player == null)
        {
            return (false, null, "Пользователь не найден");
        }

        var passwordHash = PasswordHelper.HashPassword(password);
        var isValid = await uow.Players.AuthenticateAsync(username, passwordHash);
        
        if (!isValid)
        {
            return (false, null, "Неверный пароль");
        }

        return (true, player.PlayerId, "Успешный вход");
    }

    public async Task<(bool Success, int? PlayerId, string? Message)> RegisterAsync(string username, string password)
    {
        using var uow = CreateUnitOfWork();

        var exists = await uow.Players.UsernameExistsAsync(username);
        if (exists)
        {
            return (false, null, "Пользователь уже существует");
        }

        var passwordHash = PasswordHelper.HashPassword(password);
        var playerId = await uow.Players.CreateAsync(username, passwordHash);

        return (true, playerId, "Регистрация успешна");
    }
}