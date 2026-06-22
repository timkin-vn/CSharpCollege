namespace Game2048.Common.Dto
{

    public class GameReply
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public int MoveCount { get; set; }
        public int[] Cells { get; set; }
        public int Size { get; set; }
    }
}
