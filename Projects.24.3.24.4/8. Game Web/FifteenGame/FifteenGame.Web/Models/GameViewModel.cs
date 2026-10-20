using System.Collections.Generic;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public List<CellViewModel> Cells { get; set; } = new List<CellViewModel>();
        public string StatusText { get; set; }
        public bool IsGameOver { get; set; }
        public int PlayerScore { get; set; }
        public int ComputerScore { get; set; }
    }
}