using Dapper;
using GameDB.Common.Models;
using GameDB.DataAccess.Interfaces;
using Npgsql;
using System.Data;

namespace GameDB.DataAccess.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly string _connectionString;

    public PlayerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public async Task<Player?> GetByUsernameAsync(string username)
    {
        const string sql = "SELECT * FROM players WHERE username = @Username";
        using var connection = Connection;
        return await connection.QueryFirstOrDefaultAsync<Player>(sql, new { Username = username });
    }

    public async Task<Player?> GetByIdAsync(int playerId)
    {
        const string sql = "SELECT * FROM players WHERE player_id = @PlayerId";
        using var connection = Connection;
        return await connection.QueryFirstOrDefaultAsync<Player>(sql, new { PlayerId = playerId });
    }

    public async Task<int> CreateAsync(string username, string? passwordHash = null)
    {
        const string sql = @"
            INSERT INTO players (username, password_hash) 
            VALUES (@Username, @PasswordHash) 
            RETURNING player_id";

        using var connection = Connection;
        return await connection.ExecuteScalarAsync<int>(sql, new 
        { 
            Username = username, 
            PasswordHash = passwordHash 
        });
    }

    public async Task<bool> AuthenticateAsync(string username, string passwordHash)
    {
        const string sql = "SELECT COUNT(*) FROM players WHERE username = @Username AND password_hash = @PasswordHash";
        using var connection = Connection;
        var count = await connection.ExecuteScalarAsync<int>(sql, new 
        { 
            Username = username, 
            PasswordHash = passwordHash 
        });
        return count > 0;
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        const string sql = "SELECT COUNT(*) FROM players WHERE username = @Username";
        using var connection = Connection;
        var count = await connection.ExecuteScalarAsync<int>(sql, new { Username = username });
        return count > 0;
    }

    public async Task<bool> UpdatePasswordAsync(int playerId, string newPasswordHash)
    {
        const string sql = "UPDATE players SET password_hash = @PasswordHash WHERE player_id = @PlayerId";
        using var connection = Connection;
        var affected = await connection.ExecuteAsync(sql, new 
        { 
            PlayerId = playerId, 
            PasswordHash = newPasswordHash 
        });
        return affected > 0;
    }

    public async Task<IEnumerable<Player>> GetAllAsync()
    {
        const string sql = "SELECT * FROM players ORDER BY created_at DESC";
        using var connection = Connection;
        return await connection.QueryAsync<Player>(sql);
    }
}