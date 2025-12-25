using Game2048.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game2048.Business.Services
{
    public class GameService
    {
        private static readonly Random _rnd = new Random();

        public void Initialize(GameModel model)
        {
            model.Score = 0;
            model.IsGameOver = false;
            model.HasWon = false;

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = 0;
                }
            }

            AddRandomTile(model);
            AddRandomTile(model);
        }

        public bool MoveLeft(GameModel model)
        {
            bool moved = false;
            
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                var tiles = GetRow(model, row);
                var newTiles = SlideAndMerge(model, tiles);
                
                if (!tiles.SequenceEqual(newTiles))
                {
                    moved = true;
                    SetRow(model, row, newTiles);
                }
            }

            if (moved)
            {
                AddRandomTile(model);
                CheckGameState(model);
            }

            return moved;
        }

        public bool MoveRight(GameModel model)
        {
            bool moved = false;
            
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                var tiles = GetRow(model, row).Reverse().ToArray();
                var newTiles = SlideAndMerge(model, tiles);
                newTiles = newTiles.Reverse().ToArray();
                
                if (!GetRow(model, row).SequenceEqual(newTiles))
                {
                    moved = true;
                    SetRow(model, row, newTiles);
                }
            }

            if (moved)
            {
                AddRandomTile(model);
                CheckGameState(model);
            }

            return moved;
        }

        public bool MoveUp(GameModel model)
        {
            bool moved = false;
            
            for (int col = 0; col < GameModel.ColumnCount; col++)
            {
                var tiles = GetColumn(model, col);
                var newTiles = SlideAndMerge(model, tiles);
                
                if (!tiles.SequenceEqual(newTiles))
                {
                    moved = true;
                    SetColumn(model, col, newTiles);
                }
            }

            if (moved)
            {
                AddRandomTile(model);
                CheckGameState(model);
            }

            return moved;
        }

        public bool MoveDown(GameModel model)
        {
            bool moved = false;
            
            for (int col = 0; col < GameModel.ColumnCount; col++)
            {
                var tiles = GetColumn(model, col).Reverse().ToArray();
                var newTiles = SlideAndMerge(model, tiles);
                newTiles = newTiles.Reverse().ToArray();
                
                if (!GetColumn(model, col).SequenceEqual(newTiles))
                {
                    moved = true;
                    SetColumn(model, col, newTiles);
                }
            }

            if (moved)
            {
                AddRandomTile(model);
                CheckGameState(model);
            }

            return moved;
        }

        private int[] GetRow(GameModel model, int row)
        {
            int[] tiles = new int[GameModel.ColumnCount];
            for (int col = 0; col < GameModel.ColumnCount; col++)
            {
                tiles[col] = model[row, col];
            }
            return tiles;
        }

        private void SetRow(GameModel model, int row, int[] tiles)
        {
            for (int col = 0; col < GameModel.ColumnCount; col++)
            {
                model[row, col] = tiles[col];
            }
        }

        private int[] GetColumn(GameModel model, int col)
        {
            int[] tiles = new int[GameModel.RowCount];
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                tiles[row] = model[row, col];
            }
            return tiles;
        }

        private void SetColumn(GameModel model, int col, int[] tiles)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                model[row, col] = tiles[row];
            }
        }

        private int[] SlideAndMerge(GameModel model, int[] tiles)
        {
            var nonZeroTiles = tiles.Where(t => t != 0).ToList();
            var mergedTiles = new List<int>();
            
            for (int i = 0; i < nonZeroTiles.Count; i++)
            {
                if (i < nonZeroTiles.Count - 1 && nonZeroTiles[i] == nonZeroTiles[i + 1])
                {
                    mergedTiles.Add(nonZeroTiles[i] * 2);
                    model.Score += nonZeroTiles[i] * 2;
                    i++;
                }
                else
                {
                    mergedTiles.Add(nonZeroTiles[i]);
                }
            }
            
            while (mergedTiles.Count < GameModel.ColumnCount)
            {
                mergedTiles.Add(0);
            }
            
            return mergedTiles.ToArray();
        }

        private void AddRandomTile(GameModel model)
        {
            var emptyCells = new List<(int row, int col)>();
            
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int col = 0; col < GameModel.ColumnCount; col++)
                {
                    if (model[row, col] == 0)
                    {
                        emptyCells.Add((row, col));
                    }
                }
            }

            if (emptyCells.Count > 0)
            {
                var randomCell = emptyCells[_rnd.Next(emptyCells.Count)];
                model[randomCell.row, randomCell.col] = _rnd.Next(10) == 0 ? 4 : 2;
            }
        }

        private void CheckGameState(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int col = 0; col < GameModel.ColumnCount; col++)
                {
                    if (model[row, col] == 2048)
                    {
                        model.HasWon = true;
                        return;
                    }
                }
            }

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int col = 0; col < GameModel.ColumnCount; col++)
                {
                    if (model[row, col] == 0)
                    {
                        return;
                    }
                }
            }

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int col = 0; col < GameModel.ColumnCount; col++)
                {
                    var current = model[row, col];
                    if ((row < GameModel.RowCount - 1 && model[row + 1, col] == current) ||
                        (col < GameModel.ColumnCount - 1 && model[row, col + 1] == current))
                    {
                        return;
                    }
                }
            }

            model.IsGameOver = true;
        }
    }
}
