using MinesweeperWPF.Business.Cells;

namespace MinesweeperWPF.Business.Core;

public sealed partial class GameBoard {
    private void CheckWin(List<CellUpdate> updates) {
        if (GameOver) return;

        var revealed = 0;
        for (var r = 0; r < Settings.Rows; r++) {
            for (var c = 0; c < Settings.Columns; c++) {
                var cell = _cells[r, c];
                if (!cell.IsMine && cell.State == CellState.Revealed)
                    revealed++;
            }
        }

        var safeCells = Settings.Rows * Settings.Columns - Settings.Mines;
        if (revealed == safeCells) {
            GameOver = true;
            HasWon = true;
        }
    }
}
