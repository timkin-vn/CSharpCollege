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

        public int Money { get; set; }

        public int TurnsPlayed { get; set; }

        public User User { get; set; }

        public List<Cell> Cells { get; set; }
    }
}