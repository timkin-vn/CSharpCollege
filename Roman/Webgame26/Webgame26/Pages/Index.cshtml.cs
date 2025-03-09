using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;

namespace Webgame26.Pages
{
    public class IndexModel : PageModel
    {
        private static Random _random = new Random();
        public string[] Board { get; set; }
        public bool GameOver { get; set; }
        public string GameResult { get; set; }
        //решает проблему с ходами
        public void OnGet()
        {
            Console.WriteLine("Загрузка страницы OnGet()");

            if (HttpContext.Session.GetString("Board") == null)
            {
                Console.WriteLine("Сессия не найдена, сбрасываем игру.");
                ResetGame();
            }
            else
            {
                Console.WriteLine("Сессия найдена, загружаем состояние.");
                LoadGameState();
            }
        }

        public IActionResult OnPost(int cell)
        {
            Console.WriteLine($"Игрок выбрал клетку: {cell}");

            //решает проблему с ходами
            if (Board == null)
            {
                Console.WriteLine("Board == null, загружаем состояние из сессии.");
                LoadGameState();
            }

            if (GameOver || Board[cell] != "")
            {
                Console.WriteLine("Ошибка: Ход невозможен.");
                return Page();
            }

            // Ход игрока
            Board[cell] = "X";
            SaveGameState();

            if (CheckWin("X"))
            {
                GameOver = true;
                GameResult = "Поздравляем! Вы выиграли!";
                SaveGameState();
                return Page();
            }

            if (IsBoardFull())
            {
                GameOver = true;
                GameResult = "Ничья!";
                SaveGameState();
                return Page();
            }

            // Ход компьютера
            ComputerMove();

            if (CheckWin("O"))
            {
                GameOver = true;
                GameResult = "Компьютер победил!";
            }
            else if (IsBoardFull())
            {
                GameOver = true;
                GameResult = "Ничья!";
            }

            SaveGameState();
            return Page();
        }

        public IActionResult OnPostReset()
        {
            ResetGame();
            return RedirectToPage();
        }

        private void ComputerMove()
        {
            int move;
            while (true)
            {
                move = _random.Next(0, 9);
                if (Board[move] == "")
                {
                    Board[move] = "O";
                    break;
                }
            }
        }

        private bool CheckWin(string player)
        {
            int[,] winPatterns = new int[,]
            {
                { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },
                { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 },
                { 0, 4, 8 }, { 2, 4, 6 }
            };

            for (int i = 0; i < winPatterns.GetLength(0); i++)
            {
                if (Board[winPatterns[i, 0]] == player &&
                    Board[winPatterns[i, 1]] == player &&
                    Board[winPatterns[i, 2]] == player)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsBoardFull()
        {
            return Board.All(cell => cell != "");
        }
        //решает проблему с ходами
        private void SaveGameState()
        {
            Console.WriteLine($"Сохраняем состояние: {string.Join(",", Board)}");
            HttpContext.Session.SetString("Board", string.Join(",", Board));
            HttpContext.Session.SetString("GameOver", GameOver.ToString());
            HttpContext.Session.SetString("GameResult", GameResult);
        }

        private void LoadGameState()
        {
            Console.WriteLine("Загружаем состояние из сессии...");

            string boardString = HttpContext.Session.GetString("Board");
            if (string.IsNullOrEmpty(boardString))
            {
                Console.WriteLine("Ошибка! Сессия пустая, сбрасываем игру.");
                ResetGame();
                return;
            }

            Board = boardString.Split(',');
            GameOver = bool.Parse(HttpContext.Session.GetString("GameOver"));
            GameResult = HttpContext.Session.GetString("GameResult");

            Console.WriteLine($"Состояние загружено: {string.Join(",", Board)}");
        }

        private void ResetGame()
        {
            Console.WriteLine("Сбрасываем игру.");
            Board = Enumerable.Repeat("", 9).ToArray();
            GameOver = false;
            GameResult = "";
            SaveGameState();
        }
    }
}
