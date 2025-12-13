namespace MinesweeperEF.Business;

public sealed record CellUpdate(int Row, int Column, CellState State, int AdjacentMines);
