namespace MemoryGame.DataAccess.EF.Entities
{
    public class Cell
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        // Номер картинки на карточке
        public int Value { get; set; }

        // Состояние карточки: 0 - закрыта, 1 - открыта, 2 - пара найдена
        public int State { get; set; }

        public Game Game { get; set; }
    }
}
