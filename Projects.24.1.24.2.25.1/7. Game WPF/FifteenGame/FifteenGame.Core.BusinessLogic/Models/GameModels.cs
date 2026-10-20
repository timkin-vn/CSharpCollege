namespace FifteenGame.Core.BusinessLogic.Models
{
    public enum CellType
    {
        Floor,
        Wall,
        Key,
        Exit
    }

    public enum GameState
    {
        Playing,
        Won,
        Lost
    }

    public class Monster
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }

    /// <summary>Состояние игры в виде простого объекта (для сохранения в БД или передачи по сети).</summary>
    public class GameSnapshot
    {
        public CellType[][] Cells { get; set; }
        public Monster[] Monsters { get; set; }
        public int PlayerRow { get; set; }
        public int PlayerColumn { get; set; }
        public int KeysLeft { get; set; }
        public int Moves { get; set; }
        public GameState State { get; set; }
    }
}
