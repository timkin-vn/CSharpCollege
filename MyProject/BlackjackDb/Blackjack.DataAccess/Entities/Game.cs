using System;

namespace Blackjack.DataAccess.Entities
{
    public class Game
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedAt { get; set; }

        // GameStatus: 0 — идёт, 1 — победа игрока, 2 — победа дилера, 3 — ничья
        public int Status { get; set; }

        public string Message { get; set; } = "";

        // Карты хранятся строкой вида "14:0;10:3" (достоинство:масть)
        public string PlayerCards { get; set; } = "";

        public string DealerCards { get; set; } = "";

        public string DeckCards { get; set; } = "";
    }
}
