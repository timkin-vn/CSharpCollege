using System;

namespace LightsOutGame.Models
{
    public class GameModel
    {
        private readonly Random _random = new Random();

        public int Size { get; }
        public CellModel[,] Cells { get; }

        public GameModel(int size = 5)
        {
            Size = size;
            Cells = new CellModel[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    Cells[i, j] = new CellModel();

            Initialize();
        }

        public void Initialize()
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    Cells[i, j].IsOn = _random.Next(2) == 1;
        }

        public void MakeMove(int row, int col)
        {
            Toggle(row, col);
            Toggle(row - 1, col);
            Toggle(row + 1, col);
            Toggle(row, col - 1);
            Toggle(row, col + 1);
        }

        private void Toggle(int row, int col)
        {
            if (row >= 0 && row < Size && col >= 0 && col < Size)
                Cells[row, col].Toggle();
        }

        public bool IsWin()
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    if (Cells[i, j].IsOn)
                        return false;

            return true;
        }
    }
}
