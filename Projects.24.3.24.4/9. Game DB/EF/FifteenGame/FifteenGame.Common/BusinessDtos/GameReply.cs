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

        public int MoveCount { get; set; }

        public int FreeCellRow { get; set; }

        public int FreeCellColumn { get; set; }

        public int[] Cells { get; set; }

        public int Difficulty { get; set; } = 1;

        public int Theme { get; set; } = 1;

        public int Score { get; set; }

        public int ComboStreak { get; set; }

        public int HintsLeft { get; set; }

        public int ElapsedSeconds { get; set; }
    }
}
