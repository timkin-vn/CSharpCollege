using MinesweeperEF.Business.Cells;
using MinesweeperEF.Business.Models;

namespace MinesweeperEF.Business.Core;

public sealed partial class GameBoard {
    public void ApplySettings(GameSettings settings) {
        Settings = settings;
        PrepareBoard();
    }

    public void NewGame() { PrepareBoard(); }

    private void PrepareBoard() {
        _cells = new BoardCell[Settings.Rows, Settings.Columns];
        for (var r = 0; r < Settings.Rows; r++)
        for (var c = 0; c < Settings.Columns; c++)
            _cells[r, c] = new BoardCell();

        FlagsLeft = Settings.Mines;
        GameOver = false;
        HasStarted = false;
        HasWon = false;
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
}
