using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class SnakeService
    {
        private Random _random = new Random();

        public void GenerateRandomFood(SnakeGameModel model)
        {
            var freeCells = new List<SnakePoint>();

            for (int x = 0; x < SnakeGameModel.Width; x++)
            {
                for (int y = 0; y < SnakeGameModel.Height; y++)
                {
                    var point = new SnakePoint(x, y);
                    if (!model.Snake.Contains(point))
                    {
                        freeCells.Add(point);
                    }
                }
            }

            if (freeCells.Count > 0)
            {
                var randomIndex = _random.Next(freeCells.Count);
                model.Food = freeCells[randomIndex];
            }
        }

        public bool Move(SnakeGameModel model, Direction? newDirection = null)
        {
            if (model.IsGameOver) return false;

            if (newDirection.HasValue)
            {
                if (!IsOppositeDirection(model.CurrentDirection, newDirection.Value))
                {
                    model.CurrentDirection = newDirection.Value;
                }
            }

            var head = model.Snake[0];
            var newHead = head;

            switch (model.CurrentDirection)
            {
                case Direction.Up: newHead = new SnakePoint(head.X, head.Y - 1); break;
                case Direction.Down: newHead = new SnakePoint(head.X, head.Y + 1); break;
                case Direction.Left: newHead = new SnakePoint(head.X - 1, head.Y); break;
                case Direction.Right: newHead = new SnakePoint(head.X + 1, head.Y); break;
            }

            // Проверка на съедение еды
            bool ateFood = newHead.Equals(model.Food);

            model.Snake.Insert(0, newHead);

            if (!ateFood)
            {
                model.Snake.RemoveAt(model.Snake.Count - 1);
            }
            else
            {
                model.Score++;
                GenerateRandomFood(model);
            }

            // Проверка на столкновение
            if (IsCollision(model))
            {
                model.IsGameOver = true;
                return false;
            }

            return true;
        }

        private bool IsOppositeDirection(Direction current, Direction newDir)
        {
            return (current == Direction.Up && newDir == Direction.Down) ||
                   (current == Direction.Down && newDir == Direction.Up) ||
                   (current == Direction.Left && newDir == Direction.Right) ||
                   (current == Direction.Right && newDir == Direction.Left);
        }

        private bool IsCollision(SnakeGameModel model)
        {
            var head = model.Snake[0];

            // Столкновение со стеной
            if (head.X < 0 || head.X >= SnakeGameModel.Width ||
                head.Y < 0 || head.Y >= SnakeGameModel.Height)
            {
                return true;
            }

            // Столкновение с собой
            for (int i = 1; i < model.Snake.Count; i++)
            {
                if (model.Snake[i].Equals(head))
                    return true;
            }

            return false;
        }
    }
}