using TicTacToe.DataAccess.Repositories;
using TicTacToe.Common.Models;

namespace TicTacToe.Business.Services
{
    public class GameService
    {
        private readonly GameRepository _repo = new GameRepository();

        public string CheckWinner(char[] board)
        {
            int[,] wins = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };
            for (int i = 0; i < 8; i++)
            {
                if (board[wins[i, 0]] != ' ' && board[wins[i, 0]] == board[wins[i, 1]] && board[wins[i, 1]] == board[wins[i, 2]])
                    return board[wins[i, 0]].ToString();
            }
            return null;
        }

        public void SaveGame(int id, char[] board, bool isXTurn)
        {
            _repo.UpdateGame(id, new string(board), isXTurn);
        }
    }
}