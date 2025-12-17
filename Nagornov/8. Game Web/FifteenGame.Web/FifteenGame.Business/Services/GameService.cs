using FifteenGame.Business.Models;
using System;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private Random _random = new Random();

        public void Initialize(GameField field, int minesCount)
        {
            field.TotalMines = minesCount;

            // Очистка поля
            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int column = 0; column < GameField.ColumnCount; column++)
                {
                    field[row, column] = 0;
                    field.SetRevealed(row, column, false);
                    field.SetFlagged(row, column, false);
                    field.SetMine(row, column, false);
                }
            }

            // Расстановка мин
            int minesPlaced = 0;
            while (minesPlaced < field.TotalMines)
            {
                int row = _random.Next(GameField.RowCount);
                int column = _random.Next(GameField.ColumnCount);

                if (!field.HasMine(row, column))
                {
                    field.SetMine(row, column, true);
                    minesPlaced++;
                }
            }

            // Расчет соседних мин
            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int column = 0; column < GameField.ColumnCount; column++)
                {
                    if (!field.HasMine(row, column))
                    {
                        field[row, column] = CalculateAdjacentMines(field, row, column);
                    }
                }
            }

            field.GameState = GameState.Playing;
            field.FlagsPlaced = 0;
        }

        public void Initialize(GameField field)
        {
            Initialize(field, 15);
        }

        private int CalculateAdjacentMines(GameField field, int row, int column)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newColumn = column + j;

                    if (newRow >= 0 && newRow < GameField.RowCount &&
                        newColumn >= 0 && newColumn < GameField.ColumnCount &&
                        field.HasMine(newRow, newColumn))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public void RevealCell(GameField field, int row, int column)
        {
            if (field.IsRevealed(row, column) || field.IsFlagged(row, column))
                return;

            field.SetRevealed(row, column, true);

            if (field.HasMine(row, column))
            {
                field.GameState = GameState.GameOver;
                return;
            }

            // Если клетка пустая (0 мин вокруг), открываем соседние клетки рекурсивно
            if (field[row, column] == 0)
            {
                RevealAdjacentCells(field, row, column);
            }

            CheckWinCondition(field);
        }

        private void RevealAdjacentCells(GameField field, int row, int column)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newColumn = column + j;

                    if (newRow >= 0 && newRow < GameField.RowCount &&
                        newColumn >= 0 && newColumn < GameField.ColumnCount &&
                        !field.IsRevealed(newRow, newColumn) &&
                        !field.IsFlagged(newRow, newColumn))
                    {
                        RevealCell(field, newRow, newColumn);
                    }
                }
            }
        }

        public void ToggleFlag(GameField field, int row, int column)
        {
            if (!field.IsRevealed(row, column))
            {
                bool currentlyFlagged = field.IsFlagged(row, column);
                field.SetFlagged(row, column, !currentlyFlagged);
                field.FlagsPlaced += currentlyFlagged ? -1 : 1;
            }
        }

        private void CheckWinCondition(GameField field)
        {
            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int column = 0; column < GameField.ColumnCount; column++)
                {
                    if (!field.HasMine(row, column) && !field.IsRevealed(row, column))
                    {
                        return; // Есть неоткрытые безопасные клетки
                    }
                }
            }

            field.GameState = GameState.Won;
        }
    }
}