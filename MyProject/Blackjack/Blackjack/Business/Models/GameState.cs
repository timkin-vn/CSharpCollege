using System.Collections.Generic;

namespace BlackjackGame.Business.Models
{
    public class GameState
    {
        public Deck Deck { get; set; } = new Deck();
        public List<Card> PlayerHand { get; set; } = new List<Card>();
        public List<Card> DealerHand { get; set; } = new List<Card>();

        public bool IsPlayerTurn { get; set; } = true;
        public GameStatus Status { get; set; } = GameStatus.Starting;
        public string Message { get; set; } = "";

        public void Reset()
        {
            Deck = new Deck();
            PlayerHand.Clear();
            DealerHand.Clear();
            IsPlayerTurn = true;
            Status = GameStatus.Playing;
            Message = "Ваш ход";
        }
    }

    public enum GameStatus
    {
        Starting,
        Playing,
        PlayerWin,
        DealerWin,
        Draw,
        Bust
    }
}