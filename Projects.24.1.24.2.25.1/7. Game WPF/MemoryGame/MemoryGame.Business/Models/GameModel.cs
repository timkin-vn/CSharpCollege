namespace MemoryGame.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 4;

        public const int ColumnCount = 4;

        public const int PairCount = RowCount * ColumnCount / 2;

        private readonly int[,] _values = new int[RowCount, ColumnCount];

        private readonly CardState[,] _states = new CardState[RowCount, ColumnCount];

        // Номер картинки на карточке (1..PairCount)
        public int this[int row, int column]
        {
            get => _values[row, column];
            internal set => _values[row, column] = value;
        }

        public CardState GetState(int row, int column) => _states[row, column];

        internal void SetState(int row, int column, CardState state) => _states[row, column] = state;

        // Количество попыток (открытых пар карточек)
        public int MoveCount { get; internal set; }

        public int MatchedPairCount { get; internal set; }
    }
}
