namespace Minesweeper.DataAccess.EF.Entities
{
    public class CellEntity
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsMine { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public int AdjacentMines { get; set; }
    }
}
