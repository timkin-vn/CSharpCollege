using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Common.Dto
{
    public class CreateGameRequest
    {
        public int UserId { get; set; }
        public int Size { get; set; } = 10;
        public int MineCount { get; set; } = 15;
    }
}
