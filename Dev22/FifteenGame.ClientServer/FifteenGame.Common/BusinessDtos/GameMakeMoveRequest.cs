﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.BusinessDtos
{
    public class GameMakeMoveRequest
    {
        public int GameId { get; set; }

        public string Direction { get; set; }
    }
}
