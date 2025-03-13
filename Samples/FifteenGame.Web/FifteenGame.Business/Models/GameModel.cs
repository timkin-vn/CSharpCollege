using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Models
{
    public class GameModel
    {
        public const int Size = 3;
        public const char EmptyCell = ' ';
        public const char Player = 'X';
        public const char Bot = 'O';

        public char[,] Board { get; private set; } = new char[Size, Size];

        public GameModel()
        {
            ResetBoard();
        }

        public void ResetBoard()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Board[row, col] = EmptyCell;
                }
            }
        }

        public bool IsFull()
        {
            foreach (var cell in Board)
            {
                if (cell == EmptyCell)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckWin(char player)
        {
            for (int i = 0; i < Size; i++)
            {
                if ((Board[i, 0] == player && Board[i, 1] == player && Board[i, 2] == player) ||
                    (Board[0, i] == player && Board[1, i] == player && Board[2, i] == player))
                {
                    return true;
                }
            }

            if ((Board[0, 0] == player && Board[1, 1] == player && Board[2, 2] == player) ||
                (Board[0, 2] == player && Board[1, 1] == player && Board[2, 0] == player))
            {
                return true;
            }

            return false;
        }
    }
}