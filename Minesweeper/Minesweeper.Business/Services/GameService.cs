using Minesweeper.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Business.Services
{
    public class GameService
    {
        public void InitializeGame(GameModel model)
        {
            model.FlagsPlaced = 0;
            model.State = GameState.Playing;
            model.IsFirstClick = true;

            for (int row = 0; row < model.RowCount; row++)
            {
                for (int column = 0; column < model.ColumnCount; column++)
                {
                    model[row, column] = new CellModel(row, column);
                }
            }
        }

        public bool RevealCell(GameModel model, int row, int column)
        {
            if (row < 0 || row >= model.RowCount ||
                column < 0 || column >= model.ColumnCount)
            {
                return false;
            }

            var cell = model[row, column];

            if (cell.IsRevealed || cell.IsFlagged)
            {
                return false;
            }

            if (model.IsFirstClick)
            {
                model.PlaceMines(row, column);
                model.IsFirstClick = false;
            }

            if (cell.IsMine)
            {
                cell.Reveal();
                RevealAllMines(model);
                model.State = GameState.Lost;
                return true;
            }

            RevealCellRecursive(model, row, column);

            if (model.CheckWin())
            {
                model.State = GameState.Won;
            }

            return true;
        }

        private void RevealCellRecursive(GameModel model, int row, int column)
        {
            if (row < 0 || row >= model.RowCount ||
                column < 0 || column >= model.ColumnCount)
            {
                return;
            }

            var cell = model[row, column];

            if (cell.IsRevealed || cell.IsFlagged)
            {
                return;
            }

            cell.Reveal();

            if (cell.AdjacentMinesCount == 0 && !cell.IsMine)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i != 0 || j != 0)
                        {
                            RevealCellRecursive(model, row + i, column + j);
                        }
                    }
                }
            }
        }

        private void RevealAllMines(GameModel model)
        {
            for (int row = 0; row < model.RowCount; row++)
            {
                for (int column = 0; column < model.ColumnCount; column++)
                {
                    if (model[row, column].IsMine)
                    {
                        model[row, column].Reveal();
                    }
                }
            }
        }

        public bool ToggleFlag(GameModel model, int row, int column)
        {
            if (row < 0 || row >= model.RowCount ||
                column < 0 || column >= model.ColumnCount)
            {
                return false;
            }

            var cell = model[row, column];

            if (cell.IsRevealed)
            {
                return false;
            }

            if (cell.IsFlagged)
            {
                model.FlagsPlaced--;
                cell.ToggleFlag();
                return true;
            }
            else
            {
                if (model.FlagsPlaced < model.MineCount)
                {
                    model.FlagsPlaced++;
                    cell.ToggleFlag();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int GetRemainingMines(GameModel model)
        {
            return model.MineCount - model.FlagsPlaced;
        }
    }
}