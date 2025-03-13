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
        public const int RowCount = 8;

        public const int ColumnCount = 8;


        public int X { get; set; }
        public int Y { get; set; }
        private GameModel[,] _cells = new GameModel[RowCount, ColumnCount];

        public string Symbol { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public bool IsSelected { get; set; }
        public GameModel this[int row, int column]
        {
            get
            {

                if (row < 0 || row >= RowCount || column < 0 || column >= ColumnCount)
                {
                    throw new IndexOutOfRangeException($"Индексы ({row}, {column}) находятся вне границ массива.");
                }
                return _cells[row, column];
            }
            set
            {

                if (row < 0 || row >= RowCount || column < 0 || column >= ColumnCount)
                {
                    throw new IndexOutOfRangeException($"Индексы ({row}, {column}) находятся вне границ массива.");
                }
                _cells[row, column] = value;
            }
        }

        public int FreeCellRow { get; internal set; }

        public int FreeCellColumn { get; internal set; }

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
        public int Width { get; set; } = 1;
        public int Height { get; set; } = 1;

        public GameModel(string symbol, int hp, int attack, int x, int y, UnitType type)
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
                Width = 1; // Устанавливаем значения по умолчанию для других типов
                Height = 1;
            }
        }

        public int Property1 { get; set; }
        public string Property2 { get; set; }


    }
}
