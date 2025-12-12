namespace Minesweeper.Business;

public readonly struct CellSnapshot(CellState state) {
    public CellState State { get; } = state;
}
