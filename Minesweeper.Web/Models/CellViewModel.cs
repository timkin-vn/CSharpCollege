using Minesweeper.Web.Business.Cells;

namespace Minesweeper.Web.Models;

public sealed class CellViewModel {
    public CellState State { get; set; }
    public int AdjacentMines { get; set; }
}
