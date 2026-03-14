using FifteenGame.Common.Enums;

namespace FifteenGame.Common.Dto
{
    public class GameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public int MovesLeft { get; set; }
        public GameMode Mode { get; set; }
        public GameState State { get; set; }

        public int[] Gems { get; set; }
        public bool[] Matched { get; set; }

        public int RowCount { get; set; } = 8;
        public int ColumnCount { get; set; } = 8;

        public int SelectedRow { get; set; } = -1;
        public int SelectedColumn { get; set; } = -1;
    }
}