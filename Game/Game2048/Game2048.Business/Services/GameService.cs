using Game2048.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game2048.Business.Services
{
    public class GameService
    {
        private Random _random = new Random();

        public void Initialize(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = 0;
                }
            }

            model.Score = 0;
            AddRandomTile(model);
            AddRandomTile(model);
        }

        private void AddRandomTile(GameModel model)
        {
            var emptyCells = new List<(int row, int column)>();

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] == 0)
                    {
                        emptyCells.Add((row, column));
                    }
                }
            }

            if (emptyCells.Count > 0)
            {
                var (row, column) = emptyCells[_random.Next(emptyCells.Count)];
                model[row, column] = _random.Next(10) < 9 ? 2 : 4;
            }
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            bool moved = false;

            switch (direction)
            {
                case MoveDirection.Left:
                    moved = MoveLeft(model);
                    break;
                case MoveDirection.Right:
                    moved = MoveRight(model);
                    break;
                case MoveDirection.Up:
                    moved = MoveUp(model);
                    break;
                case MoveDirection.Down:
                    moved = MoveDown(model);
                    break;
            }

            if (moved)
            {
                AddRandomTile(model);
            }

            return moved;
        }

        private bool MoveLeft(GameModel model)
        {
            bool moved = false;

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                var tiles = GetRowTiles(model, row);
                var merged = MergeTiles(tiles);
                // Дополняем список нулями справа
                var newRow = merged.Concat(Enumerable.Repeat(0, GameModel.ColumnCount - merged.Count)).ToArray();

                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] != newRow[column])
                    {
                        moved = true;
                        model[row, column] = newRow[column];
                    }
                }
            }

            return moved;
        }

        private bool MoveRight(GameModel model)
        {
            bool moved = false;

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                var tiles = GetRowTiles(model, row);
                var merged = MergeTiles(tiles);
                var newRow = Enumerable.Repeat(0, GameModel.ColumnCount - merged.Count).Concat(merged).ToArray();

                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] != newRow[column])
                    {
                        moved = true;
                        model[row, column] = newRow[column];
                    }
                }
            }

            return moved;
        }

        private bool MoveUp(GameModel model)
        {
            bool moved = false;

            for (int column = 0; column < GameModel.ColumnCount; column++)
            {
                var tiles = GetColumnTiles(model, column);
                var merged = MergeTiles(tiles);
                var newColumn = merged.Concat(Enumerable.Repeat(0, GameModel.RowCount - merged.Count)).ToArray();

                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    if (model[row, column] != newColumn[row])
                    {
                        moved = true;
                        model[row, column] = newColumn[row];
                    }
                }
            }

            return moved;
        }

        private bool MoveDown(GameModel model)
        {
            bool moved = false;

            for (int column = 0; column < GameModel.ColumnCount; column++)
            {
                var tiles = GetColumnTiles(model, column);
                var merged = MergeTiles(tiles);
                var newColumn = Enumerable.Repeat(0, GameModel.RowCount - merged.Count).Concat(merged).ToArray();

                for (int row = 0; row < GameModel.RowCount; row++)
                {
                    if (model[row, column] != newColumn[row])
                    {
                        moved = true;
                        model[row, column] = newColumn[row];
                    }
                }
            }

            return moved;
        }

        private List<int> GetRowTiles(GameModel model, int row)
        {
            var tiles = new List<int>();
            for (int column = 0; column < GameModel.ColumnCount; column++)
            {
                if (model[row, column] != 0)
                {
                    tiles.Add(model[row, column]);
                }
            }
            return tiles;
        }

        private List<int> GetColumnTiles(GameModel model, int column)
        {
            var tiles = new List<int>();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                if (model[row, column] != 0)
                {
                    tiles.Add(model[row, column]);
                }
            }
            return tiles;
        }

        private List<int> MergeTiles(List<int> tiles)
        {
            var result = new List<int>();
            var merged = new bool[tiles.Count];

            for (int i = 0; i < tiles.Count; i++)
            {
                if (merged[i]) continue;

                int current = tiles[i];
                
                for (int j = i + 1; j < tiles.Count; j++)
                {
                    if (merged[j]) continue;

                    if (tiles[j] == current)
                    {
                        result.Add(current * 2);
                        merged[i] = true;
                        merged[j] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                if (!merged[i])
                {
                    result.Add(current);
                }
            }

            return result;
        }

        public bool IsGameOver(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] == 0)
                        return false;
                }
            }

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    int current = model[row, column];

                    if (column < GameModel.ColumnCount - 1 && model[row, column + 1] == current)
                        return false;

                    if (row < GameModel.RowCount - 1 && model[row + 1, column] == current)
                        return false;
                }
            }

            return true;
        }

        public bool HasWon(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] == 2048)
                        return true;
                }
            }
            return false;
        }
    }
}
