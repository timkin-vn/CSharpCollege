using System.Collections.Generic;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public List<CellViewModel> Cells { get; set; } = new List<CellViewModel>();
        public string Status { get; set; }
    }
}