using TwentyFortyEight.Common.Definitions;

namespace TwentyFortyEight.Common.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public int BestTile { get; set; }
        public bool IsWon { get; set; }

        public int[,] Cells { get; }
            = new int[Constants.RowCount, Constants.ColumnCount];
    }
}
