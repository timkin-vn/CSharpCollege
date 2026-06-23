namespace Minesweeper.Common.Dto
{

    public class GameReply
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsLost { get; set; }
        public int MoveCount { get; set; }
        public int MinesRemaining { get; set; }
        public int[] Cells { get; set; }
        public int Size { get; set; }
    }
}
