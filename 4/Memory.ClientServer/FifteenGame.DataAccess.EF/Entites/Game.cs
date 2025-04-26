using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.Entites
{
    public class Game
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MoveCount { get; set; }

        public DateTime GameStart { get; set; }

        [Required]
        public User User { get; set; }

        public List<Cell> Cells { get; set; }
    }
}
