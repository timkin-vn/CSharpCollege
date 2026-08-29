using Minesweeper.Business;

namespace MinesweeperWPF.Business.Cells;

internal struct BoardCell {
    public bool IsMine;
    public int AdjacentMines;
    public CellState State;
}