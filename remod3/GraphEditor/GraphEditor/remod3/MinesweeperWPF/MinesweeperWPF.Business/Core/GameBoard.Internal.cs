using MinesweeperWPF.Business.Cells;
using MinesweeperWPF.Business.Results;

namespace MinesweeperWPF.Business.Core;

public sealed partial class GameBoard {
    private GameActionResult EmptyResult() => new([], GameOver, HasWon, HitMine: false);

    private GameActionResult CreateResult(List<CellUpdate> updates, bool hitMine) =>
        new(updates, GameOver, HasWon, hitMine);

    private static void EnsureInBounds(int row, int col) {
        if (row < 0 || col < 0)
            throw new ArgumentOutOfRangeException();
    }

    private bool IsInBounds(int row, int col) =>
        row >= 0 && row < Settings.Rows && col >= 0 && col < Settings.Columns;

    private static CellUpdate CellUpdateFromCell(int row, int col, in Cell cell) =>
        new(row, col, cell.State, cell.AdjacentMines);
}
