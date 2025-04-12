using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.BusinessModels
{
    public class Units
    {
        public const int RowCount = 8;

        public const int ColumnCount = 8;
        public int X { get; set; }
        public int Y { get; set; }
        public Units[,] _cells = new Units[RowCount, ColumnCount];
        public Units units { get; set; }
        public int FreeCellRow { get; set; }

        public int FreeCellColumn { get; set; }
        public string Symbol { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public bool IsSelected { get; set; }
        public int Width { get; set; } = 1;
        public int Height { get; set; } = 1;
        public Units(string symbol, int hp, int attack, int x, int y, UnitType type)
        {

            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentException("Символ не может быть null или пустым.", nameof(symbol));
            }


            if (hp < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(hp), "Здоровье не может быть отрицательным.");
            }

            if (attack < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(attack), "Атака не может быть отрицательной.");
            }
            Symbol = symbol;
            Hp = hp;
            Attack = attack;
            X = x;
            Y = y;
            Type = type;
            IsSelected = false;


            if (type == UnitType.Boss)
            {
                Width = 2;
                Height = 4;
            }
            else
            {
                Width = 1; 
                Height = 1;
            }
        }
        public Units this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }
        public enum UnitType
        {
            None,
            Dragon,
            Medic,
            Knight,
            King,
            Boss
        }

        public UnitType Type { get; set; }
    }
}
