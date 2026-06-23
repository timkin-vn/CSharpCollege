namespace Minesweeper.Common.Models
{

    public class GameModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Cell[,] Field { get; set; }
        public bool MinesPlaced { get; set; }
        public bool IsLost { get; set; }
        public int MoveCount { get; set; }

        public GameModel()
        {
            Field = new Cell[Constants.Size, Constants.Size];
            for (int r = 0; r < Constants.Size; r++)
                for (int c = 0; c < Constants.Size; c++)
                    Field[r, c] = new Cell();
        }
    }
}
