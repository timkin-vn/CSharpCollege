using GameDB.Common.Models;

namespace GameDB.DataAccess.Interfaces;

public interface ILeaderboardRepository
{
    Task<IEnumerable<LeaderboardEntry>> GetTopAsync(int limit = 10);
    Task<bool> UpdateAsync(int playerId, int highScore, int maxTile);
    Task<int> GetHighScoreAsync(int playerId);
    Task<IEnumerable<LeaderboardEntry>> GetByPlayerIdAsync(int playerId);
    Task<IEnumerable<LeaderboardEntry>> GetTopByTileAsync(int limit = 10);
    Task<int> GetPlayerRankAsync(int playerId);
}