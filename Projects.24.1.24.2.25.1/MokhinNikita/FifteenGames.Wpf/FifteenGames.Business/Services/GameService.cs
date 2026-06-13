using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FifteenGames.Common.BusinessModels;
using static FifteenGames.Common.Definitions.Constants;
namespace FifteenGames.Business.Services
{
    public class GameService
    {
        public void Initialize(GameModel model)
        {
            int value = 1;
            for(int row = 0; row < RowCount; row++)
            {
                for(int col = 0; col < ColumnCount; col++)
                {
                    model[row, col] = value++;
                }
            }

            model[RowCount - 1, ColumnCount - 1] = FreeCellValue;
            model.FreeCellColumn = ColumnCount - 1;
            model.FreeCellRow = RowCount - 1;
        }
        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            switch (direction) {
                case MoveDirection.Up:
                    if (model.FreeCellRow == ColumnCount - 1)
                    {
                        return false;
                    }
                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow + 1, model.FreeCellColumn];
                    model[model.FreeCellRow + 1, model.FreeCellColumn] = FreeCellValue;
                    model.FreeCellRow++;
                    return true;
                case MoveDirection.Down:
                    if (model.FreeCellRow == 0)
                    {
                        return false;
                    }
                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow - 1, model.FreeCellColumn];
                    model[model.FreeCellRow - 1, model.FreeCellColumn] = FreeCellValue;
                    model.FreeCellRow--;
                    return true;
                case MoveDirection.Left:
                    if(model.FreeCellColumn == ColumnCount - 1)
                    {
                        return false;
                    }
                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow, model.FreeCellColumn + 1];
                    model[model.FreeCellRow, model.FreeCellColumn + 1] = FreeCellValue;
                    model.FreeCellColumn++;
                    return true;
                case MoveDirection.Right:
                    if (model.FreeCellColumn == 0)
                    {
                        return false;
                    }
                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow, model.FreeCellColumn - 1];
                    model[model.FreeCellRow, model.FreeCellColumn - 1] = FreeCellValue;
                    model.FreeCellColumn--;
                    return true;
            }
            return false;
        }
        public void Shuffle(GameModel model)
        {
            Initialize(model);
            var rnd = new Random();
            for(int i = 0; i < 1000; i++)
            {
                var nextMove = (MoveDirection)(rnd.Next(4) + 1);
                MakeMove(model, nextMove);
            }
        }
        public bool IsGameOver(GameModel model)
        {
            int freeCellRow = model.FreeCellRow;
            if (freeCellRow != RowCount - 1) return false;

            int freeCellColumn = model.FreeCellColumn;
            if (freeCellColumn != RowCount - 1) return false;
            int value = 1;
            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColumnCount; col++)
                {
                    if(row == freeCellRow && col == freeCellColumn)
                    {
                        return model[row, col] == FreeCellValue;
                    }
                    else if (model[row, col] != value++)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
