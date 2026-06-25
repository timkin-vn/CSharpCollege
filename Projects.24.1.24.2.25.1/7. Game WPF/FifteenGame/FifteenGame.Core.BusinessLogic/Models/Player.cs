namespace FifteenGame.Business.Models
{
    public class Player
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Keys { get; set; }
        public int Moves { get; set; }

        public Player(int startRow, int startCol)
        {
            Row = startRow;
            Column = startCol;
            Keys = 0;
            Moves = 0;
        }
    }
}