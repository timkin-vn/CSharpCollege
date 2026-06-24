using GameDB.Common.Models;

namespace GameDB.DataAccess.Interfaces;

public interface IPlayerRepository
{
    Task<Player?> GetByUsernameAsync(string username);
    Task<Player?> GetByIdAsync(int playerId);
    Task<int> CreateAsync(string username, string? passwordHash = null);
    Task<bool> AuthenticateAsync(string username, string passwordHash);
    Task<bool> UsernameExistsAsync(string username);
    Task<bool> UpdatePasswordAsync(int playerId, string newPasswordHash);
    Task<IEnumerable<Player>> GetAllAsync();
}