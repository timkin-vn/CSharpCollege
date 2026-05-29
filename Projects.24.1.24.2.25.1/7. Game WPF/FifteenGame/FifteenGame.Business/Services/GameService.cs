using System;
using FifteenGame.Business.Models;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private Random _random = new Random();

        public void Initialize(GameModel model)
        {
            for (int i = 0; i < GameModel.Size; i++)
                for (int j = 0; j < GameModel.Size; j++)
                    model[i, j] = 0;

            model.Score = 0;
            model.IsWin = false;

            AddRandomTile(model);
            AddRandomTile(model);
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            if (model.IsWin) return false;

            var oldCells = (int[,])model.Cells.Clone();
            int oldScore = model.Score;

            switch (direction)
            {
                case MoveDirection.Left: MoveLeft(model); break;
                case MoveDirection.Right: MoveRight(model); break;
                case MoveDirection.Up: MoveUp(model); break;
                case MoveDirection.Down: MoveDown(model); break;
                default: return false;
            }

            if (AreEqual(oldCells, model.Cells) && oldScore == model.Score)
                return false;

            AddRandomTile(model);
            return true;
        }

        private void MoveLeft(GameModel model)
        {
            for (int row = 0; row < GameModel.Size; row++)
            {
                var line = new int[GameModel.Size];
                for (int col = 0; col < GameModel.Size; col++)
                    line[col] = model[row, col];

                var (newLine, addedScore) = MergeLine(line);
                for (int col = 0; col < GameModel.Size; col++)
                    model[row, col] = newLine[col];

                model.Score += addedScore;
            }
        }

        private void MoveRight(GameModel model)
        {
            for (int row = 0; row < GameModel.Size; row++)
            {
                var line = new int[GameModel.Size];
                for (int col = 0; col < GameModel.Size; col++)
                    line[GameModel.Size - 1 - col] = model[row, col];

                var (newLine, addedScore) = MergeLine(line);
                for (int col = 0; col < GameModel.Size; col++)
                    model[row, GameModel.Size - 1 - col] = newLine[col];

                model.Score += addedScore;
            }
        }

        private void MoveUp(GameModel model)
        {
            for (int col = 0; col < GameModel.Size; col++)
            {
                var line = new int[GameModel.Size];
                for (int row = 0; row < GameModel.Size; row++)
                    line[row] = model[row, col];

                var (newLine, addedScore) = MergeLine(line);
                for (int row = 0; row < GameModel.Size; row++)
                    model[row, col] = newLine[row];

                model.Score += addedScore;
            }
        }

        private void MoveDown(GameModel model)
        {
            for (int col = 0; col < GameModel.Size; col++)
            {
                var line = new int[GameModel.Size];
                for (int row = 0; row < GameModel.Size; row++)
                    line[GameModel.Size - 1 - row] = model[row, col];

                var (newLine, addedScore) = MergeLine(line);
                for (int row = 0; row < GameModel.Size; row++)
                    model[row, col] = newLine[GameModel.Size - 1 - row];

                model.Score += addedScore;
            }
        }

        private (int[] newLine, int addedScore) MergeLine(int[] line)
        {
            var result = new int[GameModel.Size];
            int writeIndex = 0;
            int scoreGain = 0;

            for (int i = 0; i < GameModel.Size; i++)
            {
                if (line[i] == 0) continue;
                if (writeIndex > 0 && result[writeIndex - 1] == line[i])
                {
                    result[writeIndex - 1] *= 2;
                    scoreGain += result[writeIndex - 1];
                }
                else
                {
                    result[writeIndex] = line[i];
                    writeIndex++;
                }
            }
            return (result, scoreGain);
        }

        private void AddRandomTile(GameModel model)
        {
            var emptyCells = new System.Collections.Generic.List<(int, int)>();
            for (int i = 0; i < GameModel.Size; i++)
                for (int j = 0; j < GameModel.Size; j++)
                    if (model[i, j] == 0)
                        emptyCells.Add((i, j));

            if (emptyCells.Count == 0) return;

            var (row, col) = emptyCells[_random.Next(emptyCells.Count)];
            model[row, col] = _random.Next(10) == 0 ? 4 : 2;
        }

        private bool AreEqual(int[,] a, int[,] b)
        {
            for (int i = 0; i < GameModel.Size; i++)
                for (int j = 0; j < GameModel.Size; j++)
                    if (a[i, j] != b[i, j]) return false;
            return true;
        }

        public bool IsGameOver(GameModel model)
        {
            for (int i = 0; i < GameModel.Size; i++)
                for (int j = 0; j < GameModel.Size; j++)
                    if (model[i, j] == 0) return false;

            for (int i = 0; i < GameModel.Size; i++)
                for (int j = 0; j < GameModel.Size - 1; j++)
                    if (model[i, j] == model[i, j + 1]) return false;

            for (int j = 0; j < GameModel.Size; j++)
                for (int i = 0; i < GameModel.Size - 1; i++)
                    if (model[i, j] == model[i + 1, j]) return false;

            return true;
        }

        public void Shuffle(GameModel model) => Initialize(model);
    }
}