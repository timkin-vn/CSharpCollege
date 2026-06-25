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

    [Serializable]
    public class MinesweeperGameModel
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Mines { get; set; }
        public int OpenedCount { get; set; }
        public int FlagCount { get; set; }
        public int MoveCount { get; set; }
        public string DifficultyKey { get; set; }
        public string DifficultyName { get; set; }
        public bool SafeFirstMove { get; set; }
        public bool MinesPlaced { get; set; }
        public bool IsWin { get; set; }
        public bool IsLose { get; set; }
        public MinesweeperCellModel[,] Cells { get; set; }
    }

    [Serializable]
    public class MinesweeperCellModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsMine { get; set; }
        public bool IsOpened { get; set; }
        public bool IsFlagged { get; set; }
        public int MinesAround { get; set; }
    }
}
