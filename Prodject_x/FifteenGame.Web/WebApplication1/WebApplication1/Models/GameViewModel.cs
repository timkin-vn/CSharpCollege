using System.Collections.Generic;

namespace MinesweeperWeb.Models
{
    public class GameViewModel
    {
        public List<List<CellViewModel>> Field { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsWin { get; set; }
    }
}