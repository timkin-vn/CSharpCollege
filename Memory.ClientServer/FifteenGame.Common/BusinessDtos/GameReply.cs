using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.BusinessDtos
{
    public class GameReply
    {
        public int GameId { get; set; }

        public int UserId { get; set; }

       
        public int CountPar {  get; set; }
        public int MoveCount { get; set; }

        public DateTime GameStart { get; set; }

        public List<string> Cells { get; set; }
    }
}
