using FifteenGame.Business.Models;

namespace FifteenGame.Business.Services
{
    public class TicTacToeService
    {
        public TicTacToeGame InitializeGame()
        {
            var game = new TicTacToeGame();
            for (int row = 0; row < TicTacToeGame.RowCount; row++)
            {
                for (int column = 0; column < TicTacToeGame.ColumnCount; column++)
                {
                    game[row, column] = Player.None;
                }
            }
            game.CurrentPlayer = Player.X;
            game.GameState = GameState.Playing;
            game.Winner = Player.None;
            return game;
        }

        public void MakeMove(TicTacToeGame game, int row, int column)
        {
            if (game.GameState != GameState.Playing || !game.IsCellEmpty(row, column))
            {
                return;
            }

            game[row, column] = game.CurrentPlayer;

            if (CheckForWin(game, game.CurrentPlayer))
            {
                game.GameState = GameState.Won;
                game.Winner = game.CurrentPlayer;
            }
            else if (CheckForDraw(game))
            {
                game.GameState = GameState.Draw;
            }
            else
            {
                game.CurrentPlayer = (game.CurrentPlayer == Player.X) ? Player.O : Player.X;
            }
        }

        private bool CheckForWin(TicTacToeGame game, Player player)
        {
            // Check rows
            for (int row = 0; row < TicTacToeGame.RowCount; row++)
            {
                if (game[row, 0] == player && game[row, 1] == player && game[row, 2] == player)
                    return true;
            }

            // Check columns
            for (int column = 0; column < TicTacToeGame.ColumnCount; column++)
            {
                if (game[0, column] == player && game[1, column] == player && game[2, column] == player)
                    return true;
            }

            // Check diagonals
            if (game[0, 0] == player && game[1, 1] == player && game[2, 2] == player)
                return true;
            if (game[0, 2] == player && game[1, 1] == player && game[2, 0] == player)
                return true;

            return false;
        }

        private bool CheckForDraw(TicTacToeGame game)
        {
            for (int row = 0; row < TicTacToeGame.RowCount; row++)
            {
                for (int column = 0; column < TicTacToeGame.ColumnCount; column++)
                {
                    if (game.IsCellEmpty(row, column))
                        return false; // Still empty cells, not a draw yet
                }
            }
            return true; // All cells filled, and no winner
        }
    }
}
