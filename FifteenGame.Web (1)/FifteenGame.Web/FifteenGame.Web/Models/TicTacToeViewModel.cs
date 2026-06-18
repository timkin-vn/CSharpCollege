using System.Collections.Generic;
using FifteenGame.Business.Models;

namespace FifteenGame.Web.Models
{
    public class TicTacToeViewModel
    {
        public List<TicTacToeCellViewModel> Cells { get; set; } = new List<TicTacToeCellViewModel>();
        public Player CurrentPlayer { get; set; }
        public GameState GameState { get; set; }
        public Player Winner { get; set; }
        public string GameStatus { get; set; }
    }
}
