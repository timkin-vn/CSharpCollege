namespace Minesweeper.Common.Dto
{
    public class CellActionRequest
    {
        public int UserId { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public int Action { get; set; }
    }
}
