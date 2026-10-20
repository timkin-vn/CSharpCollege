using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.Common.Dtos;
using Microsoft.Data.Sqlite;

namespace FifteenGame.DataAccess.Repositories;

public sealed class SqliteGameResultRepository : IGameResultRepository
{
    private readonly string _connectionString;

    public SqliteGameResultRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Initialize()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = @"CREATE TABLE IF NOT EXISTS GameResults
        (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            PlayerName TEXT NOT NULL,
            Difficulty TEXT NOT NULL,
            FirstMoveMode TEXT NOT NULL,
            IsWin INTEGER NOT NULL,
            Seconds INTEGER NOT NULL,
            Moves INTEGER NOT NULL,
            Rows INTEGER NOT NULL,
            Columns INTEGER NOT NULL,
            Mines INTEGER NOT NULL,
            CreatedAt TEXT NOT NULL
        );";
        command.ExecuteNonQuery();
    }

    public void Save(GameResultDto result)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO GameResults
            (PlayerName, Difficulty, FirstMoveMode, IsWin, Seconds, Moves, Rows, Columns, Mines, CreatedAt)
            VALUES ($player, $difficulty, $mode, $win, $seconds, $moves, $rows, $columns, $mines, $date);";
        command.Parameters.AddWithValue("$player", result.PlayerName);
        command.Parameters.AddWithValue("$difficulty", result.Difficulty);
        command.Parameters.AddWithValue("$mode", result.FirstMoveMode);
        command.Parameters.AddWithValue("$win", result.IsWin ? 1 : 0);
        command.Parameters.AddWithValue("$seconds", result.Seconds);
        command.Parameters.AddWithValue("$moves", result.Moves);
        command.Parameters.AddWithValue("$rows", result.Rows);
        command.Parameters.AddWithValue("$columns", result.Columns);
        command.Parameters.AddWithValue("$mines", result.Mines);
        command.Parameters.AddWithValue("$date", result.CreatedAt);
        int inserted = command.ExecuteNonQuery();
        if (inserted != 1)
            throw new InvalidOperationException($"SQLite: expected one new result, inserted {inserted}.");

        // Confirm that this very database connection can see the new record.
        using var verify = connection.CreateCommand();
        verify.CommandText = "SELECT COUNT(*) FROM GameResults WHERE Id = last_insert_rowid();";
        long found = (long)(verify.ExecuteScalar() ?? 0L);
        if (found != 1)
            throw new InvalidOperationException("SQLite: the inserted game result could not be read back.");
    }

    public IReadOnlyList<GameResultDto> GetTopResults()
    {
        var results = new List<GameResultDto>();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT Id, PlayerName, Difficulty, FirstMoveMode, IsWin,
            Seconds, Moves, Rows, Columns, Mines, CreatedAt
            FROM GameResults ORDER BY IsWin DESC, Seconds ASC, Moves ASC, CreatedAt DESC LIMIT 20;";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            results.Add(new GameResultDto
            {
                Id = reader.GetInt64(0), PlayerName = reader.GetString(1),
                Difficulty = reader.GetString(2), FirstMoveMode = reader.GetString(3),
                IsWin = reader.GetInt64(4) == 1, Seconds = reader.GetInt32(5),
                Moves = reader.GetInt32(6), Rows = reader.GetInt32(7),
                Columns = reader.GetInt32(8), Mines = reader.GetInt32(9),
                CreatedAt = reader.GetString(10)
            });
        }
        return results;
    }
}
