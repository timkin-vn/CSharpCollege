using MinesweeperWPF.Business.Cells;

namespace MinesweeperWPF.Business.Core;

public sealed partial class GameBoard {
    private struct Cell(bool isMine, int adjacentMines, CellState state) {
        public bool IsMine = isMine;
        public int AdjacentMines = adjacentMines;
        public CellState State = state;
    }
}
