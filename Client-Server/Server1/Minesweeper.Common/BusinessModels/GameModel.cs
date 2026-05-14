using System;

namespace Minesweeper.Common.BusinessModels
{
    public class GameModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public TimeSpan PlayTime { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsGameWon { get; set; }
        public string Status { get; set; }
        public string FieldData { get; set; }
        public int FieldSize { get; set; }
        public int MineCount { get; set; }
        public int FlagsPlaced { get; set; }
        public int CellsRevealed { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}