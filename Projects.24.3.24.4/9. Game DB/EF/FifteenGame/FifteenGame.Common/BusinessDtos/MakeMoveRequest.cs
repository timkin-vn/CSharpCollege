using System;

namespace FifteenGame.Common.BusinessDtos
{
    public class MakeMoveRequest
    {
        public int GameId { get; set; }

        // Координаты клетки, куда игрок ставит новый ларёк
        public int Row { get; set; }
        public int Column { get; set; }
    }
}