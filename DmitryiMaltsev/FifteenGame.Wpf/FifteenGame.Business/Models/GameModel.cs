﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public const int RowCount = 4;

        public const int ColumnCount = 4;

        public const int FreeCellValue = -1;

        private int[,] _cells = new int[RowCount, ColumnCount]; // массив Enum

        public int this[int row, int column]
        {
            get => _cells[row, column];
            internal set => _cells[row, column] = value;
        }

        public int FreeCellRow { get; internal set; }

        public int FreeCellColumn { get; internal set; }
    }
}
