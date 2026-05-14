using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Common.Dto
{
    public class GameResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsGameWon { get; set; }
        public TimeSpan PlayTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int FieldSize { get; set; }
        public int MineCount { get; set; }
        public int FlagsPlaced { get; set; }
        public int CellsRevealed { get; set; }
        public int CellsRemaining { get; set; }
        public int[][] VisibleField { get; set; }
    }
}
