using System;

namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public const int Size = 4;
        public int[,] Cells { get; private set; }
        public int Score { get; set; }
        public bool IsWin { get; set; }

        public GameModel()
        {
            Cells = new int[Size, Size];
            Score = 0;
            IsWin = false;
        }

        public int this[int row, int col]
        {
            get => Cells[row, col];
            set => Cells[row, col] = value;
        }
    }
}