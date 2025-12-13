namespace MinesweeperEF.Business;

public sealed class GameBoard {
    private const int MaxFloodCells = 60;
    private const int MaxFloodDepth = 6;

    private readonly Random _rng;
    private Cell[,] _cells = new Cell[0, 0];

    public GameBoard(Random? rng = null) {
        _rng = rng ?? new Random();
        Settings = GameSettings.Intermediate();
        PrepareBoard();
    }

    public GameSettings Settings { get; set; }
    public int FlagsLeft { get; private set; }
    public bool GameOver { get; private set; }
    public bool HasWon { get; set; }
    public bool HasStarted { get; private set; }

    public void ApplySettings(GameSettings settings) {
        Settings = settings;
        PrepareBoard();
    }

    public void NewGame() {
        PrepareBoard();
    }

    public CellSnapshot this[int row, int col] {
        get {
            EnsureInBounds(row, col);
            var cell = _cells[row, col];
            return new CellSnapshot(cell.State);
        }
    }

    public GameActionResult ToggleFlag(int row, int col, bool ignoreGameOver = false) {
        if ((GameOver && !ignoreGameOver) || !IsInBounds(row, col))
            return EmptyResult();

        ref var cell = ref _cells[row, col];
        if (cell.State == CellState.Revealed)
            return EmptyResult();

        var updates = new List<CellUpdate>(1);
        switch (cell.State) {
            case CellState.Flagged:
                cell.State = CellState.Questioned;
                FlagsLeft++;
                break;
            case CellState.Questioned:
                cell.State = CellState.Hidden;
                break;
            default:
                cell.State = CellState.Flagged;
                FlagsLeft--;
                break;
        }
        updates.Add(CellUpdateFromCell(row, col, cell));

        return CreateResult(updates, hitMine: false);
    }

    public GameActionResult Reveal(int row, int col, bool ignoreGameOver = false) {
        if ((GameOver && !ignoreGameOver) || !IsInBounds(row, col))
            return EmptyResult();

        if (!HasStarted) {
            EnsureMines(row, col);
            HasStarted = true;
        }

        var updates = new List<CellUpdate>();
        RevealInternal(row, col, allowFlood: true, updates);
        return CreateResult(updates, hitMine: updates.Any(u => u.State == CellState.Exploded));
    }

    public GameActionResult Chord(int row, int col, bool ignoreGameOver = false) {
        if ((GameOver && !ignoreGameOver) || !IsInBounds(row, col))
            return EmptyResult();

        ref var cell = ref _cells[row, col];
        if (cell.State != CellState.Revealed || cell.AdjacentMines <= 0)
            return EmptyResult();

        var flagged = 0;
        ForEachNeighbor(row, col, (r, c) => {
            if (_cells[r, c].State == CellState.Flagged)
                flagged++;
        });

        if (flagged != cell.AdjacentMines)
            return EmptyResult();

        var updates = new List<CellUpdate>();
        ForEachNeighbor(row, col, (r, c) => RevealInternal(r, c, allowFlood: true, updates));
        return CreateResult(updates, hitMine: updates.Any(u => u.State == CellState.Exploded));
    }

    public IReadOnlyList<CellUpdate> RevealAllMines() {
        var updates = new List<CellUpdate>();
        for (var r = 0; r < Settings.Rows; r++)
        for (var c = 0; c < Settings.Columns; c++) {
            ref var cell = ref _cells[r, c];
            if (!cell.IsMine || cell.State == CellState.Exploded) continue;
            cell.State = CellState.Mine;
            updates.Add(CellUpdateFromCell(r, c, cell));
        }
        return updates;
    }

    private void PrepareBoard() {
        _cells = new Cell[Settings.Rows, Settings.Columns];
        for (var r = 0; r < Settings.Rows; r++)
        for (var c = 0; c < Settings.Columns; c++)
            _cells[r, c] = new Cell();

        FlagsLeft = Settings.Mines;
        GameOver = false;
        HasStarted = false;
        HasWon = false;
    }

    private void RevealInternal(int row, int col, bool allowFlood, List<CellUpdate> updates) {
        if (!IsInBounds(row, col)) return;

        ref var cell = ref _cells[row, col];
        if (cell.State is CellState.Revealed or CellState.Flagged)
            return;

        cell.State = CellState.Revealed;
        updates.Add(CellUpdateFromCell(row, col, cell));

        if (cell.IsMine) {
            cell.State = CellState.Exploded;
            updates[^1] = CellUpdateFromCell(row, col, cell);
            GameOver = true;
            HasWon = false;
            return;
        }

        if (cell.AdjacentMines == 0 && allowFlood)
            Flood(row, col, updates);

        CheckWin(updates);
    }

