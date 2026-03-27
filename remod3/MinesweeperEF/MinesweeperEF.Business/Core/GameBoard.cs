using MinesweeperEF.Business.Cells;
using MinesweeperEF.Business.Models;
using MinesweeperEF.Business.Results;

namespace MinesweeperEF.Business.Core;

public sealed partial class GameBoard {
    private const int MaxFloodCells = 60;
    private const int MaxFloodDepth = 6;

    private readonly Random _rng;
    private BoardCell[,] _cells = new BoardCell[0, 0];

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
}
