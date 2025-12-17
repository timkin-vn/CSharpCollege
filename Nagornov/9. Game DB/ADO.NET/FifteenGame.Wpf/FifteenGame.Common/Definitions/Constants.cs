using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.Definitions
{
    public static class Constants
    {
        public const int RowCount = 10;
        public const int ColumnCount = 10;
        public const int DefaultMinesCount = 15;
        public const int MinMinesCount = 5;
        public const int MaxMinesCount = 50;
        public const int MineCellValue = -2;     // Значение для ячейки с миной
        public const int FlagCellValue = -3;     // Значение для флага
    }
}