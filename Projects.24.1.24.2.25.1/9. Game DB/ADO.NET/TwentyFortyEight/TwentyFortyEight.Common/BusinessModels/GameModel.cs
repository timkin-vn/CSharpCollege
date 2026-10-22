using TwentyFortyEight.Common.Definitions;

namespace TwentyFortyEight.Common.BusinessModels
{
    public class GameModel
    {
        
        public int[,] Cells { get; set; } = new int[Constants.RowCount, Constants.ColumnCount];

        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public int BestTile { get; set; }
        public bool IsWon { get; set; }

        
        public int this[int row, int column]
        {
            get => Cells[row, column];
            set => Cells[row, column] = value;
        }
    }
}