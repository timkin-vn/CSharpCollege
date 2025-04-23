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

        public int ScoreCount { get; set; }

        public DateTime GameBegin { get; set; }

        public int FreeCellRow { get; set; }

        public int FreeCellColumn { get; set; }

        public List<int> Cells { get; set; }
    }
}
