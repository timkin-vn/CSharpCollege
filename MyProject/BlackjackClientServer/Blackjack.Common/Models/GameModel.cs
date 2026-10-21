using Blackjack.Common.Rules;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blackjack.Common.Models
{
    public class GameModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public GameStatus Status { get; set; } = GameStatus.Playing;

        public string Message { get; set; } = "";

        public List<Card> PlayerCards { get; set; } = new List<Card>();

        public List<Card> DealerCards { get; set; } = new List<Card>();

        // Оставшиеся карты колоды. Клиенту не передаются, чтобы нельзя было подсмотреть.
        [JsonIgnore]
        public List<Card> Deck { get; set; } = new List<Card>();

        [JsonIgnore]
        public bool IsFinished => Status != GameStatus.Playing;

        [JsonIgnore]
        public int PlayerScore => ScoreCalculator.Calculate(PlayerCards);

        [JsonIgnore]
        public int DealerScore => ScoreCalculator.Calculate(DealerCards);
    }
}
