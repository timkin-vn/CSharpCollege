using LightsOutGame.Common.Definitions;

namespace LightsOutGame.Common.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MoveCount { get; set; }

        public int[,] Cells { get; set; } = new int[Constants.RowCount, Constants.ColumnCount];
    }
}
