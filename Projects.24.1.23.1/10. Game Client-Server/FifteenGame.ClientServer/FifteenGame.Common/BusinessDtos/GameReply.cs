using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.BusinessDtos
{
    public class GameReply
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MatchesCount { get; set; }
        public bool IsFinished { get; set; }
        public int[] Cells { get; set; }
    }
}
