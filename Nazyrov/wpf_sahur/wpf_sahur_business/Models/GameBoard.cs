using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_sahur_business.Models
{
    public class GameBoard
    {
        public string[] Board { get; set; } = new string[9];
        public bool IsXTurn { get; set; } = true;

        public bool MakeMove(int index)
        {
            if (string.IsNullOrEmpty(Board[index]))
            {
                Board[index] = IsXTurn ? "X" : "O";
                IsXTurn = !IsXTurn;
                return true;
            }
            return false;
        }

        public bool CheckWinner(out string winner)
        {
            // Horizontal
            for (int i = 0; i < 9; i += 3)
            {
                if (!string.IsNullOrEmpty(Board[i]) && Board[i] == Board[i + 1] && Board[i] == Board[i + 2])
                {
                    winner = Board[i];
                    return true;
                }
            }

            // Vertical
            for (int i = 0; i < 3; i++)
            {
                if (!string.IsNullOrEmpty(Board[i]) && Board[i] == Board[i + 3] && Board[i] == Board[i + 6])
                {
                    winner = Board[i];
                    return true;
                }
            }

            // Diagonal
            if (!string.IsNullOrEmpty(Board[0]) && Board[0] == Board[4] && Board[0] == Board[8])
            {
                winner = Board[0];
                return true;
            }
            if (!string.IsNullOrEmpty(Board[2]) && Board[2] == Board[4] && Board[2] == Board[6])
            {
                winner = Board[2];
                return true;
            }

            winner = null;
            return false;
        }

        public bool IsBoardFull()
        {
            return !Board.Any(cell => string.IsNullOrEmpty(cell));
        }

        public void Reset()
        {
            Board = new string[9];
            IsXTurn = true;
        }
    }
}
