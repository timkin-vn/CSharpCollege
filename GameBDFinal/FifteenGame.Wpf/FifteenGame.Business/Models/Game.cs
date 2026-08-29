using System;
using System.Linq;

namespace FifteenGame.Business.Models
{
    public class Game
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public Field PlayerField { get; set; } = new Field();
        public Field OpponentField { get; set; } = new Field();
        public bool IsPlayerTurn { get; set; } = true;

        public bool IsOver
        {
            get { return PlayerField.Ships.All(s => s.IsSunk) || OpponentField.Ships.All(s => s.IsSunk); }
        }

        public string Winner
        {
            get
            {
                if (!IsOver) return null;
                return PlayerField.Ships.All(s => s.IsSunk) ? "Opponent" : "Player";
            }
        }
    }
}