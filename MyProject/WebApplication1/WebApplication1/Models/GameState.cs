using System.Text.Json.Serialization;

namespace BlackjackMVC.Models
{
    public class GameState
    {
        public List<Card> PlayerCards { get; set; } = new();

        public List<Card> DealerCards { get; set; } = new();

        // Оставшиеся карты колоды хранятся вместе с игрой, чтобы карты не повторялись
        public List<Card> DeckCards { get; set; } = new();

        // true — партия закончена, ходить больше нельзя
        public bool IsOver { get; set; }

        public string Message { get; set; } = "";

        [JsonIgnore]
        public int PlayerPoints => GetPoints(PlayerCards);

        [JsonIgnore]
        public int DealerPoints => GetPoints(DealerCards);

        // Очки дилера, которые видит игрок (пока партия идёт — только первая карта)
        [JsonIgnore]
        public int DealerVisiblePoints => IsOver ? DealerPoints : GetPoints(DealerCards.Take(1).ToList());

        public static int GetPoints(List<Card> cards)
        {
            int total = cards.Sum(c => c.Value);
            int aces = cards.Count(c => c.Rank == "A");

            while (total > 21 && aces > 0)
            {
                total -= 10;
                aces--;
            }

            return total;
        }
    }
}
