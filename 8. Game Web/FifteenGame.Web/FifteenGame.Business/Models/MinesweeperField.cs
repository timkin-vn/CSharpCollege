using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minesweeper.Business.Models
{
    public class MinesweeperField
    {
        public int Size { get; set; }
        public bool[,] Mines { get; set; }
        public bool[,] Revealed { get; set; }
        public bool[,] Flagged { get; set; }
        public int[,] Numbers { get; set; }
        public int MineCount { get; set; }
        public bool GameOver { get; set; }
        public bool GameWon { get; set; }

        public MinesweeperField(int size = 10, int mineCount = 15)
        {
            Size = size;
            MineCount = mineCount;
            Mines = new bool[Size, Size];
            Revealed = new bool[Size, Size];
            Flagged = new bool[Size, Size];
            Numbers = new int[Size, Size];
            GameOver = false;
            GameWon = false;
            PlaceMines();
            CalculateNumbers();
        }

        private void PlaceMines()
        {
            Random random = new Random();
            int minesPlaced = 0;

            while (minesPlaced < MineCount)
            {
                int row = random.Next(Size);
                int col = random.Next(Size);

                if (!Mines[row, col])
                {
                    Mines[row, col] = true;
                    minesPlaced++;
                }
            }
        }

        private void CalculateNumbers()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (Mines[row, col])
                    {
                        Numbers[row, col] = -1;
                        continue;
                    }

                    int mineCount = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int newRow = row + i;
                            int newCol = col + j;

                            if (newRow >= 0 && newRow < Size && newCol >= 0 && newCol < Size)
                            {
                                if (Mines[newRow, newCol])
                                {
                                    mineCount++;
                                }
                            }
                        }
                    }
                    Numbers[row, col] = mineCount;
                }
            }
        }

        public bool HasMine(int row, int col) => Mines[row, col];
        public int GetNumber(int row, int col) => Numbers[row, col];

        public void RevealCell(int row, int col)
        {
            if (GameOver || GameWon || Revealed[row, col] || Flagged[row, col])
                return;

            Revealed[row, col] = true;

            if (Mines[row, col])
            {
                GameOver = true;
                return;
            }

            CheckWinCondition();
            if (Numbers[row, col] == 0)
            {
                RevealAdjacentCells(row, col);
            }
        }

        private void RevealAdjacentCells(int row, int col)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newCol = col + j;

                    if (newRow >= 0 && newRow < Size && newCol >= 0 && newCol < Size)
                    {
                        if (!Revealed[newRow, newCol] && !Flagged[newRow, newCol])
                        {
                            RevealCell(newRow, newCol);
                        }
                    }
                }
            }
        }

        public void ToggleFlag(int row, int col)
        {
            if (!Revealed[row, col] && !GameOver && !GameWon)
            {
                Flagged[row, col] = !Flagged[row, col];
                CheckWinCondition();
            }
        }

        public void CheckWinCondition()
        {
            bool allSafeCellsRevealed = true;
            bool allMinesCorrectlyFlagged = true;

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (Mines[row, col])
                    {
                        if (!Flagged[row, col])
                        {
                            allMinesCorrectlyFlagged = false;
                        }
                    }
                    else
                    {
                        if (!Revealed[row, col])
                        {
                            allSafeCellsRevealed = false;
                        }
                    }
                }
            }

            GameWon = allSafeCellsRevealed || allMinesCorrectlyFlagged;
        }

        public void RestartGame(int size = 10, int mineCount = 15)
        {
            Size = size;
            MineCount = mineCount;
            Mines = new bool[Size, Size];
            Revealed = new bool[Size, Size];
            Flagged = new bool[Size, Size];
            Numbers = new int[Size, Size];
            GameOver = false;
            GameWon = false;
            PlaceMines();
            CalculateNumbers();
        }
    }
}