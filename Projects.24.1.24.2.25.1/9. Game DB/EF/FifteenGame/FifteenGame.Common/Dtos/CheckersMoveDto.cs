using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FifteenGame.Common.Dtos
{
    public class CheckersMoveDto
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int MoveNumber { get; set; }
        public int FromRow { get; set; }
        public int FromCol { get; set; }
        public int ToRow { get; set; }
        public int ToCol { get; set; }
        public bool IsCapture { get; set; }
        public int? CapturedRow { get; set; }
        public int? CapturedCol { get; set; }
        public bool PromotedToKing { get; set; }
        public DateTime MoveTime { get; set; }
    }
}