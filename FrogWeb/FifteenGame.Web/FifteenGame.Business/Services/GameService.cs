using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private readonly Random _random = new Random();

        public void Initialize(GameField field)
        {
            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int column = 0; column < GameField.ColumnCount; column++)
                {
                    field[row, column] = CellType.Water;
                }
            }

            field.FrogRow = 0;
            field.FrogColumn = 0;
            field[field.FrogRow, field.FrogColumn] = CellType.Frog;

            field.HomeRow = GameField.RowCount - 1;
            field.HomeColumn = GameField.ColumnCount - 1;
            field[field.HomeRow, field.HomeColumn] = CellType.Home;

            CreateLilyPadPath(field);
            AddRandomAlgae(field, 10);

            field.MovesCount = 0;
            field.IsGameOver = false;
            field.IsWin = false;
            field.SelectedLilyPadRow = null;
            field.SelectedLilyPadColumn = null;
        }

        private void CreateLilyPadPath(GameField field)
        {
            var path = FindPath(field.FrogRow, field.FrogColumn, field.HomeRow, field.HomeColumn);

            foreach (var (row, column) in path)
            {
                if (field[row, column] != CellType.Frog && field[row, column] != CellType.Home)
                {
                    field[row, column] = CellType.LilyPad;
                }
            }

            for (int i = 0; i < 15; i++)
            {
                int row = _random.Next(GameField.RowCount);
                int column = _random.Next(GameField.ColumnCount);
                if (field[row, column] == CellType.Water)
                {
                    field[row, column] = CellType.LilyPad;
                }
            }
        }

        private List<(int row, int column)> FindPath(int startRow, int startCol, int endRow, int endCol)
        {
            var path = new List<(int, int)>();
            int currentRow = startRow;
            int currentCol = startCol;

            while (currentRow != endRow || currentCol != endCol)
            {
                if (currentRow < endRow) currentRow++;
                else if (currentRow > endRow) currentRow--;

                if (currentCol < endCol) currentCol++;
                else if (currentCol > endCol) currentCol--;

                if (currentRow != endRow || currentCol != endCol)
                {
                    path.Add((currentRow, currentCol));
                }
            }

            return path;
        }

        private void AddRandomAlgae(GameField field, int count)
        {
            int placed = 0;
            while (placed < count)
            {
                int row = _random.Next(GameField.RowCount);
                int column = _random.Next(GameField.ColumnCount);

                if (field[row, column] == CellType.LilyPad)
                {
                    field[row, column] = CellType.Algae;
                    placed++;
                }
            }
        }
        private void CheckAndExecuteWin(GameField field)
        {
            if (field.IsGameOver) return;

            if (HasClearPathToHome(field))
            {
                MoveFrogAlongPathToHome(field);
                field.IsGameOver = true;
                field.IsWin = true;
            }
            else if (IsFrogNextToHome(field))
            {
                MoveFrogToHome(field);
                field.IsGameOver = true;
                field.IsWin = true;
            }
        }

        private bool HasClearPathToHome(GameField field)
        {
            var visited = new bool[GameField.RowCount, GameField.ColumnCount];
            return CheckClearPath(field.FrogRow, field.FrogColumn, visited, field);
        }

        private bool CheckClearPath(int row, int col, bool[,] visited, GameField field)
        {
            if (row < 0 || row >= GameField.RowCount || col < 0 || col >= GameField.ColumnCount)
                return false;

            if (visited[row, col])
                return false;

            visited[row, col] = true;

            if (row == field.HomeRow && col == field.HomeColumn)
                return true;

            var cellType = field[row, col];

            if (cellType != CellType.LilyPad && cellType != CellType.Frog && cellType != CellType.Home)
                return false;

            return CheckClearPath(row + 1, col, visited, field) ||
                   CheckClearPath(row - 1, col, visited, field) ||
                   CheckClearPath(row, col + 1, visited, field) ||
                   CheckClearPath(row, col - 1, visited, field);
        }

        private void MoveFrogAlongPathToHome(GameField field)
        {
            var path = FindShortestPathToHome(field);

            if (path != null && path.Count > 0)
            {
                foreach (var (row, col) in path)
                {
                    MoveFrogTo(field, row, col);
                }
            }
        }

        private List<(int row, int column)> FindShortestPathToHome(GameField field)
        {
            var visited = new bool[GameField.RowCount, GameField.ColumnCount];
            var queue = new Queue<List<(int, int)>>();

            queue.Enqueue(new List<(int, int)> { (field.FrogRow, field.FrogColumn) });
            visited[field.FrogRow, field.FrogColumn] = true;

            while (queue.Count > 0)
            {
                var path = queue.Dequeue();
                var (row, col) = path.Last();

                if (row == field.HomeRow && col == field.HomeColumn)
                {
                    return path.Skip(1).ToList(); 
                }

                foreach (var (dr, dc) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
                {
                    int newRow = row + dr;
                    int newCol = col + dc;

                    if (newRow >= 0 && newRow < GameField.RowCount &&
                        newCol >= 0 && newCol < GameField.ColumnCount &&
                        !visited[newRow, newCol] &&
                        (field[newRow, newCol] == CellType.LilyPad || field[newRow, newCol] == CellType.Home))
                    {
                        visited[newRow, newCol] = true;
                        var newPath = new List<(int, int)>(path) { (newRow, newCol) };
                        queue.Enqueue(newPath);
                    }
                }
            }

            return null;
        }

        public bool RemoveAlgae(GameField field, int row, int column)
        {
            if (field.IsGameOver || field[row, column] != CellType.Algae)
                return false;

            field[row, column] = CellType.LilyPad;
            field.MovesCount++;
            field.SelectedLilyPadRow = null;
            field.SelectedLilyPadColumn = null;

            CheckAndExecuteWin(field);

            if (!field.IsGameOver)
            {
                MoveFrogOneStep(field);
                CheckAndExecuteWin(field);
            }

            return true;
        }

        public bool SelectLilyPad(GameField field, int row, int column)
        {
            if (field.IsGameOver || field[row, column] != CellType.LilyPad)
                return false;

            if (field.SelectedLilyPadRow == row && field.SelectedLilyPadColumn == column)
            {
                field.SelectedLilyPadRow = null;
                field.SelectedLilyPadColumn = null;
                return true;
            }

            field.SelectedLilyPadRow = row;
            field.SelectedLilyPadColumn = column;
            return true;
        }

        public bool MoveLilyPad(GameField field, int targetRow, int targetColumn)
        {
            if (field.IsGameOver ||
                !field.SelectedLilyPadRow.HasValue ||
                !field.SelectedLilyPadColumn.HasValue ||
                field[targetRow, targetColumn] != CellType.Water)
                return false;

            int sourceRow = field.SelectedLilyPadRow.Value;
            int sourceColumn = field.SelectedLilyPadColumn.Value;

            field[targetRow, targetColumn] = CellType.LilyPad;
            field[sourceRow, sourceColumn] = CellType.Water;
            field.MovesCount++;
            field.SelectedLilyPadRow = null;
            field.SelectedLilyPadColumn = null;

            CheckAndExecuteWin(field);

            if (!field.IsGameOver)
            {
                MoveFrogOneStep(field);
                CheckAndExecuteWin(field);
            }

            return true;
        }

        public void MoveFrogOneStep(GameField field)
        {
            if (field.IsGameOver) return;

            int currentRow = field.FrogRow;
            int currentCol = field.FrogColumn;

            if (currentRow < field.HomeRow && field[currentRow + 1, currentCol] == CellType.LilyPad)
            {
                MoveFrogTo(field, currentRow + 1, currentCol);
            }
            else if (currentCol < field.HomeColumn && field[currentRow, currentCol + 1] == CellType.LilyPad)
            {
                MoveFrogTo(field, currentRow, currentCol + 1);
            }
            else if (currentRow > field.HomeRow && field[currentRow - 1, currentCol] == CellType.LilyPad)
            {
                MoveFrogTo(field, currentRow - 1, currentCol);
            }
            else if (currentCol > field.HomeColumn && field[currentRow, currentCol - 1] == CellType.LilyPad)
            {
                MoveFrogTo(field, currentRow, currentCol - 1);
            }
        }

        private bool IsFrogNextToHome(GameField field)
        {
            int frogRow = field.FrogRow;
            int frogCol = field.FrogColumn;
            int homeRow = field.HomeRow;
            int homeCol = field.HomeColumn;

            return (frogRow == homeRow && frogCol == homeCol - 1) ||
                   (frogRow == homeRow && frogCol == homeCol + 1) ||
                   (frogRow == homeRow - 1 && frogCol == homeCol) ||
                   (frogRow == homeRow + 1 && frogCol == homeCol);
        }

        private void MoveFrogToHome(GameField field)
        {
            field[field.FrogRow, field.FrogColumn] = CellType.LilyPad;
            field.FrogRow = field.HomeRow;
            field.FrogColumn = field.HomeColumn;
            field[field.HomeRow, field.HomeColumn] = CellType.Frog;
        }

        private void MoveFrogTo(GameField field, int newRow, int newCol)
        {
            field[field.FrogRow, field.FrogColumn] = CellType.LilyPad;
            field.FrogRow = newRow;
            field.FrogColumn = newCol;
            field[newRow, newCol] = CellType.Frog;
        }
    }
}