using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public const int Size = 4;
        private static readonly Random _random = new Random();

        public int[,] Cells { get; private set; } = new int[Size, Size];

        public int this[int row, int column]
        {
            get => Cells[row, column];
            set => Cells[row, column] = value;
        }

        public bool HasEmptyCells()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    if (Cells[row, column] == 0)
                        return true;
                }
            }
            return false;
        }

        public void AddRandomTile()
        {
            if (!HasEmptyCells()) return;

            while (true)
            {
                int row = _random.Next(Size);
                int column = _random.Next(Size);
                if (Cells[row, column] == 0)
                {
                    Cells[row, column] = _random.Next(10) < 9 ? 2 : 4;
                    break;
                }
            }
        }

        public void Reset()
        {
            for (int row = 0; row < Size; row++)
                for (int column = 0; column < Size; column++)
                    Cells[row, column] = 0;

            AddRandomTile();
            AddRandomTile();
        }
    }
}
