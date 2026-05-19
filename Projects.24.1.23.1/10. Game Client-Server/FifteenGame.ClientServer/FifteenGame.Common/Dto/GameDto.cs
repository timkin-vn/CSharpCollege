using FifteenGame.Common.Definitions;

namespace FifteenGame.Common.Dto
{
    public class GameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int[,] Cells { get; } = new int[Constants.RowCount, Constants.ColumnCount];
        public int MatchesCount { get; set; }
        public bool IsFinished { get; set; }
    }
}