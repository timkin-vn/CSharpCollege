namespace MinesweeperWPF.Business.Core;

public sealed partial class GameBoard {
    private void PrepareBoard() {
        _cells = new Cell[Settings.Rows, Settings.Columns];
        for (var r = 0; r < Settings.Rows; r++) {
            for (var c = 0; c < Settings.Columns; c++) {
                _cells[r, c] = new Cell();
            }
        }
        FlagsLeft = Settings.Mines;
        GameOver = false;
        HasWon = false;
        HasStarted = false;
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

        for (var r = 0; r < Settings.Rows; r++) {
            for (var c = 0; c < Settings.Columns; c++) {
                if (_cells[r, c].IsMine) continue;
                _cells[r, c].AdjacentMines = CountAdjacentMines(r, c);
            }
        }
    }

    private int CountAdjacentMines(int row, int col) {
        var count = 0;
        ForEachNeighbor(row, col, (nr, nc) => {
            if (_cells[nr, nc].IsMine) count++;
        });
        return count;
    }
}