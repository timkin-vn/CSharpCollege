using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MinesweeperGame.Common.Definitions;

namespace MinesweeperGame.DataAccess.EF.Entities
{
    [Table("Cells")]
    public class CellEntity
    {
        [Key]
        public int Id { get; set; }

        public Guid GameId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsMine { get; set; }
        public int AdjacentMinesCount { get; set; }
        public CellState State { get; set; } = CellState.Hidden;

        [ForeignKey(nameof(GameId))]
        public virtual GameEntity Game { get; set; }
    }
}
