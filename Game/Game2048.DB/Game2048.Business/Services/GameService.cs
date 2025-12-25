using Game2048.Common.Models;
using Game2048.Common.Dtos;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game2048.Common.Services
{
    public class GameService
    {
        private readonly Random _random = new Random();

        public GameModel InitializeGame()
        {
            var game = new GameModel();
            AddNewTile(game);
            AddNewTile(game);
            return game;
        }

        public GameModel Move(GameModel game, MoveDirection direction)
        {
            var newGame = CloneGame(game);
            bool moved = false;

            switch (direction)
            {
                case MoveDirection.Left:
                    moved = MoveLeft(newGame);
                    break;
                case MoveDirection.Right:
                    moved = MoveRight(newGame);
                    break;
                case MoveDirection.Up:
                    moved = MoveUp(newGame);
                    break;
                case MoveDirection.Down:
                    moved = MoveDown(newGame);
                    break;
            }

            if (moved)
            {
                AddNewTile(newGame);
                CheckGameState(newGame);
            }

            return newGame;
        }

        private bool MoveLeft(GameModel game)
        {
            bool moved = false;
            for (int row = 0; row < 4; row++)
            {
                var tiles = GetRowTiles(game, row);
                var merged = MergeTiles(tiles, game);
                var newRow = FillRow(merged);
                
                if (!AreArraysEqual(tiles, newRow))
                {
                    moved = true;
                    SetRowTiles(game, row, newRow);
                }
            }
            return moved;
        }

        private bool MoveRight(GameModel game)
        {
            bool moved = false;
            for (int row = 0; row < 4; row++)
            {
                var tiles = GetRowTiles(game, row);
                Array.Reverse(tiles);
                var merged = MergeTiles(tiles, game);
                var newRow = FillRow(merged);
                Array.Reverse(newRow);
                
                if (!AreArraysEqual(GetRowTiles(game, row), newRow))
                {
                    moved = true;
                    SetRowTiles(game, row, newRow);
                }
            }
            return moved;
        }

        private bool MoveUp(GameModel game)
        {
            bool moved = false;
            for (int col = 0; col < 4; col++)
            {
                var tiles = GetColumnTiles(game, col);
                var merged = MergeTiles(tiles, game);
                var newCol = FillRow(merged);
                
                if (!AreArraysEqual(tiles, newCol))
                {
                    moved = true;
                    SetColumnTiles(game, col, newCol);
                }
            }
            return moved;
        }

        private bool MoveDown(GameModel game)
        {
            bool moved = false;
            for (int col = 0; col < 4; col++)
            {
                var tiles = GetColumnTiles(game, col);
                Array.Reverse(tiles);
                var merged = MergeTiles(tiles, game);
                var newCol = FillRow(merged);
                Array.Reverse(newCol);
                
                if (!AreArraysEqual(GetColumnTiles(game, col), newCol))
                {
                    moved = true;
                    SetColumnTiles(game, col, newCol);
                }
            }
            return moved;
        }

        private int[] GetRowTiles(GameModel game, int row)
        {
            var tiles = new int[4];
            for (int col = 0; col < 4; col++)
            {
                tiles[col] = game.Board[row, col];
            }
            return tiles;
        }

        private int[] GetColumnTiles(GameModel game, int col)
        {
            var tiles = new int[4];
            for (int row = 0; row < 4; row++)
            {
                tiles[row] = game.Board[row, col];
            }
            return tiles;
        }

        private void SetRowTiles(GameModel game, int row, int[] tiles)
        {
            for (int col = 0; col < 4; col++)
            {
                game.Board[row, col] = tiles[col];
            }
        }

        private void SetColumnTiles(GameModel game, int col, int[] tiles)
        {
            for (int row = 0; row < 4; row++)
            {
                game.Board[row, col] = tiles[row];
            }
        }

        private int[] MergeTiles(int[] tiles, GameModel game)
        {
            var result = new int[4];
            int index = 0;

            for (int i = 0; i < 4; i++)
            {
                if (tiles[i] == 0) continue;

                if (index > 0 && result[index - 1] == tiles[i])
                {
                    result[index - 1] *= 2;
                    game.Score += result[index - 1]; // Добавляем очки при слиянии
                }
                else
                {
                    result[index] = tiles[i];
                    index++;
                }
            }

            return result;
        }

        private int[] FillRow(int[] merged)
        {
            var result = new int[4];
            for (int i = 0; i < merged.Length; i++)
            {
                result[i] = merged[i];
            }
            return result;
        }

        private bool AreArraysEqual(int[] arr1, int[] arr2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (arr1[i] != arr2[i]) return false;
            }
            return true;
        }

        private void AddNewTile(GameModel game)
        {
            var emptyCells = new List<(int row, int col)>();
            
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (game.Board[row, col] == 0)
                    {
                        emptyCells.Add((row, col));
                    }
                }
            }

            if (emptyCells.Count > 0)
            {
                var randomCell = emptyCells[_random.Next(emptyCells.Count)];
                game.Board[randomCell.row, randomCell.col] = _random.Next(10) == 0 ? 4 : 2;
            }
        }

        private void CheckGameState(GameModel game)
        {
            // Check for win
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (game.Board[row, col] == 2048)
                    {
                        game.IsWon = true;
                        return;
                    }
                }
            }

            // Check for game over
            if (!HasEmptyCells(game) && !CanMove(game))
            {
                game.IsGameOver = true;
            }
        }

        private bool HasEmptyCells(GameModel game)
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (game.Board[row, col] == 0) return true;
                }
            }
            return false;
        }

        private bool CanMove(GameModel game)
        {
            // Check horizontal moves
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (game.Board[row, col] == game.Board[row, col + 1]) return true;
                }
            }

            // Check vertical moves
            for (int col = 0; col < 4; col++)
            {
                for (int row = 0; row < 3; row++)
                {
                    if (game.Board[row, col] == game.Board[row + 1, col]) return true;
                }
            }

            return false;
        }

        private GameModel CloneGame(GameModel game)
        {
            var newGame = new GameModel();
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    newGame.Board[row, col] = game.Board[row, col];
                }
            }
            newGame.Score = game.Score;
            newGame.IsGameOver = game.IsGameOver;
            newGame.IsWon = game.IsWon;
            return newGame;
        }

        public string SerializeBoard(GameModel game)
        {
            var boardData = new
            {
                Board = game.Board,
                Score = game.Score,
                IsGameOver = game.IsGameOver,
                IsWon = game.IsWon
            };
            return JsonConvert.SerializeObject(boardData);
        }

        public GameModel DeserializeBoard(string boardState)
        {
            dynamic boardData = JsonConvert.DeserializeObject(boardState);
            var game = new GameModel();
            
            var board = boardData.Board;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    game.Board[row, col] = board[row][col];
                }
            }
            
            game.Score = boardData.Score;
            game.IsGameOver = boardData.IsGameOver;
            game.IsWon = boardData.IsWon;
            
            return game;
        }
    }
}
