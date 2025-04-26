using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private readonly GameModel _model;
        private readonly Random _random = new Random();

        public GameService(GameModel model)
        {
            _model = model;
        }

        public void StartNewGame()
        {
            _model.Reset();
            AddRandomTile();
            AddRandomTile();
        }

        public bool Move(MoveDirection direction)
        {
            bool moved = false;

            for (int i = 0; i < GameModel.Size; i++)
            {
                int[] line = GetLine(i, direction);
                int[] merged = MergeLine(line);
                moved |= !line.SequenceEqual(merged);
                SetLine(i, direction, merged);
            }

            if (moved)
                AddRandomTile();

            return moved;
        }

        private int[] GetLine(int index, MoveDirection direction)
        {
            int[] line = new int[GameModel.Size];

            for (int i = 0; i < GameModel.Size; i++)
            {
                switch (direction)
                {
                    case MoveDirection.Left:
                        line[i] = _model.GetCell(index, i);
                        break;
                    case MoveDirection.Right:
                        line[i] = _model.GetCell(index, GameModel.Size - 1 - i);
                        break;
                    case MoveDirection.Up:
                        line[i] = _model.GetCell(i, index);
                        break;
                    case MoveDirection.Down:
                        line[i] = _model.GetCell(GameModel.Size - 1 - i, index);
                        break;
                }
            }

            return line;
        }

        private void SetLine(int index, MoveDirection direction, int[] line)
        {
            for (int i = 0; i < GameModel.Size; i++)
            {
                switch (direction)
                {
                    case MoveDirection.Left:
                        _model.SetCell(index, i, line[i]);
                        break;
                    case MoveDirection.Right:
                        _model.SetCell(index, GameModel.Size - 1 - i, line[i]);
                        break;
                    case MoveDirection.Up:
                        _model.SetCell(i, index, line[i]);
                        break;
                    case MoveDirection.Down:
                        _model.SetCell(GameModel.Size - 1 - i, index, line[i]);
                        break;
                }
            }
        }

        private int[] MergeLine(int[] line)
        {
            List<int> result = new List<int>();
            Queue<int> queue = new Queue<int>(line.Where(v => v != 0));

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                if (queue.Count > 0 && queue.Peek() == current)
                {
                    queue.Dequeue();
                    int merged = current * 2;
                    result.Add(merged);
                    _model.AddScore(merged);
                }
                else
                {
                    result.Add(current);
                }
            }

            while (result.Count < GameModel.Size)
                result.Add(0);

            return result.ToArray();
        }

        private void AddRandomTile()
        {
            var empty = new List<(int Row, int Col)>();

            for (int i = 0; i < GameModel.Size; i++)
                for (int j = 0; j < GameModel.Size; j++)
                    if (_model.GetCell(i, j) == 0)
                        empty.Add((i, j));

            if (empty.Count > 0)
            {
                var (row, col) = empty[_random.Next(empty.Count)];
                _model.SetCell(row, col, _random.NextDouble() < 0.9 ? 2 : 4);
            }
        }

        public bool Has2048()
        {
            for (int i = 0; i < GameModel.Size; i++)
                for (int j = 0; j < GameModel.Size; j++)
                    if (_model.GetCell(i, j) == 2048)
                        return true;
            return false;
        }

        public bool HasAvailableMoves()
        {
            for (int i = 0; i < GameModel.Size; i++)
            {
                for (int j = 0; j < GameModel.Size; j++)
                {
                    int val = _model.GetCell(i, j);
                    if (val == 0)
                        return true;

                    if (i < GameModel.Size - 1 && val == _model.GetCell(i + 1, j))
                        return true;

                    if (j < GameModel.Size - 1 && val == _model.GetCell(i, j + 1))
                        return true;
                }
            }
            return false;
        }
    }
}
