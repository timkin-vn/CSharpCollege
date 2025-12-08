using _2048Game.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game.Common.BusinessDtos
{
    public class GameReply
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public int MoveCount { get; set; }
        public int[,] Tiles { get; set; }
    }
}
