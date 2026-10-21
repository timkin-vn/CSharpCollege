using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Common.Definitions
{
    public static class Constants
    {
        public const int RowCount = 4;

        public const int ColumnCount = 4;

        public const int FreeCellValue = 0;

        public const int EasyRows = 2;

        public const int EasyCols = 4; 

        public const int MediumRows = 3;

        public const int MediumCols = 4; 

        public const int HardRows = 4;

        public const int HardCols = 4;  

        public const int BaseMatchPoints = 100;

        public const int DefaultHintsCount = 2; 

        public const int HintPenaltyPoints = 50; 

        public static void GetGridSize(int difficulty, out int rows, out int cols)
        {
            switch (difficulty)
            {
                case 1:
                    rows = EasyRows;
                    cols = EasyCols;
                    break;
                case 2:
                    rows = MediumRows;
                    cols = MediumCols;
                    break;
                default:
                    rows = HardRows;
                    cols = HardCols;
                    break;
            }
        }
    }
}
