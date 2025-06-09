namespace CardFile.Business.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CardNumber { get; set; }
        public int BonusPoints { get; set; }
        public string CardType { get; set; }
    }
}