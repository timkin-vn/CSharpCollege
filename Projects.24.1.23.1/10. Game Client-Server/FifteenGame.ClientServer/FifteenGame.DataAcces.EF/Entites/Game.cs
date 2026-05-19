using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAcces.EF.Entites
{
    public class Game
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MoveCount { get; set; }

        [Required]
        public User User { get; set; }

        public List<GameCell> GameCells { get; set; }
    }
}
