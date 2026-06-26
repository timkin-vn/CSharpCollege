using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.Entities
{
    public class Cell
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public int PeopleCount { get; set; }

        public bool HasShop { get; set; }

        public bool IsVeggie { get; set; }

        public bool IsRevealed { get; set; }

        public Game Game { get; set; }
    }
}
