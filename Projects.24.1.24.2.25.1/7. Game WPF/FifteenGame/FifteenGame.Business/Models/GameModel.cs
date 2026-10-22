using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 10;
        public const int ColumnCount = 10;

        public const int FreeCellValue = -1; // Оставлено для совместимости

        private readonly MoveDirection[,] _cells = new MoveDirection[RowCount, ColumnCount];

        public MoveDirection this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public List<ShipModel> Ships { get; } = new List<ShipModel>();

        public int FreeCellRow { get; set; }
        public int FreeCellColumn { get; set; }
    }

    public class ShipModel
    {
        public List<(int Row, int Column)> Coordinates { get; } = new List<(int Row, int Column)>();

        public ShipModel() { }

        public ShipModel(IEnumerable<(int Row, int Column)> coordinates)
        {
            Coordinates.AddRange(coordinates);
        }
    }
}

