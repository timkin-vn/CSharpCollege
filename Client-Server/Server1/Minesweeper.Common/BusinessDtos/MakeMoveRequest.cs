using System.ComponentModel.DataAnnotations;

namespace Minesweeper.Common.BusinessDtos
{
    public class MakeMoveRequest
    {
        public int GameId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string Action { get; set; }
    }
}