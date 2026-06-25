using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FifteenGame.DataAccess.EF.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MoveCount { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Cell> Cells { get; set; } = new List<Cell>();
    }
}