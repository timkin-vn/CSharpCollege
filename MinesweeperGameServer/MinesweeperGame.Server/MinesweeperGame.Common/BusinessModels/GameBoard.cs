using System;
using System.Collections.Generic;
using System.Linq;
using MinesweeperGame.Common.Definitions;

namespace MinesweeperGame.Common.BusinessModels
{
    public class GameBoard
    {
        public Guid Id { get; set; }
        public GameDifficulty Difficulty { get; set; }
        public GameStatus Status { get; set; } = GameStatus.NotStarted;
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int TotalMines { get; set; }
        public int FlagsPlaced { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? FinishedAt { get; set; }
        public TimeSpan? ElapsedTime { get; set; }

        public List<Cell> Cells { get; set; } = new List<Cell>();

        public Cell GetCell(int row, int column) =>
            Cells.FirstOrDefault(c => c.Row == row && c.Column == column);

        public IEnumerable<Cell> GetAdjacentCells(int row, int column)
        {
            for (int dr = -1; dr <= 1; dr++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    if (dr == 0 && dc == 0) continue;
                    var cell = GetCell(row + dr, column + dc);
                    if (cell != null) yield return cell;
                }
            }
        }

        public bool AreAllNonMineCellsRevealed() =>
            Cells.Where(c => !c.IsMine).All(c => c.State == CellState.Revealed);
    }
}
