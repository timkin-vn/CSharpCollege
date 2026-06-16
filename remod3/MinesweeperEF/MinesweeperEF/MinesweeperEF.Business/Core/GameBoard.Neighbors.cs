namespace MinesweeperEF.Business.Core;

public sealed partial class GameBoard {
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
}
