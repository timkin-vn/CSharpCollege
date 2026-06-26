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
        public void Initialize(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = GameModel.LightOffValue;
                }
            }
        }

        public bool MakeMove(GameModel model, int row, int column)
        {
            if (row < 0 || row >= GameModel.RowCount || column < 0 || column >= GameModel.ColumnCount)
            {
                return false;
            }

            Toggle(model, row, column);
            Toggle(model, row - 1, column);
            Toggle(model, row + 1, column);
            Toggle(model, row, column - 1);
            Toggle(model, row, column + 1);
            return true;
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);

            var rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                MakeMove(model, rnd.Next(GameModel.RowCount), rnd.Next(GameModel.ColumnCount));
            }
        }

        public bool IsGameOver(GameModel model)
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] == GameModel.LightOnValue)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void Toggle(GameModel model, int row, int column)
        {
            if (row < 0 || row >= GameModel.RowCount || column < 0 || column >= GameModel.ColumnCount)
            {
                return;
            }

            model[row, column] = model[row, column] == GameModel.LightOnValue
                ? GameModel.LightOffValue
                : GameModel.LightOnValue;
        }
    }
}
