using System.Collections.Generic;

namespace Minesweeper.DataAccess.EF.Entities
{
    public class GameEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsLost { get; set; }
        public bool MinesPlaced { get; set; }
        public int MoveCount { get; set; }
        public virtual ICollection<CellEntity> Cells { get; set; }

        public GameEntity()
        {
            Cells = new List<CellEntity>();
        }
    }
}
