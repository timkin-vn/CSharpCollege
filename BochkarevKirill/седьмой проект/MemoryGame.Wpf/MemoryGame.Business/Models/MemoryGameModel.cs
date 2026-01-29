namespace MemoryGame.Business.Models
{
    public class MemoryGameModel
    {
        public const int RowCount = 4;
        public const int ColumnCount = 4;

        private readonly int[,] _values = new int[RowCount, ColumnCount];
        private readonly bool[,] _revealed = new bool[RowCount, ColumnCount];
        private readonly bool[,] _matched = new bool[RowCount, ColumnCount];

        // -1 означает "не выбрано"
        public int FirstPickRow { get; internal set; }
        public int FirstPickColumn { get; internal set; }
        public int SecondPickRow { get; internal set; }
        public int SecondPickColumn { get; internal set; }

        public bool NeedToHideMismatchedPair { get; internal set; }
        public bool IsGameOver { get; internal set; }
        public bool IsWin { get; internal set; }

        public MemoryGameModel()
        {
            ResetPicks();
        }

        public int this[int row, int column]
        {
            get { return _values[row, column]; }
            internal set { _values[row, column] = value; }
        }

        public bool IsRevealed(int row, int column) { return _revealed[row, column]; }
        internal void SetRevealed(int row, int column, bool value) { _revealed[row, column] = value; }

        public bool IsMatched(int row, int column) { return _matched[row, column]; }
        internal void SetMatched(int row, int column, bool value) { _matched[row, column] = value; }

        internal void ResetPicks()
        {
            FirstPickRow = -1;
            FirstPickColumn = -1;
            SecondPickRow = -1;
            SecondPickColumn = -1;
        }
    }
}
