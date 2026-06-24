using Dapper;
using Npgsql;

namespace GameDB.DataAccess;

public class DatabaseInitializer
{
    private readonly string _connectionString;

    public DatabaseInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

    private async Task<NpgsqlConnection> GetOpenConnectionAsync()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }

    public async Task EnsureDatabaseCreatedAsync()
    {
        const string createPlayers = @"
            CREATE TABLE IF NOT EXISTS players (
                player_id SERIAL PRIMARY KEY,
                username VARCHAR(50) UNIQUE NOT NULL,
                password_hash VARCHAR(255),
                created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
            );";

        const string createLeaderboards = @"
            CREATE TABLE IF NOT EXISTS leaderboards (
                score_id SERIAL PRIMARY KEY,
                player_id INT REFERENCES players(player_id) ON DELETE CASCADE,
                high_score INT NOT NULL DEFAULT 0,
                max_tile INT NOT NULL DEFAULT 2,
                achieved_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
            );";

        const string createSessions = @"
            CREATE TABLE IF NOT EXISTS current_sessions (
                session_id SERIAL PRIMARY KEY,
                player_id INT REFERENCES players(player_id) ON DELETE CASCADE,
                current_score INT NOT NULL DEFAULT 0,
                game_field INT[][] NOT NULL DEFAULT '{{0,0,0,0},{0,0,0,0},{0,0,0,0},{0,0,0,0}}',
                updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
            );";

        const string createIndexes = @"
            CREATE INDEX IF NOT EXISTS idx_leaderboards_high_score ON leaderboards(high_score DESC);
            CREATE INDEX IF NOT EXISTS idx_leaderboards_max_tile ON leaderboards(max_tile DESC);
            CREATE INDEX IF NOT EXISTS idx_sessions_player_id ON current_sessions(player_id);
            CREATE INDEX IF NOT EXISTS idx_sessions_updated_at ON current_sessions(updated_at DESC);
            CREATE INDEX IF NOT EXISTS idx_players_username ON players(username);";

        using var connection = await GetOpenConnectionAsync();
        await connection.ExecuteAsync(createPlayers);
        await connection.ExecuteAsync(createLeaderboards);
        await connection.ExecuteAsync(createSessions);
        await connection.ExecuteAsync(createIndexes);
    }

    public async Task<bool> DatabaseExistsAsync()
    {
        try
        {
            using var connection = await GetOpenConnectionAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}