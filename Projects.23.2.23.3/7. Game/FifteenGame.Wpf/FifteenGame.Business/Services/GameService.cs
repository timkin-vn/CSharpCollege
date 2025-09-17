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
        public void Initialize(GameField model)
        {
            int value = 1;
            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int column = 0; column < GameField.ColumnCount; column++)
                {
                    model[row, column] = value++;
                }
            }

            model[GameField.RowCount - 1, GameField.ColumnCount - 1] = GameField.FreeCellValue;
            model.FreeCellRow = GameField.RowCount - 1;
            model.FreeCellColumn = GameField.ColumnCount - 1;
        }

        public bool MakeMove(GameField model, MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    if (model.FreeCellColumn == GameField.ColumnCount - 1)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow, model.FreeCellColumn + 1];
                    model[model.FreeCellRow, model.FreeCellColumn + 1] = GameField.FreeCellValue;
                    model.FreeCellColumn++;
                    return true;

                case MoveDirection.Right:
                    if (model.FreeCellColumn == 0)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow, model.FreeCellColumn - 1];
                    model[model.FreeCellRow, model.FreeCellColumn - 1] = GameField.FreeCellValue;
                    model.FreeCellColumn--;
                    return true;

                case MoveDirection.Up:
                    if (model.FreeCellRow == GameField.RowCount - 1)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow + 1, model.FreeCellColumn];
                    model[model.FreeCellRow + 1, model.FreeCellColumn] = GameField.FreeCellValue;
                    model.FreeCellRow++;
                    return true;

                case MoveDirection.Down:
                    if (model.FreeCellRow == 0)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow - 1, model.FreeCellColumn];
                    model[model.FreeCellRow - 1, model.FreeCellColumn] = GameField.FreeCellValue;
                    model.FreeCellRow--;
                    return true;
            }

            return false;
        }
    }
}
