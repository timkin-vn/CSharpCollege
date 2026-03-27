using MinesweeperEF.Business.Cells;

namespace MinesweeperEF.Business.Core;

public sealed partial class GameBoard {
    private void CheckWin(List<CellUpdate> updates) {
        if (GameOver)
            return;

        var unopened = 0;
        for (var r = 0; r < Settings.Rows; r++)
        for (var c = 0; c < Settings.Columns; c++)
            if (!_cells[r, c].IsMine && _cells[r, c].State != CellState.Revealed)
                unopened++;

        if (unopened == 0) {
            GameOver = true;
            HasWon = true;
            for (var r = 0; r < Settings.Rows; r++)
            for (var c = 0; c < Settings.Columns; c++) {
                ref var cell = ref _cells[r, c];
                if (cell.IsMine || cell.State == CellState.Revealed) continue;
                cell.State = CellState.Revealed;
                updates.Add(CellUpdateFromCell(r, c, cell));
            }
        }
    }
}
