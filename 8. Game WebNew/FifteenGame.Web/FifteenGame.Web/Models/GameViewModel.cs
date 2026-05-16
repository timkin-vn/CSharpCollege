using FifteenGame.Business.Models;

namespace FifteenGame.Web.Models
{
    public class GameViewModel
    {
        public int RowCount => GameField.RowCount;
        public int ColumnCount => GameField.ColumnCount;

        public CellViewModel[,] Cells { get; set; } = new CellViewModel[GameField.RowCount, GameField.ColumnCount];

        public int Score { get; set; }
        public int MovesLeft { get; set; }
        public GameState GameState { get; set; }
        public string GameOverMessage { get; set; }

        public string GameStatus
        {
            get
            {
                if (GameState == GameState.Playing)
                    return $"Очки: {Score} | Ходов осталось: {MovesLeft}";
                else if (GameState == GameState.Won)
                    return $"Победа! Набрано {Score} очков!";
                else
                    return $"Игра окончена. Очки: {Score}";
            }
        }
    }
}