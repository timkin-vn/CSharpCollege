using MemoryGame.Common.Definitions;

namespace MemoryGame.Common.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MoveCount { get; set; }

                // Номера картинок на карточках
        public int[,] Cells { get; } = new int[Constants.RowCount, Constants.ColumnCount];

        // Состояния карточек (CardState)
        public int[,] States { get; } = new int[Constants.RowCount, Constants.ColumnCount];
    }
}
