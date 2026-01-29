using MemoryGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGame.Business.Services
{
    public class GameService
    {
        private readonly Random _rnd = new Random();

        public void Initialize(MemoryGameModel model)
        {
            model.IsGameOver = false;
            model.IsWin = false;
            model.NeedToHideMismatchedPair = false;
            model.ResetPicks();

            // 8 пар (1..8) -> 16 значений
            List<int> values = Enumerable.Range(1, 8)
                .SelectMany(x => new[] { x, x })
                .ToList();

            Shuffle(values);

            int k = 0;
            for (int r = 0; r < MemoryGameModel.RowCount; r++)
                for (int c = 0; c < MemoryGameModel.ColumnCount; c++)
                {
                    model[r, c] = values[k++];
                    model.SetRevealed(r, c, false);
                    model.SetMatched(r, c, false);
                }
        }

        public void PickCell(MemoryGameModel model, int row, int column)
        {
            if (model.IsGameOver) return;
            if (!InBounds(row, column)) return;

            // пока не закроем неверную пару — новые открытия нельзя
            if (model.NeedToHideMismatchedPair) return;

            if (model.IsMatched(row, column)) return;
            if (model.IsRevealed(row, column)) return;

            // 1-й выбор
            if (model.FirstPickRow == -1)
            {
                model.FirstPickRow = row;
                model.FirstPickColumn = column;
                model.SetRevealed(row, column, true);
                return;
            }

            // 2-й выбор
            if (model.SecondPickRow == -1)
            {
                model.SecondPickRow = row;
                model.SecondPickColumn = column;
                model.SetRevealed(row, column, true);

                ResolveSecondPick(model);
            }
        }

        // следующий шаг пользователя после неверной пары — закрыть её
        public void CommitTurn(MemoryGameModel model)
        {
            if (model.IsGameOver) return;
            if (!model.NeedToHideMismatchedPair) return;

            HideMismatchedPair(model);
            model.NeedToHideMismatchedPair = false;
        }

        private void ResolveSecondPick(MemoryGameModel model)
        {
            int r1 = model.FirstPickRow;
            int c1 = model.FirstPickColumn;
            int r2 = model.SecondPickRow;
            int c2 = model.SecondPickColumn;

            if (model[r1, c1] == model[r2, c2])
            {
                model.SetMatched(r1, c1, true);
                model.SetMatched(r2, c2, true);

                model.ResetPicks();

                if (CheckWin(model))
                {
                    model.IsGameOver = true;
                    model.IsWin = true;
                }
            }
            else
            {
                // оставить открытыми до следующего шага (CommitTurn)
                model.NeedToHideMismatchedPair = true;
            }
        }

        private void HideMismatchedPair(MemoryGameModel model)
        {
            int r1 = model.FirstPickRow;
            int c1 = model.FirstPickColumn;
            int r2 = model.SecondPickRow;
            int c2 = model.SecondPickColumn;

            if (r1 != -1 && c1 != -1 && !model.IsMatched(r1, c1))
                model.SetRevealed(r1, c1, false);

            if (r2 != -1 && c2 != -1 && !model.IsMatched(r2, c2))
                model.SetRevealed(r2, c2, false);

            model.ResetPicks();
        }

        private bool CheckWin(MemoryGameModel model)
        {
            for (int r = 0; r < MemoryGameModel.RowCount; r++)
                for (int c = 0; c < MemoryGameModel.ColumnCount; c++)
                    if (!model.IsMatched(r, c))
                        return false;

            return true;
        }

        private bool InBounds(int r, int c)
        {
            return r >= 0 && r < MemoryGameModel.RowCount &&
                   c >= 0 && c < MemoryGameModel.ColumnCount;
        }

        private void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _rnd.Next(i + 1);
                T tmp = list[i];
                list[i] = list[j];
                list[j] = tmp;
            }
        }
    }
}
