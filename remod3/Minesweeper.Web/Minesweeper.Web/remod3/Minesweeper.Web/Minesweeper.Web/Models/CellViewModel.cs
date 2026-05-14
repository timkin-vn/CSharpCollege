using Minesweeper.Web.Business.Cells;

namespace Minesweeper.Web.Models;

public sealed class CellViewModel {
    public CellState State { get; set; } = CellState.Hidden;
    public int AdjacentMines { get; set; }
}
