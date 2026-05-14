using Minesweeper.Business;
using MinesweeperWPF.Business.Cells;

namespace MinesweeperWPF.Business.Core;

public sealed partial class GameBoard {
    private const int MaxFloodCells = 60;
    private const int MaxFloodDepth = 6;

    private readonly Random _rng;
    private Cell[,] _cells = new Cell[0, 0];

    public GameBoard(Random? rng = null) {
        _rng = rng ?? new Random();
        Settings = GameSettings.Intermediate();
        PrepareBoard();
    }

    private GameSettings Settings { get; set; }
    public int FlagsLeft { get; private set; }
    public bool GameOver { get; private set; }
    private bool HasWon { get; set; }
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
}
