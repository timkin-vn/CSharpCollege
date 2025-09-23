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
        private Random _random = new Random();

        public void InitializeGame(MineField field)
        {
            // Создаем массив ячеек
            field.Cells = new GameCell[field.Rows, field.Columns];
            field.State = GameState.NotStarted;
            field.RevealedCellsCount = 0;
            field.FlaggedMinesCount = 0;

            // Инициализируем все ячейки
            for (int row = 0; row < field.Rows; row++)
            {
                for (int col = 0; col < field.Columns; col++)
                {
                    field.Cells[row, col] = new GameCell
                    {
                        Row = row,
                        Column = col,
                        HasMine = false,
                        IsRevealed = false,
                        IsFlagged = false,
                        AdjacentMinesCount = 0
                    };
                }
            }

            // Расставляем мины
            PlaceMines(field);

            // Подсчитываем соседние мины для каждой ячейки
            CalculateAdjacentMines(field);
        }

        private void PlaceMines(MineField field)
        {
            int minesPlaced = 0;
            while (minesPlaced < field.MineCount)
            {
                int row = _random.Next(field.Rows);
                int col = _random.Next(field.Columns);

                if (!field.Cells[row, col].HasMine)
                {
                    field.Cells[row, col].HasMine = true;
                    minesPlaced++;
                }
            }
        }

        private void CalculateAdjacentMines(MineField field)
        {
            for (int row = 0; row < field.Rows; row++)
            {
                for (int col = 0; col < field.Columns; col++)
                {
                    if (!field.Cells[row, col].HasMine)
                    {
                        field.Cells[row, col].AdjacentMinesCount = CountAdjacentMines(field, row, col);
                    }
                }
            }
        }

        private int CountAdjacentMines(MineField field, int row, int col)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int newRow = row + i;
                    int newCol = col + j;

                    if (newRow >= 0 && newRow < field.Rows && newCol >= 0 && newCol < field.Columns)
                    {
                        if (field.Cells[newRow, newCol].HasMine)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        public bool RevealCell(MineField field, int row, int col)
        {
            if (field.State == GameState.NotStarted)
                field.State = GameState.InProgress;

            if (field.State != GameState.InProgress)
                return false;

            var cell = field.Cells[row, col];

            if (cell.IsRevealed || cell.IsFlagged)
                return false;

            cell.IsRevealed = true;
            field.RevealedCellsCount++;

            // Если наступили на мину - игра проиграна
            if (cell.HasMine)
            {
                field.State = GameState.Defeat;
                RevealAllMines(field);
                return true;
            }

            // Если ячейка пустая - открываем соседние пустые ячейки рекурсивно
            if (cell.AdjacentMinesCount == 0)
            {
                RevealAdjacentCells(field, row, col);
            }

            // Проверяем победу
            CheckForVictory(field);

            return true;
        }

        private void RevealAdjacentCells(MineField field, int row, int col)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;

                    int newRow = row + i;
                    int newCol = col + j;

                    if (newRow >= 0 && newRow < field.Rows && newCol >= 0 && newCol < field.Columns)
                    {
                        var adjacentCell = field.Cells[newRow, newCol];
                        if (!adjacentCell.IsRevealed && !adjacentCell.IsFlagged)
                        {
                            adjacentCell.IsRevealed = true;
                            field.RevealedCellsCount++;

                            if (adjacentCell.AdjacentMinesCount == 0)
                            {
                                RevealAdjacentCells(field, newRow, newCol);
                            }
                        }
                    }
                }
            }
        }

        public void ToggleFlag(MineField field, int row, int col)
        {
            if (field.State != GameState.InProgress && field.State != GameState.NotStarted)
                return;

            var cell = field.Cells[row, col];

            if (cell.IsRevealed)
                return;

            cell.IsFlagged = !cell.IsFlagged;

            if (cell.IsFlagged && cell.HasMine)
                field.FlaggedMinesCount++;
            else if (!cell.IsFlagged && cell.HasMine)
                field.FlaggedMinesCount--;

            // Проверяем победу
            CheckForVictory(field);
        }

        private void CheckForVictory(MineField field)
        {
            // Победа: все мины помечены флажками и все безопасные ячейки открыты
            int totalSafeCells = field.Rows * field.Columns - field.MineCount;
            if (field.RevealedCellsCount == totalSafeCells && field.FlaggedMinesCount == field.MineCount)
            {
                field.State = GameState.Victory;
            }
        }

        private void RevealAllMines(MineField field)
        {
            for (int row = 0; row < field.Rows; row++)
            {
                for (int col = 0; col < field.Columns; col++)
                {
                    if (field.Cells[row, col].HasMine)
                    {
                        field.Cells[row, col].IsRevealed = true;
                    }
                }
            }
        }

        public void StartNewGame(MineField field)
        {
            InitializeGame(field);
        }
    }
}