using Minesweeper.Web.Business.Cells;

namespace Minesweeper.Web.Business.Core;

public sealed partial class GameBoard {
    private static CellUpdate CellUpdateFromCell(int row, int col, in BoardCell cell) =>
        new(row, col, cell.State, cell.AdjacentMines);
}
