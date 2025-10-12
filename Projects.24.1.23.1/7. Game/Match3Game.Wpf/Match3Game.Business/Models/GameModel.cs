using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 8;
        public const int ColumnCount = 8;
        public const int GemTypeCount = 5; // фишки

        private int[,] _cells = new int[RowCount, ColumnCount];

        public int this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }

        public GameModel()
        {
            GenerateInitialBoard();
        }

        private void GenerateInitialBoard()
        {
            var rnd = new Random();

            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    _cells[row, column] = rnd.Next(GemTypeCount);
                }
            }
        }
    }
}
