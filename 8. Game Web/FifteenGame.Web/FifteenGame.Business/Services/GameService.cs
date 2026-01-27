using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private readonly Random _rnd = new Random();

        public void Initialize(GameModel model)
        {
            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    model[r, c] = GameModel.EmptyCellValue;
                }
            }

            model.Score = 0;
            model.IsWin = false;
            model.IsLose = false;

            
            AddRandomTile(model);
            AddRandomTile(model);
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            if (direction == MoveDirection.None) return false;
            if (model.IsWin || model.IsLose) return false;

            bool moved = false;

            switch (direction)
            {
                case MoveDirection.Left:
                    moved = MoveLeft(model);
                    break;
                case MoveDirection.Right:
                    moved = MoveRight(model);
                    break;
                case MoveDirection.Up:
                    moved = MoveUp(model);
                    break;
                case MoveDirection.Down:
                    moved = MoveDown(model);
                    break;
            }

            if (moved)
            {
                AddRandomTile(model);
            }

            
            model.IsWin = HasTile(model, 2048);
            model.IsLose = !model.IsWin && !HasAnyMoves(model);

            return moved;
        }

        public bool IsGameOver(GameModel model)
        {
            return model.IsWin || model.IsLose;
        }

       

        private void AddRandomTile(GameModel model)
        {
            var empties = new List<Tuple<int, int>>();

            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    if (model[r, c] == GameModel.EmptyCellValue)
                    {
                        empties.Add(Tuple.Create(r, c));
                    }
                }
            }

            if (empties.Count == 0) return;

            var chosen = empties[_rnd.Next(empties.Count)];
            int value = (_rnd.NextDouble() < 0.9) ? 2 : 4;

            model[chosen.Item1, chosen.Item2] = value;
        }

        private bool HasTile(GameModel model, int value)
        {
            for (int r = 0; r < GameModel.RowCount; r++)
                for (int c = 0; c < GameModel.ColumnCount; c++)
                    if (model[r, c] == value)
                        return true;

            return false;
        }

        private bool HasAnyMoves(GameModel model)
        {
           
            for (int r = 0; r < GameModel.RowCount; r++)
                for (int c = 0; c < GameModel.ColumnCount; c++)
                    if (model[r, c] == GameModel.EmptyCellValue)
                        return true;

            
            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    int v = model[r, c];
                    if (c + 1 < GameModel.ColumnCount && model[r, c + 1] == v) return true;
                    if (r + 1 < GameModel.RowCount && model[r + 1, c] == v) return true;
                }
            }

            return false;
        }

       

        private bool MoveLeft(GameModel model)
        {
            bool changed = false;

            for (int r = 0; r < GameModel.RowCount; r++)
            {
                int[] line = new int[GameModel.ColumnCount];
                for (int c = 0; c < GameModel.ColumnCount; c++)
                    line[c] = model[r, c];

                var result = CompressAndMerge(line);
                int[] merged = result.Item1;
                int gained = result.Item2;
                bool lineChanged = result.Item3;

                for (int c = 0; c < GameModel.ColumnCount; c++)
                    model[r, c] = merged[c];

                if (lineChanged)
                {
                    model.Score += gained;
                    changed = true;
                }
            }

            return changed;
        }

        private bool MoveRight(GameModel model)
        {
            bool changed = false;

            for (int r = 0; r < GameModel.RowCount; r++)
            {
                int[] line = new int[GameModel.ColumnCount];
                for (int c = 0; c < GameModel.ColumnCount; c++)
                    line[c] = model[r, GameModel.ColumnCount - 1 - c];

                var result = CompressAndMerge(line);
                int[] merged = result.Item1;
                int gained = result.Item2;
                bool lineChanged = result.Item3;

                for (int c = 0; c < GameModel.ColumnCount; c++)
                    model[r, GameModel.ColumnCount - 1 - c] = merged[c];

                if (lineChanged)
                {
                    model.Score += gained;
                    changed = true;
                }
            }

            return changed;
        }

        private bool MoveUp(GameModel model)
        {
            bool changed = false;

            for (int c = 0; c < GameModel.ColumnCount; c++)
            {
                int[] line = new int[GameModel.RowCount];
                for (int r = 0; r < GameModel.RowCount; r++)
                    line[r] = model[r, c];

                var result = CompressAndMerge(line);
                int[] merged = result.Item1;
                int gained = result.Item2;
                bool lineChanged = result.Item3;

                for (int r = 0; r < GameModel.RowCount; r++)
                    model[r, c] = merged[r];

                if (lineChanged)
                {
                    model.Score += gained;
                    changed = true;
                }
            }

            return changed;
        }

        private bool MoveDown(GameModel model)
        {
            bool changed = false;

            for (int c = 0; c < GameModel.ColumnCount; c++)
            {
                int[] line = new int[GameModel.RowCount];
                for (int r = 0; r < GameModel.RowCount; r++)
                    line[r] = model[GameModel.RowCount - 1 - r, c];

                var result = CompressAndMerge(line);
                int[] merged = result.Item1;
                int gained = result.Item2;
                bool lineChanged = result.Item3;

                for (int r = 0; r < GameModel.RowCount; r++)
                    model[GameModel.RowCount - 1 - r, c] = merged[r];

                if (lineChanged)
                {
                    model.Score += gained;
                    changed = true;
                }
            }

            return changed;
        }

     
        private Tuple<int[], int, bool> CompressAndMerge(int[] line)
        {
            List<int> nonZero = line.Where(x => x != 0).ToList();
            List<int> result = new List<int>(line.Length);

            int gained = 0;
            bool changed = false;

            for (int i = 0; i < nonZero.Count; i++)
            {
                if (i + 1 < nonZero.Count && nonZero[i] == nonZero[i + 1])
                {
                    int v = nonZero[i] * 2;
                    result.Add(v);
                    gained += v;
                    i++; 
                    changed = true;
                }
                else
                {
                    result.Add(nonZero[i]);
                }
            }

            while (result.Count < line.Length)
                result.Add(0);

            
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != result[i])
                {
                    changed = true;
                    break;
                }
            }

            return Tuple.Create(result.ToArray(), gained, changed);
        }
    }
}
