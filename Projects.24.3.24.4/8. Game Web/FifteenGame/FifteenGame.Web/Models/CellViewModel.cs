using FifteenGame.Business.Models;

namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public CellState State { get; set; }
        public bool IsWinningCell { get; set; }

        public string Text => State == CellState.Empty ? "" : State.ToString();
    }
}