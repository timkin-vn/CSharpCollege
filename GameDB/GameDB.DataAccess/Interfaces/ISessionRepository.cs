using GameDB.Common.Models;

namespace GameDB.DataAccess.Interfaces;

public interface ISessionRepository
{
    Task<GameSession?> GetByPlayerIdAsync(int playerId);
    Task<bool> SaveAsync(int playerId, int currentScore, int[][] gameField);
    Task<bool> DeleteAsync(int playerId);
    Task<bool> SessionExistsAsync(int playerId);
    Task<bool> UpdateScoreAsync(int playerId, int newScore);
    Task<IEnumerable<GameSession>> GetAllActiveSessionsAsync();
}