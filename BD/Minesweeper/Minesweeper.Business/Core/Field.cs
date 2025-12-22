using System;

namespace Minesweeper.Business.Core
{
    public class Field
    {
        public int Size { get; private set; }
        public bool[,] Mines { get; set; } 
        public bool[,] Revealed { get; set; }  
        public bool[,] Flag { get; set; }  
        public int[,] Num { get; set; }  
        public int MineCount { get; private set; }
        public bool GameOver { get; set; }  
        public bool GameWon { get; set; }  

        public Field(int size = 10, int mineCount = 15)
        {
            Size = size;
            MineCount = mineCount;
            Mines = new bool[Size, Size];
            Revealed = new bool[Size, Size];
            Flag = new bool[Size, Size];
            Num = new int[Size, Size];
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
                        Num[row, col] = -1;
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
                    Num[row, col] = mineCount;
                }
            }
        }

        public bool GetMine(int row, int col)
        {
            return Mines[row, col];
        }

        public int GetNumber(int row, int col)
        {
            return Num[row, col];
        }

        public void RevealCell(int row, int col)
        {
            if (GameOver || GameWon || Revealed[row, col] || Flag[row, col])
                return;

            Revealed[row, col] = true;

            if (Mines[row, col])
            {
                GameOver = true;
                return;
            }

            if (Num[row, col] == 0)
            {
                RevealAdjacentCells(row, col);
            }

            CheckWinCondition();
        }

        private void RevealAdjacentCells(int row, int col)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    int newRow = row + i;
                    int newCol = col + j;

                    if (newRow >= 0 && newRow < Size && newCol >= 0 && newCol < Size)
                    {
                        if (!Revealed[newRow, newCol] && !Flag[newRow, newCol])
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
                Flag[row, col] = !Flag[row, col];
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
                        if (!Flag[row, col])
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

        public void RestartGame()
        {
            Mines = new bool[Size, Size];
            Revealed = new bool[Size, Size];
            Flag = new bool[Size, Size];
            Num = new int[Size, Size];
            GameOver = false;
            GameWon = false;
            PlaceMines();
            CalculateNumbers();
        }
    }
}