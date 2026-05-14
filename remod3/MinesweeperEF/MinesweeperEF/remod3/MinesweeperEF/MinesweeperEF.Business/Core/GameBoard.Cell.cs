using MinesweeperEF.Business.Cells;

namespace MinesweeperEF.Business.Core;

public sealed partial class GameBoard {
    public CellSnapshot this[int row, int col] {
        get {
            EnsureInBounds(row, col);
            var cell = _cells[row, col];
            return new CellSnapshot(cell.State);
        }
    }

    private static CellUpdate CellUpdateFromCell(int row, int col, in BoardCell cell) =>
        new(row, col, cell.State, cell.AdjacentMines);
}
