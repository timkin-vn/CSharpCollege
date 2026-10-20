using System;
using System.Collections.Generic;
using FifteenGame.Business.Models;

namespace FifteenGame.Business.Services
{
    public class MinesweeperService
    {
        private readonly Random _random = new Random();

        public MinesweeperGame CreateGame(int rows, int columns, int minesCount, bool safeFirstMove)
        {
            var game = new MinesweeperGame(rows, columns, minesCount, safeFirstMove);
            if (!safeFirstMove)
            {
                PlaceMines(game, -1, -1);
            }
            return game;
        }

        public bool OpenCell(MinesweeperGame game, int row, int column)
        {
            if (game.IsFinished || !IsInside(game, row, column))
            {
                return false;
            }

            MinesweeperCell firstCell = game[row, column];
            if (firstCell.IsOpened || firstCell.IsFlagged)
            {
                return false;
            }

            if (!game.MinesPlaced)
            {
                PlaceMines(game, row, column);
            }

            if (firstCell.IsMine)
            {
                game.IsFinished = true;
                game.IsWon = false;
                return true;
            }

            var queue = new Queue<MinesweeperCell>();
            queue.Enqueue(firstCell);

            while (queue.Count > 0)
            {
                MinesweeperCell cell = queue.Dequeue();
                if (cell.IsOpened || cell.IsFlagged || cell.IsMine)
                {
                    continue;
                }

                cell.IsOpened = true;
                game.OpenedCells++;

                if (cell.MinesAround != 0)
                {
                    continue;
                }

                for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
                {
                    for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
                    {
                        int nextRow = cell.Row + rowOffset;
                        int nextColumn = cell.Column + columnOffset;
                        if (IsInside(game, nextRow, nextColumn) &&
                            !game[nextRow, nextColumn].IsOpened &&
                            !game[nextRow, nextColumn].IsFlagged)
                        {
                            queue.Enqueue(game[nextRow, nextColumn]);
                        }
                    }
                }
            }

            if (game.OpenedCells == game.Rows * game.Columns - game.MinesCount)
            {
                game.IsFinished = true;
                game.IsWon = true;
            }
            return true;
        }

        public bool ToggleFlag(MinesweeperGame game, int row, int column)
        {
            if (game.IsFinished || !IsInside(game, row, column))
            {
                return false;
            }

            MinesweeperCell cell = game[row, column];
            if (cell.IsOpened)
            {
                return false;
            }

            cell.IsFlagged = !cell.IsFlagged;
            game.FlagsCount += cell.IsFlagged ? 1 : -1;
            return true;
        }

        private void PlaceMines(MinesweeperGame game, int safeRow, int safeColumn)
        {
            int placed = 0;
            while (placed < game.MinesCount)
            {
                int row = _random.Next(game.Rows);
                int column = _random.Next(game.Columns);
                MinesweeperCell cell = game[row, column];
                if (cell.IsMine || (row == safeRow && column == safeColumn))
                {
                    continue;
                }
                cell.IsMine = true;
                placed++;
            }

            for (int row = 0; row < game.Rows; row++)
            {
                for (int column = 0; column < game.Columns; column++)
                {
                    MinesweeperCell cell = game[row, column];
                    if (cell.IsMine)
                    {
                        continue;
                    }

                    int minesAround = 0;
                    for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
                    {
                        for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
                        {
                            int nextRow = row + rowOffset;
                            int nextColumn = column + columnOffset;
                            if (IsInside(game, nextRow, nextColumn) && game[nextRow, nextColumn].IsMine)
                            {
                                minesAround++;
                            }
                        }
                    }
                    cell.MinesAround = minesAround;
                }
            }
            game.MinesPlaced = true;
        }

        private bool IsInside(MinesweeperGame game, int row, int column)
        {
            return row >= 0 && row < game.Rows && column >= 0 && column < game.Columns;
        }
    }
}
