using System.Collections.Generic;

namespace Игра.DataAccess.EF.Entites
{
    public class Game
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MoveCount { get; set; }

        public User User { get; set; }

        public List<GameCell> GameCells { get; set; }
    }
}