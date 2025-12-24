namespace Minesweeper.Web.Business.Cells;

public sealed record CellUpdate(int Row, int Column, CellState State, int AdjacentMines);
