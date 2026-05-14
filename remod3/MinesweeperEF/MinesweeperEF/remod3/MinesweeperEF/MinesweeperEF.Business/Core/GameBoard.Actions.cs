using MinesweeperEF.Business.Cells;
using MinesweeperEF.Business.Results;

namespace MinesweeperEF.Business.Core;

public sealed partial class GameBoard {
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
}
