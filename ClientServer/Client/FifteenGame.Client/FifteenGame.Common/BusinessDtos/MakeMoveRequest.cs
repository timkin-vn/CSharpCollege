namespace FifteenGame.Common.BusinessDtos
{
    public class MakeMoveRequest
    {
        public int GameId { get; set; }
        public int UserId { get; set; } 
        public int X { get; set; }
        public int Y { get; set; }
    }
}