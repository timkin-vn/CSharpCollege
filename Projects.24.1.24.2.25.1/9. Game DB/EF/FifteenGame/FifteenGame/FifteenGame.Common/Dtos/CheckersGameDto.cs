using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FifteenGame.Common.Dtos
{
    public class CheckersGameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public int CurrentPlayer { get; set; }
        public bool IsFinished { get; set; }
        public int? Winner { get; set; }
        public string GameStateJson { get; set; }
        public List<CheckersMoveDto> Moves { get; set; } = new List<CheckersMoveDto>();
    }
}