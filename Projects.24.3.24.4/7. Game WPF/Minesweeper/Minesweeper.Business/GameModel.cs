namespace Minesweeper.Business
{

    public class GameModel
    {
        public const int Size = 9;
        public const int MineCount = 10;

        public Cell[,] Field { get; set; }
        public bool MinesPlaced { get; set; }
        public bool IsLost { get; set; }
        public int MoveCount { get; set; }

        public GameModel()
        {
            Field = new Cell[Size, Size];
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    Field[r, c] = new Cell();
            MinesPlaced = false;
            IsLost = false;
            MoveCount = 0;
        }
    }
}
