using FifteenGame.Common.Definitions;

namespace FifteenGame.Common.BusinessModels
{
    public class GameModel
    {
        private int[,] _cells = new int[Constants.RowCount, Constants.ColumnCount];

        public int Id { get; set; }

        public int UserId { get; set; }

        public int Score { get; set; }

        public int BestTile { get; set; }

        public int this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }
    }
}