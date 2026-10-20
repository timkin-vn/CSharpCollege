using System.Collections.Generic;

namespace FifteenGame.Business.Models
{
    public enum CellState
    {
        Empty,
        X,
        O
    }

    public enum GameResult
    {
        None,
        PlayerWin,
        ComputerWin,
        Draw
    }

    public class GameModel
    {
        public const int RowCount = 3;
        public const int ColumnCount = 3;

        private CellState[,] _cells = new CellState[RowCount, ColumnCount];

        public CellState this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }

        public GameResult Result { get; internal set; } = GameResult.None;

        public List<(int Row, int Column)> WinningCells { get; } = new List<(int, int)>();
    }
}