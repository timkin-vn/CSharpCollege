using System.Collections.Generic;

namespace FifteenGame.Business.Models
{
    public class Field
    {
        public const int Size = 10;
        public Cell[,] Cells { get; private set; }
        public List<Ship> Ships { get; private set; }

        public Field()
        {
            Cells = new Cell[Size, Size];
            Ships = new List<Ship>();
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Cells[x, y] = new Cell { X = x, Y = y };
                }
            }
        }
    }
}