using MemoryGame.Common.Definitions;

namespace MemoryGame.Common.BusinessModels
{
    public class GameModel
    {
        private readonly int[,] _values = new int[Constants.RowCount, Constants.ColumnCount];

        private readonly CardState[,] _states = new CardState[Constants.RowCount, Constants.ColumnCount];

        public int Id { get; set; }

        public int UserId { get; set; }

        // Номер картинки на карточке (1..PairCount)
        public int this[int row, int column]
        {
            get => _values[row, column];
            set => _values[row, column] = value;
        }

        public CardState GetState(int row, int column) => _states[row, column];

        public void SetState(int row, int column, CardState state) => _states[row, column] = state;

        // Количество попыток (открытых пар карточек)
                public int MoveCount { get; set; }

        // Вычисляется по состояниям карточек, т.к. в БД и в DTO не хранится
        public int MatchedPairCount
        {
            get
            {
                int count = 0;
                foreach (var state in _states)
                {
                    if (state == CardState.Matched)
                    {
                        count++;
                    }
                }

                return count / 2;
            }
        }
    }
}
