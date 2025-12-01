using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAcces.EF.Entites
{
    public class GameCell
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public int Value { get; set; }

        [Required]
        public Game Game { get; set; }
    }
}
