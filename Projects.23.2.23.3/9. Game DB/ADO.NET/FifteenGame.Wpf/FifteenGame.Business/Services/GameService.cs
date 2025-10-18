using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{
    public class GameService : IGameService
    {
        public void Initialize(GameModel model)
        {
            int value = 1;
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = value++;
                }
            }

            model[GameModel.RowCount - 1, GameModel.ColumnCount - 1] = GameModel.FreeCellValue;
            model.FreeCellRow = GameModel.RowCount - 1;
            model.FreeCellColumn = GameModel.ColumnCount - 1;

            model.MoveCount = 0;
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Left:
                    if (model.FreeCellColumn == GameModel.ColumnCount - 1)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow, model.FreeCellColumn + 1];
                    model[model.FreeCellRow, model.FreeCellColumn + 1] = GameModel.FreeCellValue;
                    model.FreeCellColumn++;

                    model.MoveCount++;
                    return true;

                case MoveDirection.Right:
                    if (model.FreeCellColumn == 0)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow, model.FreeCellColumn - 1];
                    model[model.FreeCellRow, model.FreeCellColumn - 1] = GameModel.FreeCellValue;
                    model.FreeCellColumn--;

                    model.MoveCount++;
                    return true;

                case MoveDirection.Up:
                    if (model.FreeCellRow == GameModel.RowCount - 1)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow + 1, model.FreeCellColumn];
                    model[model.FreeCellRow + 1, model.FreeCellColumn] = GameModel.FreeCellValue;
                    model.FreeCellRow++;

                    model.MoveCount++;
                    return true;

                case MoveDirection.Down:
                    if (model.FreeCellRow == 0)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow - 1, model.FreeCellColumn];
                    model[model.FreeCellRow - 1, model.FreeCellColumn] = GameModel.FreeCellValue;
                    model.FreeCellRow--;

                    model.MoveCount++;
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
                MakeMove(model, nextMove);
            }

            model.MoveCount = 0;
        }

        public bool IsGameOver(GameModel model)
        {
            int freeCellRow = model.FreeCellRow;
            if (freeCellRow != GameModel.RowCount - 1)
            {
                return false;
            }

            int freeCellColumn = model.FreeCellColumn;
            if (freeCellColumn != GameModel.ColumnCount - 1)
            {
                return false;
            }

            int value = 1;
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (row == freeCellRow && column == freeCellColumn)
                    {
                        if (model[row, column] != GameModel.FreeCellValue)
                        {
                            return false;
                        }
                    }
                    else if (model[row, column] != value++)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public GameModel GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public GameModel GetByGameId(int gameId)
        {
            throw new NotImplementedException();
        }

        public void RemoveGame(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
