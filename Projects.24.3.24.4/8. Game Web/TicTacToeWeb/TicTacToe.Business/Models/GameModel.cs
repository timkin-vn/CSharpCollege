using System;

namespace TicTacToe.Business.Models
{
    public enum CellValue
    {
        Empty,
        X,
        O
    }

    public enum GameResult
    {
        InProgress,
        Draw,
        XWins,
        OWins
    }

    public class GameModel
    {
        // Внутренний массив поля
        private readonly CellValue[,] _cells = new CellValue[3, 3];

        // ИСПРАВЛЕНО: Добавлено слово static, чтобы GameService мог читать их напрямую
        public static int RowCount => 3;
        public static int ColumnCount => 3;

        // Свойства с открытым set; для веб-контроллера
        public CellValue CurrentPlayer { get; set; } = CellValue.X;
        public GameResult Result { get; set; } = GameResult.InProgress;
        public int MovesCount { get; set; } = 0;

        // Индексатор поля
        public CellValue this[int row, int col]
        {
            get => _cells[row, col];
            set => _cells[row, col] = value;
        }

        // Метод очистки поля
        public void Reset()
        {
            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    _cells[r, c] = CellValue.Empty;
                }
            }
            CurrentPlayer = CellValue.X;
            Result = GameResult.InProgress;
            MovesCount = 0;
        }
    }
}