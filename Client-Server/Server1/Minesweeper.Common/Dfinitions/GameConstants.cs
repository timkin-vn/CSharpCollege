namespace Minesweeper.Common.Definitions
{
    public static class GameConstants
    {
        public const int DefaultFieldSize = 10;
        public const int DefaultMineCount = 15;
        public const int MaxFieldSize = 20;
        public const int MinFieldSize = 5;

        public const string StatusPlaying = "playing";
        public const string StatusGameOver = "game_over";
        public const string StatusWon = "won";

        public const string ActionOpen = "open";
        public const string ActionToggleFlag = "toggle_flag";
    }
}