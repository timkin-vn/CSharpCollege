using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game2048.WEB.Models
{
    public class GameViewModel
    {
        public List<CellViewModel> Cells { get; set; }

        public bool IsGameOver { get; set; }

        public GameViewModel()
        {
            Cells = new List<CellViewModel>();
        }
    }
}