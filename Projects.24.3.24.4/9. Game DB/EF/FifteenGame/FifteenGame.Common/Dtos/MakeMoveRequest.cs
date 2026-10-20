namespace FifteenGame.Common.Dtos
{
    public class MakeMoveRequest
    {
        public int GameId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}