using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Wpf.Models
{
    public class GameModel
    {
        public int[,] Board { get; private set; } = new int[4, 4];
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
            Board = new int[4, 4];
            Score = 0;
            IsGameOver = false;
            IsWin = false;
            AddRandomTile();
            AddRandomTile();
        }

        public void AddRandomTile()
        {
            int emptyCount = 0;
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                    if (Board[r, c] == 0) emptyCount++;

            if (emptyCount == 0) return;

            int targetIndex = _random.Next(emptyCount);
            int currentIndex = 0;

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
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
            int[,] rotated = new int[4, 4];
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                    rotated[c, 3 - r] = Board[r, c];
            Board = rotated;
        }

        private bool SlideLeftRow(int r)
        {
            bool moved = false;
            for (int c = 0; c < 3; c++)
            {
                if (Board[r, c] == 0)
                {
                    for (int next = c + 1; next < 4; next++)
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

            for (int c = 0; c < 3; c++)
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
                for (int c = 0; c < 3; c++)
                {
                    if (Board[r, c] == 0)
                    {
                        for (int next = c + 1; next < 4; next++)
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
            for (int r = 0; r < 4; r++)
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
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                    if (Board[r, c] == 2048) { IsGameOver = true; IsWin = true; return; }

            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                    if (Board[r, c] == 0) return;

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if (r < 3 && Board[r, c] == Board[r + 1, c]) return;
                    if (c < 3 && Board[r, c] == Board[r, c + 1]) return;
                }
            }
            IsGameOver = true;
            IsWin = false;
        }
    }
}
