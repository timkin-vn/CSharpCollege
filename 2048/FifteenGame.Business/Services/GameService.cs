using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private Random _rnd = new Random();
        public int Score { get; private set; } = 0;

        public void Initialize(GameModel model)
        {
            Score = 0;

            for (int r = 0; r < GameModel.RowCount; r++)
                for (int c = 0; c < GameModel.ColumnCount; c++)
                    model[r, c] = 0;

            AddRandomTile(model);
            AddRandomTile(model);
        }

        public void AddRandomTile(GameModel model)
        {
            List<(int r, int c)> empty = new List<(int, int)>();
            for (int r = 0; r < GameModel.RowCount; r++)
                for (int c = 0; c < GameModel.ColumnCount; c++)
                    if (model[r, c] == 0)
                        empty.Add((r, c));

            if (empty.Count > 0)
            {
                var (r, c) = empty[_rnd.Next(empty.Count)];
                model[r, c] = _rnd.Next(10) == 0 ? 4 : 2;
            }
        }

        public bool MakeMove(GameModel model, MoveDirection dir)
        {
            bool moved = false;

            switch (dir)
            {
                case MoveDirection.Left: moved = MoveLeft(model); break;
                case MoveDirection.Right: moved = MoveRight(model); break;
                case MoveDirection.Up: moved = MoveUp(model); break;
                case MoveDirection.Down: moved = MoveDown(model); break;
            }

            if (moved) AddRandomTile(model);

            return moved;
        }

        private bool MoveLeft(GameModel model)
        {
            bool moved = false;

            for (int r = 0; r < GameModel.RowCount; r++)
            {
                int[] line = new int[GameModel.ColumnCount];
                int idx = 0;
                for (int c = 0; c < GameModel.ColumnCount; c++)
                    if (model[r, c] != 0)
                        line[idx++] = model[r, c];

                for (int i = 0; i < GameModel.ColumnCount - 1; i++)
                {
                    if (line[i] != 0 && line[i] == line[i + 1])
                    {
                        line[i] *= 2;
                        Score += line[i]; // ← добавляем очки при слиянии
                        line[i + 1] = 0;
                        moved = true;
                        i++;
                    }
                }

                int[] newLine = line.Where(x => x != 0).ToArray();
                Array.Resize(ref newLine, GameModel.ColumnCount);

                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    if (model[r, c] != newLine[c])
                    {
                        model[r, c] = newLine[c];
                        moved = true;
                    }
                }
            }

            return moved;
        }

        private bool MoveRight(GameModel model)
        {
            Rotate(model, 180);
            bool moved = MoveLeft(model);
            Rotate(model, 180);
            return moved;
        }

        private bool MoveUp(GameModel model)
        {
            Rotate(model, 270);
            bool moved = MoveLeft(model);
            Rotate(model, 90);
            return moved;
        }

        private bool MoveDown(GameModel model)
        {
            Rotate(model, 90);
            bool moved = MoveLeft(model);
            Rotate(model, 270);
            return moved;
        }

        private void Rotate(GameModel model, int degree)
        {
            int n = GameModel.RowCount;
            int[,] newCells = new int[n, n];

            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                {
                    switch (degree)
                    {
                        case 90: newCells[c, n - 1 - r] = model[r, c]; break;
                        case 180: newCells[n - 1 - r, n - 1 - c] = model[r, c]; break;
                        case 270: newCells[n - 1 - c, r] = model[r, c]; break;
                    }
                }

            if (degree != 0)
                for (int r = 0; r < n; r++)
                    for (int c = 0; c < n; c++)
                        model[r, c] = newCells[r, c];
        }
    }
}
