using FifteenGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.Entites
{
    public class Game
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Score { get; set; }

        public bool IsGameOver { get; set; }

        public bool HasWon { get; set; }

        public User User { get; set; }

        public List<GameCell> GameCells { get; set; }
    }
}
