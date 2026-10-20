using System;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount { get; set; }

        public int ColumnCount { get; set; }

        public int MineCount { get; set; }

        public int OpenedCount { get; set; }

        public int FlagCount { get; set; }

        public int MoveCount { get; set; }

        public string DifficultyKey { get; set; }

        public string DifficultyName { get; set; }

        public bool SafeFirstMove { get; set; }

        public string FirstMoveName => SafeFirstMove ? "Безопасный" : "Опасный";

        public bool IsWin { get; set; }

        public bool IsLose { get; set; }

        public bool IsGameFinished => IsWin || IsLose;

        public string StatusText
        {
            get
            {
                if (IsWin)
                {
                    return "Победа. Все безопасные клетки открыты.";
                }

                if (IsLose)
                {
                    return "Поражение. Открыта клетка с миной.";
                }

                return "Игра идёт.";
            }
        }

        public CellViewModel[,] Cells { get; set; }
    }
}
