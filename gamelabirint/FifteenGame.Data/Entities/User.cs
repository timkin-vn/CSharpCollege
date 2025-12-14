using System.Collections.Generic;

namespace FifteenGame.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 


        public virtual ICollection<GameState> GameStates { get; set; }
    }
}