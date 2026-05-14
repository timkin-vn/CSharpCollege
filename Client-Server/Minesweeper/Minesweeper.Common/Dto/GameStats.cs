using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Common.Dto
{
    public class GameStats
    {
        public int FieldSize { get; set; }
        public int MineCount { get; set; }
        public int FlagsPlaced { get; set; }
        public int CellsRevealed { get; set; }
        public int CellsRemaining { get; set; }

        public override string ToString()
        {
            return $"Размер: {FieldSize}x{FieldSize} | " +
                   $"Мины: {MineCount} | " +
                   $"Флаги: {FlagsPlaced} | " +
                   $"Открыто: {CellsRevealed} | " +
                   $"Осталось: {CellsRemaining}";
        }
    }
}
