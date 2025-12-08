using System.Collections.Generic;

namespace LabyrinthGame.Models
{

    public class CellViewModel
    {
        public string CssClass { get; set; } // wall, path, player, exit
        public string Symbol { get; set; }
    }


    public class GameViewModel
    {
        public List<List<CellViewModel>> Grid { get; set; }
        public int Moves { get; set; }
        public string Message { get; set; }
        public bool IsFinished { get; set; }
    }
}