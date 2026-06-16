using System;
using System.Collections.Generic;
using FifteenGame.Business.Models;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        private readonly Random _rng = new Random();

        public void ResetState(GameModel targetModel)
        {
            for (int r = 0; r < GameModel.Size; r++)
            {
                for (int c = 0; c < GameModel.Size; c++)
                {
                    targetModel[r, c] = 0;
                }
            }

            targetModel.Score = 0;
            targetModel.IsWin = false;

            SpawnNewNumber(targetModel);
            SpawnNewNumber(targetModel);
        }

        public bool MakeMove(GameModel targetModel, MoveDirection dir)
        {
            if (targetModel.IsWin) return false;

            int[,] previousState = (int[,])targetModel.Cells.Clone();
            int previousScore = targetModel.Score;

            switch (dir)
            {
                case MoveDirection.Left: ShiftLeft(targetModel); break;
                case MoveDirection.Right: ShiftRight(targetModel); break;
                case MoveDirection.Up: ShiftUp(targetModel); break;
                case MoveDirection.Down: ShiftDown(targetModel); break;
                default: return false;
            }

            bool hasChanges = previousScore != targetModel.Score;
            if (!hasChanges)
            {
                for (int r = 0; r < GameModel.Size; r++)
                {
                    for (int c = 0; c < GameModel.Size; c++)
                    {
                        if (previousState[r, c] != targetModel[r, c])
                        {
                            hasChanges = true;
                            break;
                        }
                    }
                    if (hasChanges) break;
                }
            }

            if (!hasChanges) return false;

            SpawnNewNumber(targetModel);
            return true;
        }

        private void ShiftLeft(GameModel targetModel)
        {
            for (int r = 0; r < GameModel.Size; r++)
            {
                var buffer = new int[GameModel.Size];
                for (int c = 0; c < GameModel.Size; c++)
                    buffer[c] = targetModel[r, c];

                var (processed, points) = CompressAndCombine(buffer);
                for (int c = 0; c < GameModel.Size; c++)
                    targetModel[r, c] = processed[c];

                targetModel.Score += points;
            }
        }

        private void ShiftRight(GameModel targetModel)
        {
            int bound = GameModel.Size - 1;
            for (int r = 0; r < GameModel.Size; r++)
            {
                var buffer = new int[GameModel.Size];
                for (int c = 0; c < GameModel.Size; c++)
                    buffer[bound - c] = targetModel[r, c];

                var (processed, points) = CompressAndCombine(buffer);
                for (int c = 0; c < GameModel.Size; c++)
                    targetModel[r, bound - c] = processed[c];

                targetModel.Score += points;
            }
        }

        private void ShiftUp(GameModel targetModel)
        {
            for (int c = 0; c < GameModel.Size; c++)
            {
                var buffer = new int[GameModel.Size];
                for (int r = 0; r < GameModel.Size; r++)
                    buffer[r] = targetModel[r, c];

                var (processed, points) = CompressAndCombine(buffer);
                for (int r = 0; r < GameModel.Size; r++)
                    targetModel[r, c] = processed[r];

                targetModel.Score += points;
            }
        }

        private void ShiftDown(GameModel targetModel)
        {
            int bound = GameModel.Size - 1;
            for (int c = 0; c < GameModel.Size; c++)
            {
                var buffer = new int[GameModel.Size];
                for (int r = 0; r < GameModel.Size; r++)
                    buffer[bound - r] = targetModel[r, c];

                var (processed, points) = CompressAndCombine(buffer);
                for (int r = 0; r < GameModel.Size; r++)
                    targetModel[r, c] = processed[bound - r];

                targetModel.Score += points;
            }
        }

        private (int[] output, int reward) CompressAndCombine(int[] source)
        {
            var cleanArray = new int[GameModel.Size];
            int pointer = 0;
            int currentReward = 0;

            for (int idx = 0; idx < GameModel.Size; idx++)
            {
                if (source[idx] == 0) continue;

                if (pointer > 0 && cleanArray[pointer - 1] == source[idx])
                {
                    cleanArray[pointer - 1] *= 2;
                    currentReward += cleanArray[pointer - 1];
                }
                else
                {
                    cleanArray[pointer] = source[idx];
                    pointer++;
                }
            }
            return (cleanArray, currentReward);
        }

        private void SpawnNewNumber(GameModel targetModel)
        {
            var freeSlots = new List<(int Row, int Col)>();
            for (int r = 0; r < GameModel.Size; r++)
            {
                for (int c = 0; c < GameModel.Size; c++)
                {
                    if (targetModel[r, c] == 0)
                        freeSlots.Add((r, c));
                }
            }

            if (freeSlots.Count == 0) return;

            var (selectedRow, selectedCol) = freeSlots[_rng.Next(freeSlots.Count)];
            targetModel[selectedRow, selectedCol] = _rng.Next(10) == 0 ? 4 : 2;
        }

        public bool CheckIfLost(GameModel targetModel)
        {
            for (int r = 0; r < GameModel.Size; r++)
                for (int c = 0; c < GameModel.Size; c++)
                    if (targetModel[r, c] == 0) return false;

            for (int r = 0; r < GameModel.Size; r++)
                for (int c = 0; c < GameModel.Size - 1; c++)
                    if (targetModel[r, c] == targetModel[r, c + 1]) return false;

            for (int c = 0; c < GameModel.Size; c++)
                for (int r = 0; r < GameModel.Size - 1; r++)
                    if (targetModel[r, c] == targetModel[r + 1, c]) return false;

            return true;
        }

        public void Shuffle(GameModel targetModel) => ResetState(targetModel);
    }
}