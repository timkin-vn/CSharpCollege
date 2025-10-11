using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private Random _random = new Random();

        public void Initialize(GameModel model, int minesCount)
        {
            model.TotalMines = minesCount;

            // Очистка поля
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = 0;
                    model.SetRevealed(row, column, false);
                    model.SetFlagged(row, column, false);
                    model.SetMine(row, column, false);
                }
            }

            // Расстановка мин
            int minesPlaced = 0;
            while (minesPlaced < model.TotalMines)
            {
                int row = _random.Next(GameModel.RowCount);
                int column = _random.Next(GameModel.ColumnCount);

                if (!model.HasMine(row, column))
                {
                    model.SetMine(row, column, true);
                    minesPlaced++;
                }
            }

            // Расчет соседних мин
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (!model.HasMine(row, column))
                    {
                        model[row, column] = CalculateAdjacentMines(model, row, column);
                    }
                }
            }

            model.GameState = GameState.Playing;
            model.FlagsPlaced = 0;
        }

        public void Initialize(GameModel model)
        {
            Initialize(model, 15); // Значение по умолчанию
        }

        private int CalculateAdjacentMines(GameModel model, int row, int column)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newColumn = column + j;

                    if (newRow >= 0 && newRow < GameModel.RowCount &&
                        newColumn >= 0 && newColumn < GameModel.ColumnCount &&
                        model.HasMine(newRow, newColumn))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public void RevealCell(GameModel model, int row, int column)
        {
            if (model.IsRevealed(row, column) || model.IsFlagged(row, column))
                return;

            model.SetRevealed(row, column, true);

            if (model.HasMine(row, column))
            {
                model.GameState = GameState.GameOver;
                return;
            }

            // Если клетка пустая (0 мин вокруг), открываем соседние клетки рекурсивно
            if (model[row, column] == 0)
            {
                RevealAdjacentCells(model, row, column);
            }

            CheckWinCondition(model);
        }

        private void RevealAdjacentCells(GameModel model, int row, int column)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newColumn = column + j;

                    if (newRow >= 0 && newRow < GameModel.RowCount &&
                        newColumn >= 0 && newColumn < GameModel.ColumnCount &&
                        !model.IsRevealed(newRow, newColumn) &&
                        !model.IsFlagged(newRow, newColumn))
                    {
                        RevealCell(model, newRow, newColumn);
                    }
                }
            }
        }

        public void ToggleFlag(GameModel model, int row, int column)
        {
            if (!model.IsRevealed(row, column))
            {
                bool currentlyFlagged = model.IsFlagged(row, column);
                model.SetFlagged(row, column, !currentlyFlagged);
                model.FlagsPlaced += currentlyFlagged ? -1 : 1;
            }
        }

        private void CheckWinCondition(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (!model.HasMine(row, column) && !model.IsRevealed(row, column))
                    {
                        return; // Есть неоткрытые безопасные клетки
                    }
                }
            }

            model.GameState = GameState.Won;
        }
    }
}