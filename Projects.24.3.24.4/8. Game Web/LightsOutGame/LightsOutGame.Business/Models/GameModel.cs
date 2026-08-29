namespace LightsOutGame.Business.Models
{
    // Хранит состояние игры (аналог GameModel у преподавателя).
    // Сериализуется в Session, поэтому все данные — публичные свойства.
    [Serializable]
    public class GameModel
    {
        public const int RowCount = 5;
        public const int ColumnCount = 5;

        // true = лампочка горит, false = выключена
        public bool[][] Cells { get; set; }

        public GameModel()
        {
            Cells = new bool[RowCount][];
            for (int row = 0; row < RowCount; row++)
            {
                Cells[row] = new bool[ColumnCount];
            }
        }

        // Удобный индексатор, как model[row, column] у преподавателя
        public bool this[int row, int column]
        {
            get => Cells[row][column];
            set => Cells[row][column] = value;
        }
    }
}
