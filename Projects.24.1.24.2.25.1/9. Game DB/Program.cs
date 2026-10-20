using FifteenGame.Business.Services;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.DataAccess.Repositories;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// SQLite stores the game results; the game itself uses shared logic from project 7.
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "minesweeper.db");
var connectionString = new SqliteConnectionStringBuilder { DataSource = dbPath }.ToString();
builder.Services.AddSingleton<IGameResultRepository>(new SqliteGameResultRepository(connectionString));
builder.Services.AddSingleton<MinesweeperDbService>();
var app = builder.Build();

var repository = app.Services.GetRequiredService<IGameResultRepository>();
repository.Initialize();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/results", (HttpContext context, IGameResultRepository repo) =>
{
    // Do not cache results while testing or after completing a game.
    context.Response.Headers.CacheControl = "no-store, no-cache";
    return Results.Ok(repo.GetTopResults());
});

// Temporary local diagnostic endpoint. It works only in Development mode.
if (app.Environment.IsDevelopment())
{
    app.MapGet("/api/debug/db", (IGameResultRepository repo) =>
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM GameResults;";
        long totalRows = (long)(command.ExecuteScalar() ?? 0L);
        using var last = connection.CreateCommand();
        last.CommandText = "SELECT COALESCE(MAX(Id), 0) FROM GameResults;";
        long latestId = (long)(last.ExecuteScalar() ?? 0L);
        return Results.Ok(new
        {
            databaseFile = dbPath,
            fileExists = File.Exists(dbPath),
            totalRows,
            latestId,
            returnedResults = repo.GetTopResults().Count
        });
    });
}


app.MapPost("/api/games", (NewGameRequest request, MinesweeperDbService service) =>
{
    var state = service.CreateGame(request.PlayerName, request.Difficulty, request.SafeFirstMove);
    return Results.Created($"/api/games/{state.Id}", state);
});

app.MapGet("/api/games/{id:guid}", (Guid id, MinesweeperDbService service) =>
{
    var state = service.GetGame(id);
    return state == null ? Results.NotFound() : Results.Ok(state);
});

app.MapPost("/api/games/{id:guid}/reveal", (Guid id, CellRequest request, MinesweeperDbService service) =>
{
    var state = service.Reveal(id, request.Row, request.Column);
    return state == null ? Results.NotFound() : Results.Ok(state);
});

app.MapPost("/api/games/{id:guid}/flag", (Guid id, CellRequest request, MinesweeperDbService service) =>
{
    var state = service.ToggleFlag(id, request.Row, request.Column);
    return state == null ? Results.NotFound() : Results.Ok(state);
});

app.Run();

record NewGameRequest(string? PlayerName, string? Difficulty, bool SafeFirstMove);
record CellRequest(int Row, int Column);
