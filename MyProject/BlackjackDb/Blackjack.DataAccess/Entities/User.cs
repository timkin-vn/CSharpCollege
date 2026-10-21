using System.Collections.Generic;

namespace Blackjack.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public List<Game> Games { get; set; } = new List<Game>();
    }
}
