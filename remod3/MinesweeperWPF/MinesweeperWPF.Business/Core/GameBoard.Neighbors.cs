namespace MinesweeperWPF.Business.Core;

public sealed partial class GameBoard {
    private void ForEachNeighbor(int row, int col, Action<int, int> action) {
        for (var dr = -1; dr <= 1; dr++) {
            for (var dc = -1; dc <= 1; dc++) {
                if (dr == 0 && dc == 0) continue;
                var nr = row + dr;
                var nc = col + dc;
                if (!IsInBounds(nr, nc)) continue;
                action(nr, nc);
            }
        }
    }
}
