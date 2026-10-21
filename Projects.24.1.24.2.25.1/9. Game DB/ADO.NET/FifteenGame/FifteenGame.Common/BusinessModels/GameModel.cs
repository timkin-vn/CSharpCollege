
namespace FifteenGame.Common.BusinessModels
{
    public class GameModel
    {
        private readonly int[,] _cells = new int[4, 4];

        public int Id { get; set; }
        public int UserId { get; set; }
        public int MoveCount { get; set; }
        public int Score { get; set; }
        public int BestScore { get; set; }
        public bool IsGameOver { get; set; }

        public int this[int row, int column]
        {
            get => _cells[row, column];
            set => _cells[row, column] = value;
        }

        public GameModel Clone()
        {
            var copy = new GameModel
            {
                Id = Id,
                UserId = UserId,
                MoveCount = MoveCount,
                Score = Score,
                BestScore = BestScore,
                IsGameOver = IsGameOver
            };

            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    copy[row, column] = this[row, column];
                }
            }

            return copy;
        }
    }
}