    private void Flood(int row, int col, List<CellUpdate> updates) {
        var queue = new Queue<(int r, int c, int depth)>();
        queue.Enqueue((row, col, 0));

        var opened = 0;
        while (queue.Count > 0) {
            var (r, c, depth) = queue.Dequeue();
            ForEachNeighbor(r, c, (nr, nc) => {
                if (opened >= MaxFloodCells)
                    return;

                ref var neighbor = ref _cells[nr, nc];
                if (neighbor.State is CellState.Revealed or CellState.Flagged or CellState.Exploded || neighbor.IsMine)
                    return;

                neighbor.State = CellState.Revealed;
                updates.Add(CellUpdateFromCell(nr, nc, neighbor));
                opened++;

                if (neighbor.AdjacentMines == 0 && depth < MaxFloodDepth)
                    queue.Enqueue((nr, nc, depth + 1));
            });
        }
    }

    private void EnsureMines(int safeRow, int safeColumn) {
        var placed = 0;
        var forbidden = new HashSet<(int r, int c)> { (safeRow, safeColumn) };

        while (placed < Settings.Mines) {
            var r = _rng.Next(Settings.Rows);
            var c = _rng.Next(Settings.Columns);
            if (_cells[r, c].IsMine || forbidden.Contains((r, c)))
                continue;

            _cells[r, c].IsMine = true;
            placed++;
        }

        for (var r = 0; r < Settings.Rows; r++)
        for (var c = 0; c < Settings.Columns; c++)
            if (!_cells[r, c].IsMine)
                _cells[r, c].AdjacentMines = CountAdjacentMines(r, c);
    }

    private int CountAdjacentMines(int row, int col) {
        var count = 0;
        ForEachNeighbor(row, col, (r, c) => {
            if (_cells[r, c].IsMine)
                count++;
        });
        return count;
    }

    private void ForEachNeighbor(int row, int col, Action<int, int> action) {
        for (var dr = -1; dr <= 1; dr++)
        for (var dc = -1; dc <= 1; dc++) {
            if (dr == 0 && dc == 0) continue;
            var nr = row + dr;
            var nc = col + dc;
            if (IsInBounds(nr, nc))
                action(nr, nc);
        }
    }

    private void CheckWin(List<CellUpdate> updates) {
        if (GameOver)
            return;

        var unopened = 0;
        for (var r = 0; r < Settings.Rows; r++)
        for (var c = 0; c < Settings.Columns; c++)
            if (!_cells[r, c].IsMine && _cells[r, c].State != CellState.Revealed)
                unopened++;

        if (unopened == 0) {
            GameOver = true;
            HasWon = true;
            for (var r = 0; r < Settings.Rows; r++)
            for (var c = 0; c < Settings.Columns; c++) {
                ref var cell = ref _cells[r, c];
                if (cell.IsMine || cell.State == CellState.Revealed) continue;
                cell.State = CellState.Revealed;
                updates.Add(CellUpdateFromCell(r, c, cell));
            }
        }
    }

    private GameActionResult EmptyResult() => new([], GameOver, HasWon, HitMine: false);

    private GameActionResult CreateResult(List<CellUpdate> updates, bool hitMine) =>
        new(updates, GameOver, HasWon, hitMine);

    private static void EnsureInBounds(int row, int col) {
        if (row < 0 || col < 0)
            throw new ArgumentOutOfRangeException();
    }

    private bool IsInBounds(int row, int col) =>
        row >= 0 && row < Settings.Rows && col >= 0 && col < Settings.Columns;

    private static CellUpdate CellUpdateFromCell(int row, int col, in Cell cell) =>
        new(row, col, cell.State, cell.AdjacentMines);

    private struct Cell {
        public bool IsMine;
        public int AdjacentMines;
        public CellState State;
    }
    
    public GameStateDto ExportState() {
        var rows = Settings.Rows;
        var cols = Settings.Columns;

        var cells = new CellDto[rows * cols];
        var k = 0;

        for (var r = 0; r < rows; r++) {
            for (var c = 0; c < cols; c++) {
                var cell = _cells[r, c];
                cells[k++] = new CellDto(cell.IsMine, cell.AdjacentMines, cell.State);
            }
        }

        return new GameStateDto(Settings, FlagsLeft, GameOver, HasWon, HasStarted, cells);
    }

    public static GameBoard ImportState(GameStateDto dto) {
        var board = new GameBoard { Settings = dto.Settings };

        var rows = dto.Settings.Rows;
        var cols = dto.Settings.Columns;
        board._cells = new Cell[rows, cols];
        
        board.FlagsLeft = dto.FlagsLeft;
        board.GameOver = dto.GameOver;
        board.HasWon = dto.HasWon;
        board.HasStarted = dto.HasStarted;

        var k = 0;
        for (var r = 0; r < rows; r++) {
            for (var c = 0; c < cols; c++) {
                var d = dto.Cells[k++];
                board._cells[r, c] = new Cell {
                    IsMine = d.IsMine,
                    AdjacentMines = d.AdjacentMines,
                    State = d.State
                };
            }
        }

        return board;
    }
}
