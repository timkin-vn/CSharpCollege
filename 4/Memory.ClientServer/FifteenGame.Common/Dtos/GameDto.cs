using FifteenGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int[,] Field { get; set; } = new int[4, 4];

        public int Score { get; set; }

        public int MoveCount { get; set; }
        public DateTime GameStart { get; set; }
    }
}
