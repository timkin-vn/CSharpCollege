using System.Collections.Generic;
using FifteenGame.Common.Dto;

namespace FifteenGame.Common.BusinessModels
{
    public class GameModel
    {
        public int Id { get; set; }
        public List<CellDto> PlayerCells { get; set; }
        public List<CellDto> EnemyCells { get; set; }
        public bool IsPlayerTurn { get; set; }
        public bool IsGameOver { get; set; }
        public string Winner { get; set; } 
    }
}