using LightsOut.Business.Models;
using System;

namespace LightsOut.Business.Service
{
    public class GameService
    {
        public void Initialize(GameModel model)
        {
            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    model[r, c] = false;
                }
            }
        }

        public void MakeMoveAt(GameModel model, int row, int column)
        {
            Toggle(model, row, column);
            Toggle(model, row - 1, column);
            Toggle(model, row + 1, column);
            Toggle(model, row, column - 1);
            Toggle(model, row, column + 1);
        }

        private void Toggle(GameModel model, int row, int column)
        {
            if (row >= 0 && row < GameModel.RowCount && column >= 0 && column < GameModel.ColumnCount)
            {
                model[row, column] = !model[row, column];
            }
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);
            var rnd = new Random();

            int moves = 50;
            for (int i = 0; i < moves; i++)
            {
                int r = rnd.Next(GameModel.RowCount);
                int c = rnd.Next(GameModel.ColumnCount);
                MakeMoveAt(model, r, c);
            }
        }

        public bool IsGameOver(GameModel model)
        {
            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    if (model[r, c])
                        return false;
                }
            }
            return true;
        }
    }
}
