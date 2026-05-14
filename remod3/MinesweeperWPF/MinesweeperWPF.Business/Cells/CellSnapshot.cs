using Minesweeper.Business;

namespace MinesweeperWPF.Business.Cells;

public readonly struct CellSnapshot(CellState state) {
    public CellState State { get; } = state;
}
