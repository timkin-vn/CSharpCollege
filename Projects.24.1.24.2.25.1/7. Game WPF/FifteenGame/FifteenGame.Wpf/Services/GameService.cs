using System;
using System.Collections.Generic;
using System.Linq;
using FifteenGame.Business.Models;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        
        private const int BoardSize = GameModel.Size;
        private const int ChanceToSpawnFour = 10;
        private readonly Random _randomizer = new Random();

        public void ResetState(GameModel board)
        {
            for (int i = 0; i < BoardSize; i++)
                for (int j = 0; j < BoardSize; j++)
                    board[i, j] = 0;

            board.Score = 0;
            board.IsWin = false;

            AddRandomTile(board);
            AddRandomTile(board);
        }

        public bool MakeMove(GameModel board, MoveDirection direction)
        {
            if (board.IsWin) return false;

            
            int[,] oldBoard = (int[,])board.Cells.Clone();
            int oldScore = board.Score;

          
            switch (direction)
            {
                case MoveDirection.Left: ProcessRows(board, true); break;
                case MoveDirection.Right: ProcessRows(board, false); break;
                case MoveDirection.Up: ProcessColumns(board, true); break;
                case MoveDirection.Down: ProcessColumns(board, false); break;
                default: return false;
            }

           
            bool boardChanged = oldScore != board.Score;
            if (!boardChanged)
            {
                for (int r = 0; r < BoardSize; r++)
                {
                    for (int c = 0; c < BoardSize; c++)
                    {
                        if (oldBoard[r, c] != board[r, c])
                        {
                            boardChanged = true;
                            goto EndCheck;
                        }
                    }
                }
            EndCheck:;
            }

            if (!boardChanged) return false;

            AddRandomTile(board);
            return true;
        }

        private void ProcessRows(GameModel board, bool leftToRight)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                var currentRow = new int[BoardSize];
                for (int col = 0; col < BoardSize; col++)
                {
                    int targetCol = leftToRight ? col : (BoardSize - 1 - col);
                    currentRow[col] = board[row, targetCol];
                }

                var (newRow, earnedPoints) = MergeTiles(currentRow);
                board.Score += earnedPoints;

                for (int col = 0; col < BoardSize; col++)
                {
                    int targetCol = leftToRight ? col : (BoardSize - 1 - col);
                    board[row, targetCol] = newRow[col];
                }
            }
        }

        private void ProcessColumns(GameModel board, bool topToBottom)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                var currentCol = new int[BoardSize];
                for (int row = 0; row < BoardSize; row++)
                {
                    int targetRow = topToBottom ? row : (BoardSize - 1 - row);
                    currentCol[row] = board[targetRow, col];
                }

                var (newCol, earnedPoints) = MergeTiles(currentCol);
                board.Score += earnedPoints;

                for (int row = 0; row < BoardSize; row++)
                {
                    int targetRow = topToBottom ? row : (BoardSize - 1 - row);
                    board[targetRow, col] = newCol[row];
                }
            }
        }

        private (int[] result, int points) MergeTiles(int[] line)
        {
            var compressed = new List<int>();

            foreach (var tile in line)
            {
                if (tile != 0) compressed.Add(tile);
            }

            int totalPoints = 0;
            
            for (int i = 0; i < compressed.Count - 1; i++)
            {
                if (compressed[i] == compressed[i + 1])
                {
                    compressed[i] *= 2;
                    totalPoints += compressed[i];
                    compressed.RemoveAt(i + 1);
                }
            }

            
            var finalArray = compressed.ToArray();
            Array.Resize(ref finalArray, BoardSize);
            return (finalArray, totalPoints);
        }

        private void AddRandomTile(GameModel board)
        {
            var emptyCells = new List<(int r, int c)>();
            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    if (board[r, c] == 0) emptyCells.Add((r, c));
                }
            }

            if (emptyCells.Count == 0) return;

            var (row, col) = emptyCells[_randomizer.Next(emptyCells.Count)];
            int value = _randomizer.Next(100) < ChanceToSpawnFour ? 4 : 2;
            board[row, col] = value;
        }

        public bool CheckIfLost(GameModel board)
        {
           
            for (int r = 0; r < BoardSize; r++)
                for (int c = 0; c < BoardSize; c++)
                    if (board[r, c] == 0) return false;

            
            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize - 1; c++)
                {
                    if (board[r, c] == board[r, c + 1]) return false;
                }
            }
            for (int c = 0; c < BoardSize; c++)
            {
                for (int r = 0; r < BoardSize - 1; r++)
                {
                    if (board[r, c] == board[r + 1, c]) return false;
                }
            }

            return true;
        }

        public void Shuffle(GameModel board) => ResetState(board);
    }
}