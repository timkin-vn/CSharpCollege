using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Models
{
    public class GameLogic
    {
        public string[,] board = new string[3, 3];
        private string currentPlayer = "X";

        public GameLogic()
        {
            // Инициализируем поле
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = "";
                }
            }
        }

        public bool MakeMove(int row, int col)
        {
            if (board[row, col] == "")
            {
                board[row, col] = currentPlayer;
                return true;
            }
            return false;
        }

        public bool CheckForWin()
        {
            // Проверка строк столбцов и диагоналей
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer) return true;
                if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer) return true;
            }
            if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) return true;
            if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer) return true;

            return false;
        }

        public void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == "X") ? "O" : "X";
        }

        public string GetCurrentPlayer()
        {
            return currentPlayer;
        }


        public void ComputerMove()
        {
            Random rand = new Random();
            int row, col;
            do
            {
                row = rand.Next(3);//случайно
                col = rand.Next(3);
            } while (board[row, col] != ""); // Ищем пустую клетку
            board[row, col] = "O"; // Ход компьютера
        }

        public bool IsDraw()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == "")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }


}
