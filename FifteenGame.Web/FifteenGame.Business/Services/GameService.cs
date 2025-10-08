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
            int value = 1;
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (value % 2 == 0)
                    {
                        model[row, column] = 0-value;
                    }
                    else
                    {
                        model[row, column] = 0+value;
                    }
                    value++;
                }
            }
            model.PlayerValue = 0;
            model.StepsLeft = 10;
            model[GameModel.RowCount - 1, GameModel.ColumnCount - 1] = model.PlayerValue;
            model.PlayerRow = GameModel.RowCount - 1;
            model.PlayerColumn = GameModel.ColumnCount - 1;
        }

        public bool IsGameOver(GameModel model)
        {
            if (model.StepsLeft > 0)
            {
                return false;
            }
            return true;
        }

        public bool MakeMove(GameModel model, MoveDirection direction, bool isShuffling)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    if (model.PlayerColumn == GameModel.ColumnCount - 1)
                    {
                        return false;
                    }
                    if (!isShuffling)
                    {
                        model.PlayerValue += model[model.PlayerRow, model.PlayerColumn + 1];
                        model[model.PlayerRow, model.PlayerColumn + 1] = 0;
                        model.StepsLeft--;
                    }
                    model[model.PlayerRow, model.PlayerColumn] = model[model.PlayerRow, model.PlayerColumn + 1];
                    model[model.PlayerRow, model.PlayerColumn + 1] = model.PlayerValue;
                    model.PlayerColumn++;
                    return true;

                case MoveDirection.Right:
                    if (model.PlayerColumn == 0)
                    {
                        return false;
                    }
                    if (!isShuffling)
                    {
                        model.PlayerValue += model[model.PlayerRow, model.PlayerColumn - 1];
                        model[model.PlayerRow, model.PlayerColumn - 1] = 0;
                        model.StepsLeft--;
                    }
                    model[model.PlayerRow, model.PlayerColumn] = model[model.PlayerRow, model.PlayerColumn - 1];
                    model[model.PlayerRow, model.PlayerColumn - 1] = model.PlayerValue;
                    model.PlayerColumn--;
                    return true;

                case MoveDirection.Up:
                    if (model.PlayerRow == GameModel.RowCount - 1)
                    {
                        return false;
                    }
                    if (!isShuffling)
                    {
                        model.PlayerValue += model[model.PlayerRow + 1, model.PlayerColumn];
                        model[model.PlayerRow + 1, model.PlayerColumn] = 0;
                        model.StepsLeft--;
                    }
                    model[model.PlayerRow, model.PlayerColumn] = model[model.PlayerRow + 1, model.PlayerColumn];
                    model[model.PlayerRow + 1, model.PlayerColumn] = model.PlayerValue;
                    model.PlayerRow++;
                    return true;

                case MoveDirection.Down:
                    if (model.PlayerRow == 0)
                    {
                        return false;
                    }
                    if (!isShuffling)
                    {
                        model.PlayerValue += model[model.PlayerRow - 1, model.PlayerColumn];
                        model[model.PlayerRow - 1, model.PlayerColumn] = 0;
                        model.StepsLeft--;
                    }
                    model[model.PlayerRow, model.PlayerColumn] = model[model.PlayerRow - 1, model.PlayerColumn];
                    model[model.PlayerRow - 1, model.PlayerColumn] = model.PlayerValue;
                    model.PlayerRow--;
                    return true;
            }
            return false;
        }

        public void Shuffle(GameModel model)
        {
            Initialize(model);

            var rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var nextMove = (MoveDirection)(rnd.Next(4) + 1);
                MakeMove(model, nextMove, true);
            }
        }
    }
}
