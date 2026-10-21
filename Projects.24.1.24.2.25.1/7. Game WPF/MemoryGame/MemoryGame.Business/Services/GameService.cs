using MemoryGame.Business.Models;
using System;
using System.Collections.Generic;

namespace MemoryGame.Business.Services
{
    public class GameService
    {
        private readonly Random _random = new Random();

        public void Initialize(GameModel model)
        {
            var values = new List<int>();
            for (int value = 1; value <= GameModel.PairCount; value++)
            {
                values.Add(value);
                values.Add(value);
            }

            int index = 0;
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = values[index++];
                    model.SetState(row, column, CardState.Hidden);
                }
            }

            model.MoveCount = 0;
            model.MatchedPairCount = 0;
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);

            // Алгоритм Фишера-Йетса
            int cellCount = GameModel.RowCount * GameModel.ColumnCount;
            for (int i = cellCount - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);

                int rowI = i / GameModel.ColumnCount, columnI = i % GameModel.ColumnCount;
                int rowJ = j / GameModel.ColumnCount, columnJ = j % GameModel.ColumnCount;

                int temp = model[rowI, columnI];
                model[rowI, columnI] = model[rowJ, columnJ];
                model[rowJ, columnJ] = temp;
            }
        }

        /// <summary>
        /// Ход игрока - щелчок по карточке.
        /// Если открыты две несовпавшие карточки, они сначала закрываются.
        /// Затем, если выбранная карточка закрыта, она открывается.
        /// Если открыта вторая карточка, засчитывается попытка, а совпавшая пара остается открытой навсегда.
        /// </summary>
        public bool MakeMove(GameModel model, int row, int column)
        {
            if (row < 0 || row >= GameModel.RowCount || column < 0 || column >= GameModel.ColumnCount)
            {
                return false;
            }

            bool changed = false;
            var opened = GetOpenedCells(model);

            if (opened.Count == 2)
            {
                foreach (var (openedRow, openedColumn) in opened)
                {
                    model.SetState(openedRow, openedColumn, CardState.Hidden);
                }

                opened.Clear();
                changed = true;
            }

            if (model.GetState(row, column) != CardState.Hidden)
            {
                return changed;
            }

            model.SetState(row, column, CardState.Opened);
            opened.Add((row, column));

            if (opened.Count == 2)
            {
                model.MoveCount++;

                var (firstRow, firstColumn) = opened[0];
                if (model[firstRow, firstColumn] == model[row, column])
                {
                    model.SetState(firstRow, firstColumn, CardState.Matched);
                    model.SetState(row, column, CardState.Matched);
                    model.MatchedPairCount++;
                }
            }

            return true;
        }

        public bool CanClick(GameModel model, int row, int column)
        {
            var state = model.GetState(row, column);
            return state == CardState.Hidden || (state == CardState.Opened && GetOpenedCells(model).Count == 2);
        }

        public bool IsGameOver(GameModel model)
        {
            return model.MatchedPairCount == GameModel.PairCount;
        }

        private List<(int Row, int Column)> GetOpenedCells(GameModel model)
        {
            var result = new List<(int Row, int Column)>();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model.GetState(row, column) == CardState.Opened)
                    {
                        result.Add((row, column));
                    }
                }
            }

            return result;
        }
    }
}
