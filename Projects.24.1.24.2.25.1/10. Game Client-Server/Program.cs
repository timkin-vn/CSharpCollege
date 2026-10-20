using FifteenGame.Business.Services;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.DataAccess.Repositories;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Same layers as project 9. Project 9's business library uses the actual
// Minesweeper classes from project 7, so the game algorithms are not duplicated.
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "minesweeper.db");
var connectionString = new SqliteConnectionStringBuilder { DataSource = dbPath }.ToString();
builder.Services.AddSingleton<IGameResultRepository>(new SqliteGameResultRepository(connectionString));
builder.Services.AddSingleton<MinesweeperDbService>();

var app = builder.Build();
app.Services.GetRequiredService<IGameResultRepository>().Initialize();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/api/games", (CreateGameRequest request, MinesweeperDbService service) =>
{
    var state = service.CreateGame(request.PlayerName, request.Difficulty, request.FirstMoveMode != "dangerous");
    return Results.Ok(ToPublicState(state));
});

app.MapGet("/api/games/{id:guid}", (Guid id, MinesweeperDbService service) =>
{
    var state = service.GetGame(id);
    return state == null ? Results.NotFound() : Results.Ok(ToPublicState(state));
});

app.MapPost("/api/games/{id:guid}/reveal", (Guid id, CellActionRequest request, MinesweeperDbService service) =>
{
    var state = service.Reveal(id, request.Row, request.Column);
    return state == null ? Results.NotFound() : Results.Ok(ToPublicState(state));
});

app.MapPost("/api/games/{id:guid}/flag", (Guid id, CellActionRequest request, MinesweeperDbService service) =>
{
    var state = service.ToggleFlag(id, request.Row, request.Column);
    return state == null ? Results.NotFound() : Results.Ok(ToPublicState(state));
});

app.MapGet("/api/results", (HttpContext context, IGameResultRepository repository) =>
{
    context.Response.Headers.CacheControl = "no-store, no-cache";
    return Results.Ok(repository.GetTopResults());
});

// Available locally in Development only. Helps diagnose SQLite independently of the browser.
if (app.Environment.IsDevelopment())
{
    app.MapGet("/api/debug/db", (IGameResultRepository repository) =>
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM GameResults;";
        long count = (long)(command.ExecuteScalar() ?? 0L);
        return Results.Ok(new
        {
            databaseFile = dbPath,
            fileExists = File.Exists(dbPath),
            totalRows = count,
            returnedResults = repository.GetTopResults().Count
        });
    });
}

app.Run();

// Adapt the shared game state to the response expected by the existing browser client.
static PublicGameState ToPublicState(GameState state)
{
    var cells = state.Cells.Select(cell => new PublicCell(
        cell.Row,
        cell.Column,
        cell.IsOpened || (state.IsFinished && !state.IsWon && cell.IsMine),
        cell.IsFlagged,
        cell.IsOpened && !cell.IsMine ? cell.MinesAround : null,
        state.IsFinished && !state.IsWon && cell.IsMine
    )).ToArray();

    string status = !state.IsFinished ? "Игра идёт" : state.IsWon ? "Победа" : "Поражение";
    return new PublicGameState(
        state.Id,
        state.DifficultyName,
        state.SafeFirstMove ? "Безопасный" : "Опасный",
        status,
        state.Rows,
        state.Columns,
        state.Mines,
        state.Moves,
        state.Flags,
        cells
    );
}

record CreateGameRequest(string? PlayerName, string? Difficulty, string? FirstMoveMode);
record CellActionRequest(int Row, int Column);
record PublicCell(int Row, int Column, bool IsRevealed, bool IsFlagged, int? AdjacentMines, bool IsMineVisible);
record PublicGameState(
    Guid Id,
    string Difficulty,
    string FirstMoveMode,
    string Status,
    int Rows,
    int Columns,
    int Mines,
    int Moves,
    int Flags,
    PublicCell[] Cells
);
