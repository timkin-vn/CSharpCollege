using System;
using System.Runtime.Remoting.Channels;
using _2048Game.Business.Models;

namespace _2048Game.Business.Services
{
    public class GameService
    {
        public GameBoard Board { get; set; }
        public int CurrentScore { get; private set; }
        public int BestScore { get; private set; }

        private Random _random = new Random();

        public event Action BoardUpdated;
        public event Action GameOver;

        public GameService()
        {
            Restart();
        }

        public void StartGame()
        {
            Restart();
            AddRandomTile();
            AddRandomTile();
            BoardUpdated?.Invoke();
        }

        public void Restart()
        {
            Board = new GameBoard();
            CurrentScore = 0;
        }

        private void AddRandomTile()
        {
            var emptyCells = new System.Collections.Generic.List<(int r, int c)>();

            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                    if (Board.Tiles[r, c] == 0)
                        emptyCells.Add((r, c));

            if (emptyCells.Count == 0)
                return;

            var (row, col) = emptyCells[_random.Next(emptyCells.Count)];
            Board.Tiles[row, col] = _random.NextDouble() < 0.9 ? 2 : 4;
        }

        public void Move(MoveDirection.Direction direction)
        {
            bool moved = false;

            switch (direction)
            {
                case MoveDirection.Direction.Left:
                    for (int r = 0; r < 4; r++)
                        moved |= SlideRowLeft(r);
                    break;

                case MoveDirection.Direction.Right:
                    for (int r = 0; r < 4; r++)
                        moved |= SlideRowRight(r);
                    break;

                case MoveDirection.Direction.Up:
                    for (int c = 0; c < 4; c++)
                        moved |= SlideColumnUp(c);
                    break;

                case MoveDirection.Direction.Down:
                    for (int c = 0; c < 4; c++)
                        moved |= SlideColumnDown(c);
                    break;
            }

            if (moved)
            {
                AddRandomTile();
                BoardUpdated?.Invoke();

                if (CheckGameOver())
                    GameOver?.Invoke();
            }
        }
        private bool SlideRowLeft(int row)
        {
            bool moved = false;

            for (int c = 1; c < 4; c++)
            {
                if (Board.Tiles[row, c] == 0) continue;

                int target = c;
                while (target > 0 && Board.Tiles[row, target - 1] == 0)
                    target--;

                if (target != c)
                {
                    Board.Tiles[row, target] = Board.Tiles[row, c];
                    Board.Tiles[row, c] = 0;
                    moved = true;
                }

                if (target > 0 && Board.Tiles[row, target - 1] == Board.Tiles[row, target])
                {
                    Board.Tiles[row, target - 1] *= 2;
                    CurrentScore += Board.Tiles[row, target - 1];
                    Board.Tiles[row, target] = 0;
                    moved = true;
                }
            }

            return moved;
        }

        private bool SlideRowRight(int row)
        {
            bool moved = false;

            for (int c = 2; c >= 0; c--)
            {
                if (Board.Tiles[row, c] == 0) continue;

                int target = c;
                while (target < 3 && Board.Tiles[row, target + 1] == 0)
                    target++;

                if (target != c)
                {
                    Board.Tiles[row, target] = Board.Tiles[row, c];
                    Board.Tiles[row, c] = 0;
                    moved = true;
                }

                if (target < 3 && Board.Tiles[row, target + 1] == Board.Tiles[row, target])
                {
                    Board.Tiles[row, target + 1] *= 2;
                    CurrentScore += Board.Tiles[row, target + 1];
                    Board.Tiles[row, target] = 0;
                    moved = true;
                }
            }

            return moved;
        }

        private bool SlideColumnUp(int col)
        {
            bool moved = false;

            for (int r = 1; r < 4; r++)
            {
                if (Board.Tiles[r, col] == 0) continue;

                int target = r;
                while (target > 0 && Board.Tiles[target - 1, col] == 0)
                    target--;

                if (target != r)
                {
                    Board.Tiles[target, col] = Board.Tiles[r, col];
                    Board.Tiles[r, col] = 0;
                    moved = true;
                }

                if (target > 0 && Board.Tiles[target - 1, col] == Board.Tiles[target, col])
                {
                    Board.Tiles[target - 1, col] *= 2;
                    CurrentScore += Board.Tiles[target - 1, col];
                    Board.Tiles[target, col] = 0;
                    moved = true;
                }
            }

            return moved;
        }

        private bool SlideColumnDown(int col)
        {
            bool moved = false;

            for (int r = 2; r >= 0; r--)
            {
                if (Board.Tiles[r, col] == 0) continue;

                int target = r;
                while (target < 3 && Board.Tiles[target + 1, col] == 0)
                    target++;

                if (target != r)
                {
                    Board.Tiles[target, col] = Board.Tiles[r, col];
                    Board.Tiles[r, col] = 0;
                    moved = true;
                }

                if (target < 3 && Board.Tiles[target + 1, col] == Board.Tiles[target, col])
                {
                    Board.Tiles[target + 1, col] *= 2;
                    CurrentScore += Board.Tiles[target + 1, col];
                    Board.Tiles[target, col] = 0;
                    moved = true;
                }
            }

            return moved;
        }

        private bool CheckGameOver()
        {
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 4; c++)
                    if (Board.Tiles[r, c] == 0)
                        return false;

            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 3; c++)
                    if (Board.Tiles[r, c] == Board.Tiles[r, c + 1])
                        return false;

            for (int c = 0; c < 4; c++)
                for (int r = 0; r < 3; r++)
                    if (Board.Tiles[r, c] == Board.Tiles[r + 1, c])
                        return false;

            return true;
        }
    }
}
