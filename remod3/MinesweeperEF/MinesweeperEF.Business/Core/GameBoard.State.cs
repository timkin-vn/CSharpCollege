using MinesweeperEF.Business.Cells;
using MinesweeperEF.Business.Models;

namespace MinesweeperEF.Business.Core;

public sealed partial class GameBoard {
    public GameStateDto ExportState() {
        var rows = Settings.Rows;
        var cols = Settings.Columns;

        var cells = new CellDto[rows * cols];
        var k = 0;

        for (var r = 0; r < rows; r++) {
            for (var c = 0; c < cols; c++) {
                var cell = _cells[r, c];
                cells[k++] = new CellDto(cell.IsMine, cell.AdjacentMines, cell.State);
            }
        }

        return new GameStateDto(Settings, FlagsLeft, GameOver, HasWon, HasStarted, cells);
    }

    public static GameBoard ImportState(GameStateDto dto) {
        var board = new GameBoard { Settings = dto.Settings };

        var rows = dto.Settings.Rows;
        var cols = dto.Settings.Columns;
        board._cells = new BoardCell[rows, cols];

        board.FlagsLeft = dto.FlagsLeft;
        board.GameOver = dto.GameOver;
        board.HasWon = dto.HasWon;
        board.HasStarted = dto.HasStarted;

        var k = 0;
        for (var r = 0; r < rows; r++) {
            for (var c = 0; c < cols; c++) {
                var d = dto.Cells[k++];
                board._cells[r, c] = new BoardCell {
                    IsMine = d.IsMine,
                    AdjacentMines = d.AdjacentMines,
                    State = d.State
                };
            }
        }

        return board;
    }
}
