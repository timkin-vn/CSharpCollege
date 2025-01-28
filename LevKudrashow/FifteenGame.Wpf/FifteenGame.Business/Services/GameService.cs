using FifteenGame.Business.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        int[,] list = new int[5, 5] {
            {0,1,0,0,0},
            {0,0,0,1,0},
            {0,1,1,1,1},
            {0,1,0,0,0},
            {0,0,0,1,0}};

        public void Initialize(GameModel model)
        {
            int value = 1;
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (list[row, column] == 0)
                    { 
                        model[row, column] = value++;
                    }
                    else
                    {
                        model[row, column] = -value++;
                    }
                }
            }
            model.PlayerValue = 0;
            model[GameModel.RowCount - 1, GameModel.ColumnCount - 1] = model.PlayerValue;
            model.PlayerRow = GameModel.RowCount - 1;
            model.PlayerColumn = GameModel.ColumnCount - 1;
        }

        public bool IsGameOver(GameModel model)
        {
            int freeCellRow = model.PlayerRow;
            int freeCellColumn = model.PlayerColumn;
            if (freeCellRow != 1 || freeCellColumn != GameModel.ColumnCount - 1)
            {
                return false;
            }
            return true;
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    if (model.PlayerColumn == GameModel.ColumnCount - 1)
                    {
                        return false;
                    }

                    model.PlayerValue += model[model.PlayerRow, model.PlayerColumn + 1];
                    model[model.PlayerRow, model.PlayerColumn + 1] = 0;
                    model[model.PlayerRow, model.PlayerColumn] = model[model.PlayerRow, model.PlayerColumn + 1];
                    model[model.PlayerRow, model.PlayerColumn + 1] = model.PlayerValue;
                    model.PlayerColumn++;
                    return true;

                case MoveDirection.Right:
                    if (model.PlayerColumn == 0)
                    {
                        return false;
                    }

                    model.PlayerValue += model[model.PlayerRow, model.PlayerColumn - 1];
                    model[model.PlayerRow, model.PlayerColumn - 1] = 0;
                    model[model.PlayerRow, model.PlayerColumn] = model[model.PlayerRow, model.PlayerColumn - 1];
                    model[model.PlayerRow, model.PlayerColumn - 1] = model.PlayerValue;
                    model.PlayerColumn--;
                    return true;

                case MoveDirection.Up:
                    if (model.PlayerRow == GameModel.RowCount - 1)
                    {
                        return false;
                    }

                    model.PlayerValue += model[model.PlayerRow + 1, model.PlayerColumn];
                    model[model.PlayerRow + 1, model.PlayerColumn] = 0;
                    model[model.PlayerRow, model.PlayerColumn] = model[model.PlayerRow + 1, model.PlayerColumn];
                    model[model.PlayerRow + 1, model.PlayerColumn] = model.PlayerValue;
                    model.PlayerRow++;
                    return true;

                case MoveDirection.Down:
                    if (model.PlayerRow == 0)
                    {
                        return false;
                    }

                    model.PlayerValue += model[model.PlayerRow - 1, model.PlayerColumn];
                    model[model.PlayerRow - 1, model.PlayerColumn] = 0;
                    model[model.PlayerRow, model.PlayerColumn] = model[model.PlayerRow - 1, model.PlayerColumn];
                    model[model.PlayerRow - 1, model.PlayerColumn] = model.PlayerValue;
                    model.PlayerRow--;
                    return true;
            }

            return false;
        }

    }
}
