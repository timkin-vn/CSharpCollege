using TicTacToeGame.Common;
using System.Linq;

namespace TicTacToeGame.Business
{
    public class GameService
    {
        public GameState MakeMove(GameState state, int pos)
        {
            if (pos < 0 || pos > 8 || state.Board[pos] != '\0' || state.Status != "In Progress")
                return state;

            state.Board[pos] = state.IsXTurn ? 'X' : 'O';

            if (CheckWin(state.Board, state.Board[pos]))
                state.Status = state.Board[pos] + " Wins!";
            else if (!state.Board.Contains('\0'))
                state.Status = "Draw";
            else
                state.IsXTurn = !state.IsXTurn;

            return state;
        }

        private bool CheckWin(char[] b, char p)
        {
            int[,] w = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };
            for (int i = 0; i < 8; i++)
                if (b[w[i, 0]] == p && b[w[i, 1]] == p && b[w[i, 2]] == p) return true;
            return false;
        }
    }
}