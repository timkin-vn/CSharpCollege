using System.Collections.Generic;
using FifteenGame.Common.Dto;

namespace FifteenGame.Common.BusinessDtos
{
    public class StartGameReply
    {
        public int GameId { get; set; } 
        public List<CellDto> PlayerField { get; set; }

        public List<CellDto> EnemyField { get; set; }
    }
}