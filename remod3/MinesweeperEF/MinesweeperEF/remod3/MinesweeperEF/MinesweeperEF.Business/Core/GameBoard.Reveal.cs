using MinesweeperEF.Business.Cells;

namespace MinesweeperEF.Business.Core;

public sealed partial class GameBoard {
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
}
