using System.Collections.Generic;

namespace FifteenGame.Business.Models
{
    public class SnakeGameModel
    {
        public const int Width = 20;
        public const int Height = 15;

        public List<SnakePoint> Snake { get; set; } = new List<SnakePoint>();
        public SnakePoint Food { get; set; }
        public Direction CurrentDirection { get; set; } = Direction.Right;
        public int Score { get; set; }
        public bool IsGameOver { get; set; }

        public SnakeGameModel()
        {

            Snake.Add(new SnakePoint(5, 5));
            Snake.Add(new SnakePoint(4, 5));
            Snake.Add(new SnakePoint(3, 5));
        }
    }

    public struct SnakePoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public SnakePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is SnakePoint other)
            {
                return X == other.X && Y == other.Y;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }

    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}