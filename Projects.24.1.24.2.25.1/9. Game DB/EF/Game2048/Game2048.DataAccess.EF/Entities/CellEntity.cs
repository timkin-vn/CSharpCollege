namespace Game2048.DataAccess.EF.Entities
{
    public class CellEntity
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int Value { get; set; }
    }
}
