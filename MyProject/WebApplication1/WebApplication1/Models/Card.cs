namespace BlackjackMVC.Models
{
    public class Card
    {
        public string Suit { get; set; }
        public string Rank { get; set; }

        public int Value
        {
            get
            {
                if (Rank == "A") return 11;
                if (Rank == "K" || Rank == "Q" || Rank == "J")
                    return 10;

                return int.Parse(Rank);
            }
        }
    }
}