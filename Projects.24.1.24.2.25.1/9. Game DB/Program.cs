using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var connectionString = builder.Configuration.GetConnectionString("Default") ?? "Data Source=minesweeper.db";
InitializeDatabase(connectionString);

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/results", () =>
{
    var results = new List<GameResultResponse>();

    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = """
        SELECT Id, PlayerName, Difficulty, FirstMoveMode, IsWin, Seconds, Moves, Rows, Columns, Mines, CreatedAt
        FROM GameResults
        ORDER BY IsWin DESC, Seconds ASC, Moves ASC, CreatedAt DESC
        LIMIT 20;
        """;

    using var reader = command.ExecuteReader();
    while (reader.Read())
    {
        results.Add(new GameResultResponse(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetString(3),
            reader.GetInt64(4) == 1,
            reader.GetInt32(5),
            reader.GetInt32(6),
            reader.GetInt32(7),
            reader.GetInt32(8),
            reader.GetInt32(9),
            reader.GetString(10)
        ));
    }

    return Results.Ok(results);
});

app.MapPost("/api/results", (SaveGameResultRequest request) =>
{
    var playerName = string.IsNullOrWhiteSpace(request.PlayerName) ? "Игрок" : request.PlayerName.Trim();
    if (playerName.Length > 40)
    {
        playerName = playerName[..40];
    }

    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = """
        INSERT INTO GameResults
        (PlayerName, Difficulty, FirstMoveMode, IsWin, Seconds, Moves, Rows, Columns, Mines, CreatedAt)
        VALUES
        ($playerName, $difficulty, $firstMoveMode, $isWin, $seconds, $moves, $rows, $columns, $mines, $createdAt);
        """;

    command.Parameters.AddWithValue("$playerName", playerName);
    command.Parameters.AddWithValue("$difficulty", request.Difficulty);
    command.Parameters.AddWithValue("$firstMoveMode", request.FirstMoveMode);
    command.Parameters.AddWithValue("$isWin", request.IsWin ? 1 : 0);
    command.Parameters.AddWithValue("$seconds", Math.Max(0, request.Seconds));
    command.Parameters.AddWithValue("$moves", Math.Max(0, request.Moves));
    command.Parameters.AddWithValue("$rows", Math.Max(1, request.Rows));
    command.Parameters.AddWithValue("$columns", Math.Max(1, request.Columns));
    command.Parameters.AddWithValue("$mines", Math.Max(1, request.Mines));
    command.Parameters.AddWithValue("$createdAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    command.ExecuteNonQuery();

    return Results.Created("/api/results", new { message = "Результат сохранён" });
});

app.Run();

static void InitializeDatabase(string connectionString)
{
    using var connection = new SqliteConnection(connectionString);
    connection.Open();

    using var command = connection.CreateCommand();
    command.CommandText = """
        CREATE TABLE IF NOT EXISTS GameResults
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
        );
        """;

    command.ExecuteNonQuery();
}

record SaveGameResultRequest(
    string PlayerName,
    string Difficulty,
    string FirstMoveMode,
    bool IsWin,
    int Seconds,
    int Moves,
    int Rows,
    int Columns,
    int Mines
);

record GameResultResponse(
    long Id,
    string PlayerName,
    string Difficulty,
    string FirstMoveMode,
    bool IsWin,
    int Seconds,
    int Moves,
    int Rows,
    int Columns,
    int Mines,
    string CreatedAt
);
