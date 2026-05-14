using System;
using System.Text.Json;
using Minesweeper.Common.Models;

namespace Minesweeper.Business.Core
{
    public class Field
    {
        public int Size { get; private set; }
        public bool[,] Mines { get; private set; }
        public bool[,] Revealed { get; private set; }
        public bool[,] Flags { get; private set; }
        public int[,] Numbers { get; private set; }
        public int MineCount { get; private set; }
        public bool GameOver { get; private set; }
        public bool GameWon { get; private set; }
        public int CellsRevealed { get; private set; }
        public int FlagsPlaced { get; private set; }

        public Field(int size = 10, int mineCount = 15)
        {
            if (size < 5 || size > 20)
                throw new ArgumentException("Size must be between 5 and 20");

            if (mineCount < 1 || mineCount > size * size - 1)
                throw new ArgumentException($"Mine count must be between 1 and {size * size - 1}");

            Size = size;
            MineCount = mineCount;

            InitializeArrays();
            PlaceMines();
            CalculateNumbers();
        }

        private void InitializeArrays()
        {
            Mines = new bool[Size, Size];
            Revealed = new bool[Size, Size];
            Flags = new bool[Size, Size];
            Numbers = new int[Size, Size];
            GameOver = false;
            GameWon = false;
            CellsRevealed = 0;
            FlagsPlaced = 0;
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
                        Numbers[row, col] = 9; 
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

        public bool OpenCell(int row, int col)
        {
            if (GameOver || GameWon || Revealed[row, col] || Flags[row, col])
                return false;

            Revealed[row, col] = true;
            CellsRevealed++;

            if (Mines[row, col])
            {
                GameOver = true;
                RevealAllMines();
                return true;
            }

            if (Numbers[row, col] == 0)
            {
                OpenAdjacentCells(row, col);
            }

            CheckWinCondition();
            return true;
        }

        private void OpenAdjacentCells(int row, int col)
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
                        if (!Revealed[newRow, newCol] && !Flags[newRow, newCol])
                        {
                            OpenCell(newRow, newCol);
                        }
                    }
                }
            }
        }

        public bool ToggleFlag(int row, int col)
        {
            if (GameOver || GameWon || Revealed[row, col])
                return false;

            Flags[row, col] = !Flags[row, col];

            if (Flags[row, col])
                FlagsPlaced++;
            else
                FlagsPlaced--;

            CheckWinCondition();
            return true;
        }

        private void CheckWinCondition()
        {
            bool allSafeCellsRevealed = true;
            bool allMinesFlagged = true;

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (Mines[row, col])
                    {
                        if (!Flags[row, col])
                        {
                            allMinesFlagged = false;
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

            GameWon = allSafeCellsRevealed || allMinesFlagged;
        }

        private void RevealAllMines()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (Mines[row, col])
                    {
                        Revealed[row, col] = true;
                    }
                }
            }
        }

        public int[,] GetVisibleField()
        {
            int[,] visible = new int[Size, Size];

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (Flags[row, col])
                    {
                        visible[row, col] = -2; // -2 = флаг
                    }
                    else if (!Revealed[row, col])
                    {
                        visible[row, col] = -1; // -1 = скрыто
                    }
                    else if (Mines[row, col])
                    {
                        visible[row, col] = 9; // 9 = мина
                    }
                    else
                    {
                        visible[row, col] = Numbers[row, col]; // 0-8 = число мин рядом
                    }
                }
            }

            return visible;
        }

        public string Serialize()
        {
            var model = new FieldModel
            {
                Size = Size,
                Mines = ConvertToJagged(Mines),
                Revealed = ConvertToJagged(Revealed),
                Flag = ConvertToJagged(Flags),
                Num = ConvertToJagged(Numbers),
                MineCount = MineCount,
                GameOver = GameOver,
                GameWon = GameWon
            };

            return JsonSerializer.Serialize(model);
        }

        public static Field Deserialize(string json)
        {
            var model = JsonSerializer.Deserialize<FieldModel>(json);

            var field = new Field(model.Size, model.MineCount)
            {
                Mines = ConvertTo2D(model.Mines, model.Size),
                Revealed = ConvertTo2D(model.Revealed, model.Size),
                Flags = ConvertTo2D(model.Flag, model.Size),
                Numbers = ConvertTo2D(model.Num, model.Size),
                GameOver = model.GameOver,
                GameWon = model.GameWon
            };

            field.RecalculateCounters();

            return field;
        }

        private void RecalculateCounters()
        {
            CellsRevealed = 0;
            FlagsPlaced = 0;

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (Revealed[row, col]) CellsRevealed++;
                    if (Flags[row, col]) FlagsPlaced++;
                }
            }
        }

        private static bool[][] ConvertToJagged(bool[,] array2D)
        {
            int size = array2D.GetLength(0);
            var result = new bool[size][];

            for (int i = 0; i < size; i++)
            {
                result[i] = new bool[size];
                for (int j = 0; j < size; j++)
                {
                    result[i][j] = array2D[i, j];
                }
            }

            return result;
        }

        private static int[][] ConvertToJagged(int[,] array2D)
        {
            int size = array2D.GetLength(0);
            var result = new int[size][];

            for (int i = 0; i < size; i++)
            {
                result[i] = new int[size];
                for (int j = 0; j < size; j++)
                {
                    result[i][j] = array2D[i, j];
                }
            }

            return result;
        }

        private static bool[,] ConvertTo2D(bool[][] jagged, int size)
        {
            var result = new bool[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = jagged[i][j];
                }
            }

            return result;
        }

        private static int[,] ConvertTo2D(int[][] jagged, int size)
        {
            var result = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = jagged[i][j];
                }
            }

            return result;
        }
    }
}