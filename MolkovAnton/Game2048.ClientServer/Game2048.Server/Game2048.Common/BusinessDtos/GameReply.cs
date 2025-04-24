using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Common.BusinessDtos
{
    public class GameReply
    {
        public int GameId { get; set; }

        public int UserId { get; set; }

        public int FreeCellRow { get; set; }

        public int FreeCellColumn { get; set; }

        public int MoveCount { get; set; }

        public DateTime GameStart { get; set; }

        public List<int> Cells { get; set; }
    }
}
