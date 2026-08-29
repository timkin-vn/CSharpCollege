// Файл: LightsOutGame.Business/Game.cs
using System;

namespace LightsOutGame.Business
{
    public class Game
    {
        public int Size { get; } = 5; // Сетка светильников 5х5
        public bool[,] Board { get; private set; } // true - горит, false - погас
        public bool IsWon { get; private set; }

        private readonly Random _random = new Random();

        public Game()
        {
            Board = new bool[Size, Size];
            Reset();
        }

        // Перемешивание поля для новой игры
        public void Reset()
        {
            IsWon = false;
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    Board[r, c] = _random.Next(0, 2) == 1; // Случайно включаем/выключаем
                }
            }

            // На случай, если поле сразу пустое
            if (CheckWin())
            {
                Reset();
            }
        }

        // Ход по правилам Светильников
        public void MakeMove(int r, int c)
        {
            if (IsWon) return;

            // Инвертируем нажатую лампочку и ее соседей
            Toggle(r, c);     // Центр
            Toggle(r - 1, c); // Верх
            Toggle(r + 1, c); // Низ
            Toggle(r, c - 1); // Лево
            Toggle(r, c + 1); // Право

            IsWon = CheckWin();
        }

        private void Toggle(int r, int c)
        {
            if (r >= 0 && r < Size && c >= 0 && c < Size)
            {
                Board[r, c] = !Board[r, c];
            }
        }

        private bool CheckWin()
        {
            foreach (bool cell in Board)
            {
                if (cell) return false; // Если хоть одна лампа горит — играем дальше
            }
            return true;
        }
    }
}