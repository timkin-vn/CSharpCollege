namespace SeaBattle.Common.Dto
{
    public class GameReply
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MoveCount { get; set; }
        public bool Won { get; set; }
    }
}
