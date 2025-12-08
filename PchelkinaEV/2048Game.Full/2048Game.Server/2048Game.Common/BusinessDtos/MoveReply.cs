using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game.Common.BusinessDtos
{
    public class MoveReply
    {
        public bool Success { get; set; }
        public int[,] Tiles { get; set; }
        public bool GameOver { get; set; }
        public int MoveCount { get; set; }
    }
}
