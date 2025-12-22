using System.Text.Json.Serialization;

namespace Minesweeper.Common.Models
{
    public class Field
    {
        public int Size { get; set; }
        public bool[][] Mines { get; set; }
        public bool[][] Revealed { get; set; }
        public bool[][] Flag { get; set; }
        public int[][] Num { get; set; }
        public int MineCount { get; set; }
        public bool GameOver { get; set; }
        public bool GameWon { get; set; }
    }
}