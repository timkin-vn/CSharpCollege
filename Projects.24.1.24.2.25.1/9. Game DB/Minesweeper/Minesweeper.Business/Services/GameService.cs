using Minesweeper.Business.Models;
using System;

namespace Minesweeper.Business.Services
{
    public class GameService
    {
        public void Initialize(GameModel model)
        {
            var random = new Random();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model.SetMine(row, column, false);
                    model.SetRevealed(row, column, false);
                    model.SetFlagged(row, column, false);
                    model.SetNumber(row, column, 0);
                }
            }

            int placed = 0;
            while (placed < GameModel.MineCount)
            {
                int row = random.Next(GameModel.RowCount);
                int column = random.Next(GameModel.ColumnCount);
                if (!model.HasMine(row, column))
                {
                    model.SetMine(row, column, true);
                    placed++;
                }
            }

            for (int row = 0; row < GameModel.RowCount; row++)
                for (int column = 0; column < GameModel.ColumnCount; column++)
                    if (!model.HasMine(row, column))
                        model.SetNumber(row, column, CountAdjacentMines(model, row, column));

            model.IsGameOver = false;
            model.IsWon = false;
        }

        public bool Reveal(GameModel model, int row, int column)
        {
            if (model.IsGameOver || model.IsRevealed(row, column) || model.IsFlagged(row, column))
                return false;

            if (model.HasMine(row, column))
            {
                model.SetRevealed(row, column, true);
                model.IsGameOver = true;
                model.IsWon = false;
                RevealAllMines(model);
                return true;
            }

            RevealArea(model, row, column);
            CheckWin(model);
            return true;
        }

        public bool ToggleFlag(GameModel model, int row, int column)
        {
            if (model.IsGameOver || model.IsRevealed(row, column))
                return false;

            model.SetFlagged(row, column, !model.IsFlagged(row, column));
            return true;
        }

        private int CountAdjacentMines(GameModel model, int row, int column)
        {
            int count = 0;
            for (int r = Math.Max(0, row - 1); r <= Math.Min(GameModel.RowCount - 1, row + 1); r++)
                for (int c = Math.Max(0, column - 1); c <= Math.Min(GameModel.ColumnCount - 1, column + 1); c++)
                    if ((r != row || c != column) && model.HasMine(r, c)) count++;
            return count;
        }

        private void RevealArea(GameModel model, int row, int column)
        {
            if (row < 0 || row >= GameModel.RowCount || column < 0 || column >= GameModel.ColumnCount)
                return;
            if (model.IsRevealed(row, column) || model.IsFlagged(row, column) || model.HasMine(row, column))
                return;

            model.SetRevealed(row, column, true);
            if (model.GetNumber(row, column) != 0) return;

            for (int r = row - 1; r <= row + 1; r++)
                for (int c = column - 1; c <= column + 1; c++)
                    if (r != row || c != column) RevealArea(model, r, c);
        }

        private void CheckWin(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
                for (int column = 0; column < GameModel.ColumnCount; column++)
                    if (!model.HasMine(row, column) && !model.IsRevealed(row, column))
                        return;

            model.IsGameOver = true;
            model.IsWon = true;
        }

        private void RevealAllMines(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
                for (int column = 0; column < GameModel.ColumnCount; column++)
                    if (model.HasMine(row, column)) model.SetRevealed(row, column, true);
        }
    }
}
