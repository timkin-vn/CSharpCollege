using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.BusinessDtos
{
    public class MakeMoveRequest
    {
        public int GameId { get; set; }

        public string Direction { get; set; }

        public int Row { get; set; } = -1;

        public int Column { get; set; } = -1;

        public bool UseHint { get; set; }

        public int ElapsedSeconds { get; set; }
    }
}
