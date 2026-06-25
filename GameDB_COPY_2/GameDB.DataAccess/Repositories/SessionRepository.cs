using Dapper;
using GameDB.Common.Models;
using GameDB.DataAccess.Interfaces;
using Npgsql;
using System.Data;

namespace GameDB.DataAccess.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly string _connectionString;

    public SessionRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public async Task<GameSession?> GetByPlayerIdAsync(int playerId)
    {
        const string sql = "SELECT * FROM current_sessions WHERE player_id = @PlayerId";
        using var connection = Connection;
        return await connection.QueryFirstOrDefaultAsync<GameSession>(sql, new { PlayerId = playerId });
    }

    public async Task<bool> SaveAsync(int playerId, int currentScore, int[][] gameField)
    {
        const string sql = @"
            INSERT INTO current_sessions (player_id, current_score, game_field, updated_at)
            VALUES (@PlayerId, @CurrentScore, @GameField, CURRENT_TIMESTAMP)
            ON CONFLICT (player_id) 
            DO UPDATE SET 
                current_score = EXCLUDED.current_score,
                game_field = EXCLUDED.game_field,
                updated_at = EXCLUDED.updated_at";

        using var connection = Connection;
        var affected = await connection.ExecuteAsync(sql, new
        {
            PlayerId = playerId,
            CurrentScore = currentScore,
            GameField = gameField
        });
        return affected > 0;
    }

    public async Task<bool> DeleteAsync(int playerId)
    {
        const string sql = "DELETE FROM current_sessions WHERE player_id = @PlayerId";
        using var connection = Connection;
        var affected = await connection.ExecuteAsync(sql, new { PlayerId = playerId });
        return affected > 0;
    }

    public async Task<bool> SessionExistsAsync(int playerId)
    {
        const string sql = "SELECT COUNT(*) FROM current_sessions WHERE player_id = @PlayerId";
        using var connection = Connection;
        var count = await connection.ExecuteScalarAsync<int>(sql, new { PlayerId = playerId });
        return count > 0;
    }

    public async Task<bool> UpdateScoreAsync(int playerId, int newScore)
    {
        const string sql = @"
            UPDATE current_sessions 
            SET current_score = @NewScore, updated_at = CURRENT_TIMESTAMP
            WHERE player_id = @PlayerId";

        using var connection = Connection;
        var affected = await connection.ExecuteAsync(sql, new 
        { 
            PlayerId = playerId, 
            NewScore = newScore 
        });
        return affected > 0;
    }

    public async Task<IEnumerable<GameSession>> GetAllActiveSessionsAsync()
    {
        const string sql = @"
            SELECT * FROM current_sessions 
            WHERE updated_at > NOW() - INTERVAL '7 days'
            ORDER BY updated_at DESC";

        using var connection = Connection;
        return await connection.QueryAsync<GameSession>(sql);
    }
}