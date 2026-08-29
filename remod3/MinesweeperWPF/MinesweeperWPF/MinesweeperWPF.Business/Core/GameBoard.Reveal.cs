using MinesweeperWPF.Business.Cells;

namespace MinesweeperWPF.Business.Core;

public sealed partial class GameBoard {
    private void RevealInternal(int row, int col, bool allowFlood, List<CellUpdate> updates) {
        ref var cell = ref _cells[row, col];

        if (cell.State == CellState.Flagged || cell.State == CellState.Revealed || cell.State == CellState.Mine)
            return;

        if (cell.IsMine) {
            cell.State = CellState.Exploded;
            GameOver = true;
            updates.Add(CellUpdateFromCell(row, col, cell));
            return;
        }

        cell.State = CellState.Revealed;
        updates.Add(CellUpdateFromCell(row, col, cell));

        if (allowFlood && cell.AdjacentMines == 0)
            Flood(row, col, updates);
    }

    private void Flood(int row, int col, List<CellUpdate> updates) {
        var visited = new HashSet<(int r, int c)>();
        var q = new Queue<((int r, int c) pos, int depth)>();

        visited.Add((row, col));
        q.Enqueue(((row, col), 0));

        var processed = 0;

        while (q.Count > 0) {
            var (pos, depth) = q.Dequeue();
            if (depth > MaxFloodDepth) continue;

            ForEachNeighbor(pos.r, pos.c, (nr, nc) => {
                if (!visited.Add((nr, nc))) return;

                ref var neighbor = ref _cells[nr, nc];
                if (neighbor.State is CellState.Flagged or CellState.Revealed or CellState.Mine)
                    return;

                neighbor.State = CellState.Revealed;
                updates.Add(CellUpdateFromCell(nr, nc, neighbor));
                processed++;

                if (processed >= MaxFloodCells) return;

                if (!neighbor.IsMine && neighbor.AdjacentMines == 0)
                    q.Enqueue(((nr, nc), depth + 1));
            });

            if (processed >= MaxFloodCells) break;
        }
    }
}