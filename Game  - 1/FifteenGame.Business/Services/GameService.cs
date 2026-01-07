using FifteenGame.Business.Models;
using System;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private Random _random = new Random();

        private const int MINES_SMALL = 10;
        private const int MINES_MEDIUM = 40;
        private const int MINES_LARGE = 99;

        public void Initialize(GameModel model, int size)
        {
            switch (size)
            {
                case 9:
                    model.MinesCount = MINES_SMALL;
                    break;
                case 16:
                    model.MinesCount = MINES_MEDIUM;
                    break;
                case 24:
                    model.MinesCount = MINES_LARGE;
                    break;
            }

            model.InitializeCells(size);
            PlaceMines(model);
            CountMinesAround(model);
        }

        private void PlaceMines(GameModel model)
        {
            int placed = 0;

            while (placed < model.MinesCount)
            {
                int r = _random.Next(model.Size);
                int c = _random.Next(model.Size);

                if (!model[r, c].IsMine)
                {
                    model[r, c].IsMine = true;
                    placed++;
                }
            }
        }

        private void CountMinesAround(GameModel model)
        {
            for (int r = 0; r < model.Size; r++)
            {
                for (int c = 0; c < model.Size; c++)
                {
                    if (model[r, c].IsMine) continue;

                    int count = 0;

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int nr = r + i;
                            int nc = c + j;

                            if (nr >= 0 && nr < model.Size &&
                                nc >= 0 && nc < model.Size &&
                                model[nr, nc].IsMine)
                                count++;
                        }
                    }

                    model[r, c].MinesAround = count;
                }
            }
        }

        public void OpenCell(GameModel model, int row, int column)
        {
            if (model.GameOver || model[row, column].IsOpened || model[row, column].IsFlagged)
                return;

            model[row, column].IsOpened = true;
            model.CellsOpened++;

            if (model[row, column].IsMine)
            {
                model.GameOver = true;
                model.GameWon = false;
                return;
            }

            if (model[row, column].MinesAround == 0)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int nr = row + i;
                        int nc = column + j;

                        if (nr >= 0 && nr < model.Size &&
                            nc >= 0 && nc < model.Size &&
                            !(i == 0 && j == 0))
                        {
                            OpenCell(model, nr, nc);
                        }
                    }
                }
            }
        }

        public void ToggleFlag(GameModel model, int row, int column)
        {
            if (model.GameOver || model[row, column].IsOpened)
                return;

            model[row, column].IsFlagged = !model[row, column].IsFlagged;

            if (model[row, column].IsFlagged)
                model.FlagsPlaced++;
            else
                model.FlagsPlaced--;
        }

        public bool CheckWin(GameModel model)
        {
            if (model.CellsOpened == model.GetTotalNonMineCells())
            {
                model.GameOver = true;
                model.GameWon = true;
                return true;
            }

            return false;
        }

        public void RevealAllMines(GameModel model)
        {
            for (int r = 0; r < model.Size; r++)
            {
                for (int c = 0; c < model.Size; c++)
                {
                    if (model[r, c].IsMine)
                    {
                        model[r, c].IsOpened = true;
                    }
                }
            }
        }
    }
}