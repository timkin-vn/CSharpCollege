namespace FifteenGame.Common.Dtos
{
    public class GameReply
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string BoardState { get; set; }
        public int Result { get; set; }
        public int PlayerScore { get; set; }
        public int ComputerScore { get; set; }
    }
}