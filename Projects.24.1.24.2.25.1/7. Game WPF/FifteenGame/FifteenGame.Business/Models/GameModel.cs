using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Wpf.Models
{
    public class GameModel
    {
        public const int BoardSize = 4;
        public const int TargetScore = 2048;

        public int[,] Board { get; private set; } = new int[BoardSize, BoardSize];
        public int Score { get; private set; }
        public bool IsGameOver { get; private set; }
        public bool IsWin { get; private set; }

        private readonly Random _random = new Random();

        public GameModel()
        {
            Reset();
        }

        public void Reset()
        {
            Board = new int[BoardSize, BoardSize];
            Score = 0;
            IsGameOver = false;
            IsWin = false;
            AddRandomTile();
            AddRandomTile();
        }

        public void AddRandomTile()
        {
            int emptyCount = 0;
            for (int r = 0; r < BoardSize; r++)
                for (int c = 0; c < BoardSize; c++)
                    if (Board[r, c] == 0) emptyCount++;

            if (emptyCount == 0) return;

            int targetIndex = _random.Next(emptyCount);
            int currentIndex = 0;

            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    if (Board[r, c] == 0)
                    {
                        if (currentIndex == targetIndex)
                        {
                            Board[r, c] = _random.Next(10) == 0 ? 4 : 2;
                            return;
                        }
                        currentIndex++;
                    }
                }
            }
        }

        private void RotateClockwise()
        {
            int[,] rotated = new int[BoardSize, BoardSize];
            for (int r = 0; r < BoardSize; r++)
                for (int c = 0; c < BoardSize; c++)
                    rotated[c, (BoardSize - 1) - r] = Board[r, c];
            Board = rotated;
        }

        private bool SlideLeftRow(int r)
        {
            bool moved = false;
            for (int c = 0; c < BoardSize - 1; c++)
            {
                if (Board[r, c] == 0)
                {
                    for (int next = c + 1; next < BoardSize; next++)
                    {
                        if (Board[r, next] != 0)
                        {
                            Board[r, c] = Board[r, next];
                            Board[r, next] = 0;
                            moved = true;
                            break;
                        }
                    }
                }
            }

            for (int c = 0; c < BoardSize - 1; c++)
            {
                if (Board[r, c] != 0 && Board[r, c] == Board[r, c + 1])
                {
                    Board[r, c] *= 2;
                    Score += Board[r, c];
                    Board[r, c + 1] = 0;
                    moved = true;
                }
            }

            if (moved)
            {
                for (int c = 0; c < BoardSize - 1; c++)
                {
                    if (Board[r, c] == 0)
                    {
                        for (int next = c + 1; next < BoardSize; next++)
                        {
                            if (Board[r, next] != 0)
                            {
                                Board[r, c] = Board[r, next];
                                Board[r, next] = 0;
                                break;
                            }
                        }
                    }
                }
            }
            return moved;
        }

        public bool MakeMove(MoveDirection direction)
        {
            if (IsGameOver) return false;

            int rotations;
            switch (direction)
            {
                case MoveDirection.Left:
                    rotations = 0;
                    break;
                case MoveDirection.Down:
                    rotations = 1;
                    break;
                case MoveDirection.Right:
                    rotations = 2;
                    break;
                case MoveDirection.Up:
                    rotations = 3;
                    break;
                default:
                    rotations = -1;
                    break;
            }

            if (rotations == -1) return false;

            for (int i = 0; i < rotations; i++) RotateClockwise();

            bool anyMoved = false;
            for (int r = 0; r < BoardSize; r++)
            {
                if (SlideLeftRow(r)) anyMoved = true;
            }

            int returnRotations = (4 - rotations) % 4;
            for (int i = 0; i < returnRotations; i++) RotateClockwise();

            if (anyMoved)
            {
                AddRandomTile();
                CheckGameStatus();
            }

            return anyMoved;
        }

        private void CheckGameStatus()
        {
            for (int r = 0; r < BoardSize; r++)
                for (int c = 0; c < BoardSize; c++)
                    if (Board[r, c] == TargetScore) { IsGameOver = true; IsWin = true; return; }

            for (int r = 0; r < BoardSize; r++)
                for (int c = 0; c < BoardSize; c++)
                    if (Board[r, c] == 0) return;

            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    if (r < BoardSize - 1 && Board[r, c] == Board[r + 1, c]) return;
                    if (c < BoardSize - 1 && Board[r, c] == Board[r, c + 1]) return;
                }
            }
            IsGameOver = true;
            IsWin = false;
        }
    }
}
