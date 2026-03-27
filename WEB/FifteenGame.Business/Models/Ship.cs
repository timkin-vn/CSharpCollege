namespace FifteenGame.Business.Models
{
    public class Ship
    {
        public ShipType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsHorizontal { get; set; }
        public int Hits { get; set; } = 0;

        public bool IsKilled => Hits >= (int)Type;
    }
}