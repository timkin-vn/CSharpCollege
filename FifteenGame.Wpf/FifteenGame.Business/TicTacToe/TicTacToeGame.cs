using System;

namespace Pacman.Business.TicTacToe
{
    public class TicTacToeGame
    {
        public string[] Cells { get; } = new string[9];

        public bool XTurn { get; private set; } = true;
        public bool GameOver { get; private set; } = false;

        public string StatusText
        {
            get
            {
                if (GameOver) return _statusAfterEnd;
                return XTurn ? "Ход: X" : "Ход: O";
            }
        }

        private string _statusAfterEnd = "";

        public void MakeMove(int index)
        {
            if (GameOver) return;
            if (index < 0 || index > 8) return;
            if (!string.IsNullOrEmpty(Cells[index])) return; // клетка занята

            Cells[index] = XTurn ? "X" : "O";

            // Проверка победы/ничьи
            var winner = GetWinner();
            if (winner != null)
            {
                GameOver = true;
                _statusAfterEnd = $"Победили {winner}!";
            }
            else if (IsDraw())
            {
                GameOver = true;
                _statusAfterEnd = "Ничья!";
            }
            else
            {
                XTurn = !XTurn; // следующий ход
            }
        }

        public void ResetGame()
        {
            for (int i = 0; i < 9; i++)
                Cells[i] = "";

            XTurn = true;
            GameOver = false;
            _statusAfterEnd = "";
        }

        private string GetWinner()
        {
            int[][] lines =
            {
                new[]{0,1,2}, new[]{3,4,5}, new[]{6,7,8},
                new[]{0,3,6}, new[]{1,4,7}, new[]{2,5,8},
                new[]{0,4,8}, new[]{2,4,6}
            };

            foreach (var l in lines)
            {
                var a = Cells[l[0]];
                if (string.IsNullOrEmpty(a)) continue;

                if (Cells[l[1]] == a && Cells[l[2]] == a)
                    return a; // "X" или "O"
            }

            return null;
        }

        private bool IsDraw()
        {
            for (int i = 0; i < 9; i++)
                if (string.IsNullOrEmpty(Cells[i]))
                    return false;
            return true;
        }
    }
}
