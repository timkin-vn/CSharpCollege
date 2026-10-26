using FifteenGame.Business.Models;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        public GameModel Model { get; private set; } = new GameModel();

        public void StartNewGame()
        {
            Model.Reset();
        }

        public bool MakeMove(int row, int col)
        {
            if (Model.Winner != 0 || Model.Board[row, col] != 0)
                return false;

            Model.Board[row, col] = Model.CurrentPlayer;

            CheckWinner();

            if (Model.Winner == 0)
            {
                Model.CurrentPlayer = Model.CurrentPlayer == 1 ? 2 : 1;
            }

            return true;
        }

        private void CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Model.Board[i, 0] != 0 && Model.Board[i, 0] == Model.Board[i, 1] && Model.Board[i, 1] == Model.Board[i, 2])
                    Model.Winner = Model.Board[i, 0];

                if (Model.Board[0, i] != 0 && Model.Board[0, i] == Model.Board[1, i] && Model.Board[1, i] == Model.Board[2, i])
                    Model.Winner = Model.Board[0, i];
            }

            if (Model.Board[0, 0] != 0 && Model.Board[0, 0] == Model.Board[1, 1] && Model.Board[1, 1] == Model.Board[2, 2])
                Model.Winner = Model.Board[0, 0];

            if (Model.Board[0, 2] != 0 && Model.Board[0, 2] == Model.Board[1, 1] && Model.Board[1, 1] == Model.Board[2, 0])
                Model.Winner = Model.Board[0, 2];
        }
    }
}