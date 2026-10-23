namespace Game2048.Common.Models
{

    public class GameModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int[,] Field { get; set; }
        public int Score { get; set; }
        public int MoveCount { get; set; }

        public GameModel()
        {
            Field = new int[Constants.Size, Constants.Size];
        }
    }
}
