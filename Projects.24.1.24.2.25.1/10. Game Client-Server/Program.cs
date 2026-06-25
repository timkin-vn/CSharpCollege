using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var games = new ConcurrentDictionary<Guid, GameSession>();
var difficulties = new Dictionary<string, DifficultySettings>
{
    ["easy"] = new("easy", "Лёгкий", 9, 9, 10),
    ["medium"] = new("medium", "Средний", 12, 12, 25),
    ["hard"] = new("hard", "Тяжёлый", 16, 16, 50)
};

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/api/games", (CreateGameRequest request) =>
{
    var difficulty = difficulties.GetValueOrDefault(request.Difficulty ?? "easy", difficulties["easy"]);
    var firstMoveMode = request.FirstMoveMode == "dangerous" ? FirstMoveMode.Dangerous : FirstMoveMode.Safe;

    var game = new GameSession(difficulty, firstMoveMode);
    games[game.Id] = game;

    return Results.Ok(game.ToPublicState());
});

app.MapGet("/api/games/{id:guid}", (Guid id) =>
{
    if (!games.TryGetValue(id, out var game))
    {
        return Results.NotFound(new { message = "Игра не найдена" });
    }

    return Results.Ok(game.ToPublicState());
});

app.MapPost("/api/games/{id:guid}/reveal", (Guid id, CellActionRequest request) =>
{
    if (!games.TryGetValue(id, out var game))
    {
        return Results.NotFound(new { message = "Игра не найдена" });
    }

    game.Reveal(request.Row, request.Column);
    return Results.Ok(game.ToPublicState());
});

app.MapPost("/api/games/{id:guid}/flag", (Guid id, CellActionRequest request) =>
{
    if (!games.TryGetValue(id, out var game))
    {
        return Results.NotFound(new { message = "Игра не найдена" });
    }

    game.ToggleFlag(request.Row, request.Column);
    return Results.Ok(game.ToPublicState());
});

app.Run();

record CreateGameRequest(string? Difficulty, string? FirstMoveMode);
record CellActionRequest(int Row, int Column);
record DifficultySettings(string Key, string Name, int Rows, int Columns, int Mines);
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

enum FirstMoveMode
{
    Safe,
    Dangerous
}

enum GameStatus
{
    Playing,
    Won,
    Lost
}

class Cell
{
    public bool HasMine { get; set; }
    public bool IsRevealed { get; set; }
    public bool IsFlagged { get; set; }
    public int AdjacentMines { get; set; }
}

class GameSession
{
    private readonly DifficultySettings _difficulty;
    private readonly FirstMoveMode _firstMoveMode;
    private readonly Cell[] _cells;
    private bool _minesPlaced;

    public Guid Id { get; } = Guid.NewGuid();
    public GameStatus Status { get; private set; } = GameStatus.Playing;
    public int Moves { get; private set; }

    public GameSession(DifficultySettings difficulty, FirstMoveMode firstMoveMode)
    {
        _difficulty = difficulty;
        _firstMoveMode = firstMoveMode;
        _cells = Enumerable.Range(0, difficulty.Rows * difficulty.Columns)
            .Select(_ => new Cell())
            .ToArray();

        if (_firstMoveMode == FirstMoveMode.Dangerous)
        {
            PlaceMines(excludedIndex: null);
            CalculateNumbers();
            _minesPlaced = true;
        }
    }

    public void Reveal(int row, int column)
    {
        if (Status != GameStatus.Playing || !IsInside(row, column))
        {
            return;
        }

        var index = GetIndex(row, column);
        var cell = _cells[index];

        if (cell.IsRevealed || cell.IsFlagged)
        {
            return;
        }

        if (!_minesPlaced)
        {
            PlaceMines(excludedIndex: index);
            CalculateNumbers();
            _minesPlaced = true;
        }

        Moves++;

        if (cell.HasMine)
        {
            cell.IsRevealed = true;
            Status = GameStatus.Lost;
            RevealAllMines();
            return;
        }

        OpenSafeArea(index);
        CheckWin();
    }

