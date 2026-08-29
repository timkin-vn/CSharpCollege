using Microsoft.Extensions.Caching.Memory;
using Minesweeper.Web.Business.Cells;
using Minesweeper.Web.Business.Core;
using Minesweeper.Web.Business.Models;
using Minesweeper.Web.Business.Results;
using Minesweeper.Web.Models;

namespace Minesweeper.Web.Services;

public sealed class GameService {
    private readonly IMemoryCache _cache;
    private readonly IHttpContextAccessor _contextAccessor;
    private const string CacheKeyPrefix = "minesweeper:session:";

    public GameService(IMemoryCache cache, IHttpContextAccessor contextAccessor) {
        _cache = cache;
        _contextAccessor = contextAccessor;
    }

    public GameViewModel GetView() => BuildView(GetOrCreateState());

    public GameViewModel StartNewGame(DifficultyRequest request) {
        var settings = ResolveSettings(request);
        var state = GetOrCreateState();
        state.Reset(settings);
        return BuildView(state);
    }

    public GameViewModel ApplyAction(GameActionRequest request) {
        var state = GetOrCreateState();
        var board = state.Board;

        var action = request.Action?.ToLowerInvariant();
        var result = action switch {
            "reveal" => board.Reveal(request.Row, request.Column),
            "flag" => board.ToggleFlag(request.Row, request.Column),
            "chord" => board.Chord(request.Row, request.Column),
            _ => new GameActionResult(Array.Empty<CellUpdate>(), board.GameOver, board.GameOver && board.FlagsLeft == 0, false)
        };

        ApplyUpdates(state, result);
        return BuildView(state);
    }

    private GameState GetOrCreateState() {
        var context = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HTTP контекст недоступен");
        context.Session.SetString("active", "1");
        var cacheKey = CacheKeyPrefix + context.Session.Id;
        return _cache.GetOrCreate(cacheKey, _ => new GameState(GameSettings.Intermediate()))!;
    }

    private static void ApplyUpdates(GameState state, GameActionResult result) {
        if (result.Updates.Count > 0) {
            foreach (var update in result.Updates) {
                var cell = state.Cells[update.Row, update.Column];
                cell.State = update.State;
                cell.AdjacentMines = update.AdjacentMines;
                state.Cells[update.Row, update.Column] = cell;
            }
        }

        if (state.Board.HasStarted && state.StartedAt is null && !state.Completed)
            state.StartedAt = DateTimeOffset.UtcNow;

        if (result.GameOver) {
            state.Completed = true;
            state.HasWon = result.HasWon;
            state.HitMine = result.HitMine;
            state.ElapsedSeconds = CalculateElapsed(state);
            state.StartedAt = null;
        }
    }

    private static int CalculateElapsed(GameState state) {
        if (state.StartedAt is null)
            return state.ElapsedSeconds;

        var elapsed = (int)Math.Max(0, (DateTimeOffset.UtcNow - state.StartedAt.Value).TotalSeconds);
        return state.ElapsedSeconds + elapsed;
    }

    private static GameSettings ResolveSettings(DifficultyRequest request) {
        return request.Preset?.ToLowerInvariant() switch {
            "beginner" => GameSettings.Beginner(),
            "expert" => GameSettings.Expert(),
            _ when request.Rows.HasValue && request.Columns.HasValue && request.Mines.HasValue =>
                new GameSettings(
                    Math.Clamp(request.Rows.Value, 5, 60),
                    Math.Clamp(request.Columns.Value, 5, 60),
                    Math.Clamp(request.Mines.Value, 1, 999)
                ),
            _ => GameSettings.Intermediate()
        };
    }

    private static GameViewModel BuildView(GameState state) {
        var rows = state.Settings.Rows;
        var cols = state.Settings.Columns;
        var grid = new List<IReadOnlyList<CellViewModel>>(rows);
        for (var r = 0; r < rows; r++) {
            var row = new CellViewModel[cols];
            for (var c = 0; c < cols; c++)
                row[c] = state.Cells[r, c];
            grid.Add(row);
        }

        var seconds = state.Completed ? state.ElapsedSeconds : CalculateElapsed(state);

        return new GameViewModel {
            Rows = rows,
            Columns = cols,
            Mines = state.Settings.Mines,
            FlagsLeft = state.Board.FlagsLeft,
            Seconds = seconds,
            GameOver = state.Board.GameOver,
            HasWon = state.HasWon,
            HitMine = state.HitMine,
            HasStarted = state.Board.HasStarted,
            Difficulty = DescribeDifficulty(state.Settings),
            DifficultyKey = DescribeDifficultyKey(state.Settings),
            StatusText = BuildStatus(state),
            Cells = grid
        };
    }

    private static string DescribeDifficulty(GameSettings settings) {
        if (settings == GameSettings.Beginner())
            return "Новичок (9×9, 10 мин)";
        if (settings == GameSettings.Expert())
            return "Профессионал (30×16, 99 мин)";
        if (settings == GameSettings.Intermediate())
            return "Любитель (16×16, 40 мин)";
        return $"Пользовательский ({settings.Columns}×{settings.Rows}, {settings.Mines} мин)";
    }

    private static string DescribeDifficultyKey(GameSettings settings) {
        if (settings == GameSettings.Beginner())
            return "beginner";
        if (settings == GameSettings.Expert())
            return "expert";
        if (settings == GameSettings.Intermediate())
            return "intermediate";
        return "custom";
    }

    private static string BuildStatus(GameState state) {
        if (state.HasWon)
            return "Поздравляем, вы нашли все мины!";
        if (state.HitMine)
            return "Вы подорвались на мине. Попробуйте ещё раз!";
        if (!state.Board.HasStarted)
            return "Нажмите на любое поле, чтобы начать";
        return "Игра продолжается…";
    }

    private sealed class GameState {
        public GameState(GameSettings settings) {
            Settings = settings;
            Board = new GameBoard();
            Board.ApplySettings(settings);
            Cells = CreateCells(settings);
        }

        public GameBoard Board { get; }
        public GameSettings Settings { get; private set; }
        public CellViewModel[,] Cells { get; private set; }
        public DateTimeOffset? StartedAt { get; set; }
        public int ElapsedSeconds { get; set; }
        public bool Completed { get; set; }
        public bool HitMine { get; set; }
        public bool HasWon { get; set; }

        public void Reset(GameSettings settings) {
            Settings = settings;
            Board.ApplySettings(settings);
            Board.NewGame();
            Cells = CreateCells(settings);
            StartedAt = null;
            ElapsedSeconds = 0;
            Completed = false;
            HitMine = false;
            HasWon = false;
        }

        private static CellViewModel[,] CreateCells(GameSettings settings) {
            var grid = new CellViewModel[settings.Rows, settings.Columns];
            for (var r = 0; r < settings.Rows; r++)
            for (var c = 0; c < settings.Columns; c++)
                grid[r, c] = new CellViewModel();
            return grid;
        }
    }
}
