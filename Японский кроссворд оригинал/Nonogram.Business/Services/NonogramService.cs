using Nonogram.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Business.Services
{
    public class NonogramService
    {
        public void Initialize(NonogramModel model)
        {
            // Устанавливаем подсказки для столбцов
            int[][] columnClues = new int[][]
            {
                new int[] { 2 },
                new int[] { 4 },
                new int[] { 2, 2 },
                new int[] { 7 },
                new int[] { 1, 3, 2 },
                new int[] { 3, 3 },
                new int[] { 10 },
                new int[] { 12 },
                new int[] { 13 },
                new int[] { 4, 6, 3 },
                new int[] { 2, 4, 2 },
                new int[] { 1, 2, 1 },
                new int[] { 4 },
                new int[] { 8 },
                new int[] { 4, 5 }
            };

            // Устанавливаем подсказки для строк
            int[][] rowClues = new int[][]
            {
                new int[] { 3 },
                new int[] { 3, 1 },
                new int[] { 3, 1 },
                new int[] { 5, 2 },
                new int[] { 6, 3 },
                new int[] { 2, 6, 2 },
                new int[] { 4, 8 },
                new int[] { 2, 2, 8 },
                new int[] { 5, 5, 2 },
                new int[] { 3, 5, 2 },
                new int[] { 7, 2 },
                new int[] { 5, 1 },
                new int[] { 4, 1 },
                new int[] { 4 },
                new int[] { 3 }
            };

            // Очищаем старые подсказки
            model.RowClues.Clear();
            model.ColumnClues.Clear();

            // Добавляем новые подсказки
            foreach (var clue in rowClues)
            {
                model.RowClues.Add(new List<int>(clue));
            }

            foreach (var clue in columnClues)
            {
                model.ColumnClues.Add(new List<int>(clue));
            }

            // Генерируем решение на основе координат
            GenerateSolutionFromCoordinates(model);

            // Сбрасываем ошибки и пользовательскую сетку
            model.MistakesCount = 0;
            ResetUserGrid(model);
        }

        private void GenerateSolutionFromCoordinates(NonogramModel model)
        {
            // Инициализируем все клетки как пустые
            for (int row = 0; row < NonogramModel.Size; row++)
            {
                for (int col = 0; col < NonogramModel.Size; col++)
                {
                    model.SetSolution(row, col, NonogramModel.EmptyCell);
                }
            }

            // Координаты закрашенных квадратов (исправленные для нумерации с 0)
            var filledCells = new List<(int, int)>
            {
                // Первая часть координат (была)
                (0, 9), (0, 10), (0, 11),
                (1, 8), (1, 9), (1, 10),
                (2, 7), (2, 8), (2, 9),
                (3, 5), (3, 6), (3, 7), (3, 8), (3, 9),
                (4, 3), (4, 4), (4, 5), (4, 6), (4, 7), (4, 8),
                (5, 2), (5, 3), (5, 5), (5, 6), (5, 7), (5, 8), (5, 9), (5, 10), (5, 12), (5, 13),
                (6, 1), (6, 2), (6, 3), (6, 4), (6, 6), (6, 7), (6, 8), (6, 9), (6, 10), (6, 11), (6, 12), (6, 13),
                (7, 0), (7, 1), (7, 3), (7, 4), (7, 6), (7, 7), (7, 8), (7, 9), (7, 10), (7, 11), (7, 12), (7, 13),
                (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 6), (8, 7), (8, 8), (8, 9), (8, 10), (8, 13), (8, 14),
                (9, 1), (9, 2), (9, 3), (9, 5), (9, 6), (9, 7), (9, 8), (9, 9), (9, 13), (9, 14),
                (10, 3), (10, 4), (10, 5), (10, 6), (10, 7), (10, 8), (10, 9), (10, 13), (10, 14),
                (11, 4), (11, 5), (11, 6), (11, 7), (11, 8), (11, 14),
                (12, 6), (12, 7), (12, 8), (12, 9), (12, 14),
                (13, 7), (13, 8), (13, 9), (13, 10),
                (14, 9), (14, 10), (14, 11),
                
                // ДОБАВЛЕННЫЕ КООРДИНАТЫ:
                // (2, 15) -> (1, 14)  (т.к. в C# индексация с 0: строка 1, столбец 14)
                // (3, 15) -> (2, 14)
                // (4, 14) -> (3, 13)
                // (4, 15) -> (3, 14)
                // (5, 13) -> (4, 12)
                // (5, 14) -> (4, 13)
                // (5, 15) -> (4, 14)
                
                (1, 14),  // (2, 15)
                (2, 14),  // (3, 15)
                (3, 13),  // (4, 14)
                (3, 14),  // (4, 15)
                (4, 12),  // (5, 13)
                (4, 13),  // (5, 14)
                (4, 14)   // (5, 15)
            };

            // Закрашиваем указанные клетки
            foreach (var (row, col) in filledCells)
            {
                if (row >= 0 && row < NonogramModel.Size && col >= 0 && col < NonogramModel.Size)
                {
                    model.SetSolution(row, col, NonogramModel.FilledCell);
                }
            }

            // Проверяем, соответствует ли решение подсказкам
            ValidateSolution(model);
        }

        private void ValidateSolution(NonogramModel model)
        {
            Console.WriteLine("Проверка решения:");

            // Проверяем строки
            for (int row = 0; row < NonogramModel.Size; row++)
            {
                var rowClues = model.RowClues[row];
                var rowGroups = new List<int>();
                int currentGroup = 0;

                for (int col = 0; col < NonogramModel.Size; col++)
                {
                    if (model.GetSolution(row, col) == NonogramModel.FilledCell)
                    {
                        currentGroup++;
                    }
                    else if (currentGroup > 0)
                    {
                        rowGroups.Add(currentGroup);
                        currentGroup = 0;
                    }
                }

                if (currentGroup > 0)
                {
                    rowGroups.Add(currentGroup);
                }

                // Сравниваем с подсказками
                bool rowValid = rowClues.SequenceEqual(rowGroups);
                Console.WriteLine($"Строка {row + 1}: подсказки [{string.Join(",", rowClues)}], решение [{string.Join(",", rowGroups)}], valid: {rowValid}");
            }

            // Проверяем столбцы
            for (int col = 0; col < NonogramModel.Size; col++)
            {
                var colClues = model.ColumnClues[col];
                var colGroups = new List<int>();
                int currentGroup = 0;

                for (int row = 0; row < NonogramModel.Size; row++)
                {
                    if (model.GetSolution(row, col) == NonogramModel.FilledCell)
                    {
                        currentGroup++;
                    }
                    else if (currentGroup > 0)
                    {
                        colGroups.Add(currentGroup);
                        currentGroup = 0;
                    }
                }

                if (currentGroup > 0)
                {
                    colGroups.Add(currentGroup);
                }

                // Сравниваем с подсказками
                bool colValid = colClues.SequenceEqual(colGroups);
                Console.WriteLine($"Столбец {col + 1}: подсказки [{string.Join(",", colClues)}], решение [{string.Join(",", colGroups)}], valid: {colValid}");
            }
        }

        private void ResetUserGrid(NonogramModel model)
        {
            for (int row = 0; row < NonogramModel.Size; row++)
            {
                for (int col = 0; col < NonogramModel.Size; col++)
                {
                    model[row, col] = NonogramModel.EmptyCell;
                }
            }
        }

        public MoveResult MakeMove(NonogramModel model, int row, int column, bool isFill)
        {
            if (row < 0 || row >= NonogramModel.Size || column < 0 || column >= NonogramModel.Size)
                return MoveResult.Invalid;

            // Если клетка уже закрашена или с крестиком, ничего не делаем
            if (model[row, column] == NonogramModel.FilledCell || model[row, column] == NonogramModel.CrossedCell)
                return MoveResult.AlreadyFilled;

            // Проверяем правильность хода
            int solution = model.GetSolution(row, column);

            // Всегда пытаемся закрасить клетку (isFill всегда true в этой версии)
            if (solution == NonogramModel.FilledCell)
            {
                // Правильный ход - закрашиваем клетку
                model[row, column] = NonogramModel.FilledCell;
                return MoveResult.Correct;
            }
            else
            {
                // Ошибочный ход - ставим крестик и увеличиваем счетчик ошибок
                model[row, column] = NonogramModel.CrossedCell;
                model.MistakesCount++;
                return MoveResult.Wrong;
            }
        }

        public bool CheckRowComplete(NonogramModel model, int row)
        {
            // Проверяем, правильно ли угадана вся строка
            for (int col = 0; col < NonogramModel.Size; col++)
            {
                int solution = model.GetSolution(row, col);
                int user = model[row, col];

                // Если в решении клетка должна быть закрашена, но пользователь не закрасил
                if (solution == NonogramModel.FilledCell && user != NonogramModel.FilledCell)
                    return false;
            }

            return true;
        }

        public bool CheckColumnComplete(NonogramModel model, int column)
        {
            // Проверяем, правильно ли угадан весь столбец
            for (int row = 0; row < NonogramModel.Size; row++)
            {
                int solution = model.GetSolution(row, column);
                int user = model[row, column];

                // Если в решении клетка должна быть закрашена, но пользователь не закрасил
                if (solution == NonogramModel.FilledCell && user != NonogramModel.FilledCell)
                    return false;
            }

            return true;
        }

        public bool IsGameOver(NonogramModel model)
        {
            // Игра окончена, если сделано 5 ошибок
            return model.MistakesCount >= 5;
        }

        public bool IsGameWon(NonogramModel model)
        {
            // Проверяем, совпадает ли вся сетка пользователя с решением
            for (int row = 0; row < NonogramModel.Size; row++)
            {
                for (int col = 0; col < NonogramModel.Size; col++)
                {
                    int solution = model.GetSolution(row, col);
                    int user = model[row, col];

                    // Для закрашенных клеток должно быть точное совпадение
                    if (solution == NonogramModel.FilledCell && user != NonogramModel.FilledCell)
                        return false;
                }
            }

            return true;
        }
    }

    public enum MoveResult
    {
        Invalid,
        AlreadyFilled,
        Correct,
        Wrong
    }
}