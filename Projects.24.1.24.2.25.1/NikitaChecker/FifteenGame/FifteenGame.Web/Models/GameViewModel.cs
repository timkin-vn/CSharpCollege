using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public CellViewModel[,] Cells { get; set; }
        public string CurrentPlayer { get; set; }
        public bool IsGameOver { get; set; }
        public string Winner {  get; set; }
    }
}
