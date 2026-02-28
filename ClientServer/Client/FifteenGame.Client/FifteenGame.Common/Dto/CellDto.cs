using FifteenGame.Common.Definitions;

namespace FifteenGame.Common.Dto
{
    public class CellDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public CellState State { get; set; }
    }
}