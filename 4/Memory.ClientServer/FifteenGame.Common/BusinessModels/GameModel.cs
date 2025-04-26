using System;

namespace FifteenGame.Common.BusinessModels
{
    public class GameModel
    {
        public int[,] Field { get; set; } = new int[4, 4];

        public int GameId { get; set; }
        public int UserId { get; set; }

        public int MoveCount { get; set; }
        public DateTime GameStart { get; set; }
        public int Score { get; set; } 

        public int GetCell(int row, int col) => Field[row, col];
        public void SetCell(int row, int col, int value) => Field[row, col] = value;

        public void AddScore(int value) => Score += value;

        public void Reset()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    Field[i, j] = 0;
            Score = 0;
        }
    }
}
