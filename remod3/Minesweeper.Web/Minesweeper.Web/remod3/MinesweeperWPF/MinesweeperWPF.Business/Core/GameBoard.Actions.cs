using MinesweeperWPF.Business.Cells;
using MinesweeperWPF.Business.Results;

namespace MinesweeperWPF.Business.Core;

public sealed partial class GameBoard {
    public GameActionResult ToggleFlag(int row, int col) {
        if (GameOver || !IsInBounds(row, col))
            return EmptyResult();

        ref var cell = ref _cells[row, col];
        if (cell.State == CellState.Revealed || cell.State == CellState.Mine)
            return EmptyResult();

        var updates = new List<CellUpdate>();
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

    public GameActionResult Reveal(int row, int col) {
        if (GameOver || !IsInBounds(row, col))
            return EmptyResult();

        if (!HasStarted) {
            EnsureMines(row, col);
            HasStarted = true;
        }

        var updates = new List<CellUpdate>();
        RevealInternal(row, col, allowFlood: true, updates);

        CheckWin(updates);
        return CreateResult(updates, hitMine: updates.Any(u => u.State == CellState.Exploded));
    }

    public GameActionResult Chord(int row, int col) {
        if (GameOver || !IsInBounds(row, col))
            return EmptyResult();

        ref var cell = ref _cells[row, col];
        if (cell.State != CellState.Revealed || cell.AdjacentMines <= 0)
            return EmptyResult();

        var flagged = 0;
        ForEachNeighbor(row, col, (nr, nc) => {
            if (_cells[nr, nc].State == CellState.Flagged)
                flagged++;
        });
        if (flagged != cell.AdjacentMines)
            return EmptyResult();

        var updates = new List<CellUpdate>();
        ForEachNeighbor(row, col, (nr, nc) => {
            if (_cells[nr, nc].State is CellState.Hidden or CellState.Questioned)
                RevealInternal(nr, nc, allowFlood: false, updates);
        });

        CheckWin(updates);
        return CreateResult(updates, hitMine: updates.Any(u => u.State == CellState.Exploded));
    }

    public IReadOnlyList<CellUpdate> RevealAllMines() {
        var updates = new List<CellUpdate>();
        for (var r = 0; r < Settings.Rows; r++) {
            for (var c = 0; c < Settings.Columns; c++) {
                ref var cell = ref _cells[r, c];
                if (!cell.IsMine || cell.State == CellState.Exploded) continue;
                cell.State = CellState.Mine;
                updates.Add(CellUpdateFromCell(r, c, cell));
            }
        }
        return updates;
    }
}
