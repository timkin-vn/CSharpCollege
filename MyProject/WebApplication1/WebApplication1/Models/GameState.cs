namespace BlackjackMVC.Models
{
    public class GameState
    {
        public List<Card> PlayerCards { get; set; } = new();

        public List<Card> DealerCards { get; set; } = new();

        public string Message { get; set; } = "";
    }
}