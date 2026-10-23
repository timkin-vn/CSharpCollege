using System.Collections.Generic;

namespace Game2048.DataAccess.EF.Entities
{
    public class GameEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public int MoveCount { get; set; }
        public virtual ICollection<CellEntity> Cells { get; set; }

        public GameEntity()
        {
            Cells = new List<CellEntity>();
        }
    }
}
