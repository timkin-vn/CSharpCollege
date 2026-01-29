using System;

namespace FifteenGame.Common.Dtos
{
    public class MoveRequest
    {
        public int GameId { get; set; }
        public string Direction { get; set; } // "up", "down", "left", "right"
    }
}
