using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business
{
    public class Game2048Service
    {
        private static readonly Random _rnd = new Random();

        public GameDto InitializeGame()
        {
            var game = new GameDto
            {
                Score = 0,
                IsGameOver = false,
                HasWon = false,
                Cells = new int[Constants.RowCount * Constants.ColumnCount]
            };

            // Initialize empty board
            for (int i = 0; i < Constants.RowCount * Constants.ColumnCount; i++)
            {
                game.Cells[i] = Constants.FreeCellValue;
            }

            // Add two random tiles
            AddRandomTile(game);
            AddRandomTile(game);

            // Логирование для отладки
            System.Diagnostics.Debug.WriteLine("Game initialized with tiles:");
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int col = 0; col < Constants.ColumnCount; col++)
                {
                    int index = row * Constants.ColumnCount + col;
                    System.Diagnostics.Debug.Write($"[{game.Cells[index]}] ");
                }
                System.Diagnostics.Debug.WriteLine("");
            }

            return game;
        }

        public GameDto MakeMove(GameDto game, string direction)
        {
            if (game.IsGameOver || game.HasWon)
                return game;

            bool moved = false;

            switch (direction.ToLower())
            {
                case "left":
                    moved = MoveLeft(game);
                    break;
                case "right":
                    moved = MoveRight(game);
                    break;
                case "up":
                    moved = MoveUp(game);
                    break;
                case "down":
                    moved = MoveDown(game);
                    break;
            }

            if (moved)
            {
                AddRandomTile(game);
                CheckGameState(game);
            }

            return game;
        }

        private bool MoveLeft(GameDto game)
        {
            bool moved = false;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                var tiles = GetRow(game, row);
                var newTiles = SlideAndMerge(game, tiles);

                if (!tiles.SequenceEqual(newTiles))
                {
                    moved = true;
                    SetRow(game, row, newTiles);
                }
            }

            return moved;
        }

        private bool MoveRight(GameDto game)
        {
            bool moved = false;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                var tiles = GetRow(game, row).Reverse().ToArray();
                var newTiles = SlideAndMerge(game, tiles);
                newTiles = newTiles.Reverse().ToArray();

                if (!GetRow(game, row).SequenceEqual(newTiles))
                {
                    moved = true;
                    SetRow(game, row, newTiles);
                }
            }

            return moved;
        }

        private bool MoveUp(GameDto game)
        {
            bool moved = false;

            for (int col = 0; col < Constants.ColumnCount; col++)
            {
                var tiles = GetColumn(game, col);
                var newTiles = SlideAndMerge(game, tiles);

                if (!tiles.SequenceEqual(newTiles))
                {
                    moved = true;
                    SetColumn(game, col, newTiles);
                }
            }

            return moved;
        }

        private bool MoveDown(GameDto game)
        {
            bool moved = false;

            for (int col = 0; col < Constants.ColumnCount; col++)
            {
                var tiles = GetColumn(game, col).Reverse().ToArray();
                var newTiles = SlideAndMerge(game, tiles);
                newTiles = newTiles.Reverse().ToArray();

                if (!GetColumn(game, col).SequenceEqual(newTiles))
                {
                    moved = true;
                    SetColumn(game, col, newTiles);
                }
            }

            return moved;
        }

        private int[] GetRow(GameDto game, int row)
        {
            if (game?.Cells == null)
            {
                System.Diagnostics.Debug.WriteLine("Game or Cells is null in GetRow");
                return new int[Constants.ColumnCount];
            }
            
            int[] tiles = new int[Constants.ColumnCount];
            for (int col = 0; col < Constants.ColumnCount; col++)
            {
                int index = row * Constants.ColumnCount + col;
                tiles[col] = game.Cells[index];
            }
            return tiles;
        }

        private void SetRow(GameDto game, int row, int[] tiles)
        {
            if (game?.Cells == null)
            {
                System.Diagnostics.Debug.WriteLine("Game or Cells is null in SetRow");
                return;
            }
            
            for (int col = 0; col < Constants.ColumnCount; col++)
            {
                int index = row * Constants.ColumnCount + col;
                game.Cells[index] = tiles[col];
            }
        }

        private int[] GetColumn(GameDto game, int col)
        {
            if (game?.Cells == null)
            {
                System.Diagnostics.Debug.WriteLine("Game or Cells is null in GetColumn");
                return new int[Constants.RowCount];
            }
            
            int[] tiles = new int[Constants.RowCount];
            for (int row = 0; row < Constants.RowCount; row++)
            {
                int index = row * Constants.ColumnCount + col;
                tiles[row] = game.Cells[index];
            }
            return tiles;
        }

        private void SetColumn(GameDto game, int col, int[] tiles)
        {
            if (game?.Cells == null)
            {
                System.Diagnostics.Debug.WriteLine("Game or Cells is null in SetColumn");
                return;
            }
            
            for (int row = 0; row < Constants.RowCount; row++)
            {
                int index = row * Constants.ColumnCount + col;
                game.Cells[index] = tiles[row];
            }
        }

        private int[] SlideAndMerge(GameDto game, int[] tiles)
        {
            var nonZeroTiles = tiles.Where(t => t != Constants.FreeCellValue).ToList();
            var mergedTiles = new List<int>();

            for (int i = 0; i < nonZeroTiles.Count; i++)
            {
                if (i < nonZeroTiles.Count - 1 && nonZeroTiles[i] == nonZeroTiles[i + 1])
                {
                    int mergedValue = nonZeroTiles[i] * 2;
                    mergedTiles.Add(mergedValue);
                    game.Score += mergedValue;
                    i++;
                }
                else
                {
                    mergedTiles.Add(nonZeroTiles[i]);
                }
            }

            while (mergedTiles.Count < Constants.ColumnCount)
            {
                mergedTiles.Add(Constants.FreeCellValue);
            }

            return mergedTiles.ToArray();
        }

        private void AddRandomTile(GameDto game)
        {
            if (game?.Cells == null)
            {
                System.Diagnostics.Debug.WriteLine("Game or Cells is null in AddRandomTile");
                return;
            }
            
            var emptyCells = new List<(int row, int col)>();

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int col = 0; col < Constants.ColumnCount; col++)
                {
                    int index = row * Constants.ColumnCount + col;
                    if (game.Cells[index] == Constants.FreeCellValue)
                    {
                        emptyCells.Add((row, col));
                    }
                }
            }

            if (emptyCells.Count > 0)
            {
                var randomCell = emptyCells[_rnd.Next(emptyCells.Count)];
                var tileValue = _rnd.Next(10) == 0 ? 4 : 2;
                int index = randomCell.row * Constants.ColumnCount + randomCell.col;
                game.Cells[index] = tileValue;
                System.Diagnostics.Debug.WriteLine($"Added tile {tileValue} at position ({randomCell.row}, {randomCell.col})");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No empty cells available for tile placement");
            }
        }

        private void CheckGameState(GameDto game)
        {
            // Check for win
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int col = 0; col < Constants.ColumnCount; col++)
                {
                    int index = row * Constants.ColumnCount + col;
                    if (game.Cells[index] == Constants.WinValue)
                    {
                        game.HasWon = true;
                        return;
                    }
                }
            }

            // Check for empty cells
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int col = 0; col < Constants.ColumnCount; col++)
                {
                    int index = row * Constants.ColumnCount + col;
                    if (game.Cells[index] == Constants.FreeCellValue)
                    {
                        return;
                    }
                }
            }

            // Check for possible merges
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int col = 0; col < Constants.ColumnCount - 1; col++)
                {
                    int index1 = row * Constants.ColumnCount + col;
                    int index2 = row * Constants.ColumnCount + (col + 1);
                    if (game.Cells[index1] != Constants.FreeCellValue && game.Cells[index1] == game.Cells[index2])
                    {
                        return;
                    }
                }
            }

            // Check for possible merges vertically
            for (int col = 0; col < Constants.ColumnCount; col++)
            {
                for (int row = 0; row < Constants.RowCount - 1; row++)
                {
                    int index1 = row * Constants.ColumnCount + col;
                    int index2 = (row + 1) * Constants.ColumnCount + col;
                    if (game.Cells[index1] != Constants.FreeCellValue && game.Cells[index1] == game.Cells[index2])
                    {
                        return;
                    }
                }
            }

            game.IsGameOver = true;
        }
    }
}
