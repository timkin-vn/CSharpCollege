namespace Игра.DataAccess.EF.Entites
{
    public class Game
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public System.Collections.Generic.List<GameCell> GameCells { get; set; }
    }
}
