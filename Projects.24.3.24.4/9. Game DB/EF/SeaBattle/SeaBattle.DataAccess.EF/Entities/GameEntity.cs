namespace SeaBattle.DataAccess.EF.Entities
{
    public class GameEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MoveCount { get; set; }
        public bool Won { get; set; }
    }
}
