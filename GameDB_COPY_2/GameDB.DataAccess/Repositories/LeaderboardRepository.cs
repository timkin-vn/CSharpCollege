using Dapper;
using GameDB.Common.Models;
using GameDB.DataAccess.Interfaces;
using Npgsql;
using System.Data;

namespace GameDB.DataAccess.Repositories;

public class LeaderboardRepository : ILeaderboardRepository
{
    private readonly string _connectionString;

    public LeaderboardRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public async Task<IEnumerable<LeaderboardEntry>> GetTopAsync(int limit = 10)
    {
        const string sql = @"
            SELECT 
                l.score_id as ScoreId,
                l.player_id as PlayerId,
                p.username as Username,
                l.high_score as HighScore,
                l.max_tile as MaxTile,
                l.achieved_at as AchievedAt
            FROM leaderboards l
            JOIN players p ON l.player_id = p.player_id
            ORDER BY l.high_score DESC 
            LIMIT @Limit";

        using var connection = Connection;
        return await connection.QueryAsync<LeaderboardEntry>(sql, new { Limit = limit });
    }

    public async Task<bool> UpdateAsync(int playerId, int highScore, int maxTile)
    {
        const string sql = @"
            INSERT INTO leaderboards (player_id, high_score, max_tile, achieved_at) 
            VALUES (@PlayerId, @HighScore, @MaxTile, CURRENT_TIMESTAMP)
            ON CONFLICT (player_id) 
            DO UPDATE SET 
                high_score = EXCLUDED.high_score,
                max_tile = EXCLUDED.max_tile,
                achieved_at = EXCLUDED.achieved_at
            WHERE EXCLUDED.high_score > leaderboards.high_score";

        using var connection = Connection;
        var affected = await connection.ExecuteAsync(sql, new 
        { 
            PlayerId = playerId, 
            HighScore = highScore, 
            MaxTile = maxTile 
        });
        return affected > 0;
    }

    public async Task<int> GetHighScoreAsync(int playerId)
    {
        const string sql = "SELECT COALESCE(high_score, 0) FROM leaderboards WHERE player_id = @PlayerId";
        using var connection = Connection;
        return await connection.ExecuteScalarAsync<int>(sql, new { PlayerId = playerId });
    }

    public async Task<IEnumerable<LeaderboardEntry>> GetByPlayerIdAsync(int playerId)
    {
        const string sql = @"
            SELECT 
                l.score_id as ScoreId,
                l.player_id as PlayerId,
                p.username as Username,
                l.high_score as HighScore,
                l.max_tile as MaxTile,
                l.achieved_at as AchievedAt
            FROM leaderboards l
            JOIN players p ON l.player_id = p.player_id
            WHERE l.player_id = @PlayerId
            ORDER BY l.achieved_at DESC";

        using var connection = Connection;
        return await connection.QueryAsync<LeaderboardEntry>(sql, new { PlayerId = playerId });
    }

    public async Task<IEnumerable<LeaderboardEntry>> GetTopByTileAsync(int limit = 10)
    {
        const string sql = @"
            SELECT 
                l.score_id as ScoreId,
                l.player_id as PlayerId,
                p.username as Username,
                l.high_score as HighScore,
                l.max_tile as MaxTile,
                l.achieved_at as AchievedAt
            FROM leaderboards l
            JOIN players p ON l.player_id = p.player_id
            ORDER BY l.max_tile DESC, l.high_score DESC
            LIMIT @Limit";

        using var connection = Connection;
        return await connection.QueryAsync<LeaderboardEntry>(sql, new { Limit = limit });
    }

    public async Task<int> GetPlayerRankAsync(int playerId)
    {
        const string sql = @"
            WITH ranked AS (
                SELECT 
                    player_id,
                    high_score,
                    ROW_NUMBER() OVER (ORDER BY high_score DESC) as rank
                FROM leaderboards
            )
            SELECT COALESCE(rank, 0) FROM ranked WHERE player_id = @PlayerId";

        using var connection = Connection;
        return await connection.ExecuteScalarAsync<int>(sql, new { PlayerId = playerId });
    }
}