using System;
using MinesweeperGame.Common.Definitions;

namespace MinesweeperGame.Common.BusinessDtos
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public GameDifficulty Difficulty { get; set; }
        public GameStatus Status { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int TotalMines { get; set; }
        public int FlagsPlaced { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public double? ElapsedSeconds { get; set; }
    }
}
