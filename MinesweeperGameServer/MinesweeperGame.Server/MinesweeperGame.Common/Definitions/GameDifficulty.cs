namespace MinesweeperGame.Common.Definitions
{
    public enum GameDifficulty
    {
        Beginner = 0,
        Intermediate = 1,
        Expert = 2
    }

    public static class GameDifficultyExtensions
    {
        public static (int Rows, int Columns, int Mines) GetParameters(this GameDifficulty difficulty)
        {
            return difficulty switch
            {
                GameDifficulty.Beginner => (9, 9, 10),
                GameDifficulty.Intermediate => (16, 16, 40),
                GameDifficulty.Expert => (16, 30, 99),
                _ => (9, 9, 10)
            };
        }
    }
}
