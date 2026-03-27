using System.Collections.Generic;
using FifteenGame.Common.Dto;

namespace FifteenGame.Common.BusinessDtos
{
    public class GameReply
    {
        public bool IsGameOver { get; set; }
        public string Winner { get; set; }

        
        public List<CellDto> ChangedEnemyCells { get; set; } = new List<CellDto>();
        public List<CellDto> ChangedPlayerCells { get; set; } = new List<CellDto>();
    }
}