    public void ToggleFlag(int row, int column)
    {
        if (Status != GameStatus.Playing || !IsInside(row, column))
        {
            return;
        }

        var index = GetIndex(row, column);
        var cell = _cells[index];

        if (cell.IsRevealed)
        {
            return;
        }

        cell.IsFlagged = !cell.IsFlagged;
    }

    public PublicGameState ToPublicState()
    {
        var publicCells = _cells.Select((cell, index) =>
        {
            var row = index / _difficulty.Columns;
            var column = index % _difficulty.Columns;
            int? adjacentMines = cell.IsRevealed && !cell.HasMine ? cell.AdjacentMines : null;
            var isMineVisible = Status == GameStatus.Lost && cell.HasMine;

            return new PublicCell(row, column, cell.IsRevealed, cell.IsFlagged, adjacentMines, isMineVisible);
        }).ToArray();

        return new PublicGameState(
            Id,
            _difficulty.Name,
            _firstMoveMode == FirstMoveMode.Safe ? "Безопасный" : "Опасный",
            Status switch
            {
                GameStatus.Won => "Победа",
                GameStatus.Lost => "Поражение",
                _ => "Игра идёт"
            },
            _difficulty.Rows,
            _difficulty.Columns,
            _difficulty.Mines,
            Moves,
            _cells.Count(cell => cell.IsFlagged),
            publicCells
        );
    }

    private void PlaceMines(int? excludedIndex)
    {
        var placed = 0;

        while (placed < _difficulty.Mines)
        {
            var index = Random.Shared.Next(_cells.Length);

            if (index == excludedIndex || _cells[index].HasMine)
            {
                continue;
            }

            _cells[index].HasMine = true;
            placed++;
        }
    }

    private void CalculateNumbers()
    {
        for (var index = 0; index < _cells.Length; index++)
        {
            _cells[index].AdjacentMines = GetNeighbors(index).Count(neighborIndex => _cells[neighborIndex].HasMine);
        }
    }

    private void OpenSafeArea(int startIndex)
    {
        var queue = new Queue<int>();
        var visited = new HashSet<int>();
        queue.Enqueue(startIndex);

        while (queue.Count > 0)
        {
            var index = queue.Dequeue();
            if (!visited.Add(index))
            {
                continue;
            }

            var cell = _cells[index];
            if (cell.IsFlagged || cell.HasMine)
            {
                continue;
            }

            cell.IsRevealed = true;

            if (cell.AdjacentMines == 0)
            {
                foreach (var neighborIndex in GetNeighbors(index))
                {
                    var neighbor = _cells[neighborIndex];
                    if (!neighbor.IsRevealed && !neighbor.HasMine)
                    {
                        queue.Enqueue(neighborIndex);
                    }
                }
            }
        }
    }

    private void CheckWin()
    {
        var openedSafeCells = _cells.Count(cell => cell.IsRevealed && !cell.HasMine);
        var requiredSafeCells = _cells.Length - _difficulty.Mines;

        if (openedSafeCells == requiredSafeCells)
        {
            Status = GameStatus.Won;

            foreach (var cell in _cells.Where(cell => cell.HasMine))
            {
                cell.IsFlagged = true;
            }
        }
    }

    private void RevealAllMines()
    {
        foreach (var cell in _cells.Where(cell => cell.HasMine))
        {
            cell.IsRevealed = true;
        }
    }

    private IEnumerable<int> GetNeighbors(int index)
    {
        var row = index / _difficulty.Columns;
        var column = index % _difficulty.Columns;

        for (var rowOffset = -1; rowOffset <= 1; rowOffset++)
        {
            for (var columnOffset = -1; columnOffset <= 1; columnOffset++)
            {
                if (rowOffset == 0 && columnOffset == 0)
                {
                    continue;
                }

                var newRow = row + rowOffset;
                var newColumn = column + columnOffset;

                if (IsInside(newRow, newColumn))
                {
                    yield return GetIndex(newRow, newColumn);
                }
            }
        }
    }

    private bool IsInside(int row, int column)
    {
        return row >= 0 && row < _difficulty.Rows && column >= 0 && column < _difficulty.Columns;
    }

    private int GetIndex(int row, int column)
    {
        return row * _difficulty.Columns + column;
    }
}
