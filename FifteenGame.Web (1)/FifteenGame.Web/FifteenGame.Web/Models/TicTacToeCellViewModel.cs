using FifteenGame.Business.Models;

namespace FifteenGame.Web.Models
{
    public class TicTacToeCellViewModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Player Player { get; set; }

        public string DisplayText
        {
            get
            {
                if (Player == Player.X) return "X";
                if (Player == Player.O) return "O";
                return "";
            }
        }
    }
}
