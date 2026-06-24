using System.Collections.Generic;

namespace FifteenGame.DataAccess.EF.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }    
        public int MoveCount { get; set; }
        public bool IsWin { get; set; }     

        public User User { get; set; }
        public List<Cell> Cells { get; set; }
    }
}