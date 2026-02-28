using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class TicTacToeService
    {
        private TicTacToeGame _game;

        public TicTacToeService()
        {
            _game = new TicTacToeGame();
        }

        public TicTacToeGame GetGame()
        {
            return _game;
        }

        public bool MakeMove(int row, int col)
        {
            return _game.MakeMove(row, col);
        }

        public void ResetGame()
        {
            _game.Reset();
        }
    }
} 