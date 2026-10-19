using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MinesweeperGame.Common.Definitions;

namespace MinesweeperGame.DataAccess.EF.Entities
{
    [Table("Games")]
    public class GameEntity
    {
        [Key]
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

        public virtual ICollection<CellEntity> Cells { get; set; } = new List<CellEntity>();
    }
}
