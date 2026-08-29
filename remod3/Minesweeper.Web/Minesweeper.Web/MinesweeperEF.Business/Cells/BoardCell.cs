namespace MinesweeperEF.Business.Cells;

public struct BoardCell {
    public bool IsMine;
    public int AdjacentMines;
    public CellState State;
}
