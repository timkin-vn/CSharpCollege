using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Business.Services
{
    public class GameService
    {
        

        public void Initialize(GameModel model)
        {
            
            var units = new List<GameModel>
            {
                new GameModel("Д", 100, 20, 0, 1, GameModel.UnitType.Dragon),  // Дракон
                new GameModel("М", 80, 10, 0, 3, GameModel.UnitType.Medic),    // Медик
                new GameModel("Р", 120, 25, 0, 5, GameModel.UnitType.Knight),  // Рыцарь
                new GameModel("К", 150, 15, 0, 7, GameModel.UnitType.King),    // Король
                new GameModel("Б", 500, 40, 6, 2, GameModel.UnitType.Boss)     // Босс
            };
            
            
            var count = 0;
            

            for (int row = 0; row < GameModel.RowCount; row++)
            { 
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = count++;
                }
            }

            foreach (var unit in units)
            {
                model[unit.X, unit.Y] = unit;

               
                if (unit.Type == GameModel.UnitType.Boss)
                {
                    for (int i = 0; i < unit.Height; i++)
                    {
                        for (int j = 0; j < unit.Width; j++)
                        {
                            model[unit.X + i, unit.Y + j] = unit;
                        }
                    }
                }
            }
        }

        public bool IsGameOver(GameModel model)
        {
            int freeCellRow = model.FreeCellRow;
            int freeCellColumn = model.FreeCellColumn;

            if (freeCellRow != GameModel.RowCount - 1 || freeCellColumn != GameModel.ColumnCount - 1)
            {
                return false;
            }

            
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    var unit = model[row, column];
                    if (row == freeCellRow && column == freeCellColumn)
                    {
                        if (unit != null)
                        {
                            return false;
                        }
                    }
                    
                }
            }

            return true;
        }

        public bool MakeMove(GameModel model, MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.XLeft:
                    if (model.FreeCellColumn == GameModel.ColumnCount - 1)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow, model.FreeCellColumn + 1];
                    model[model.FreeCellRow, model.FreeCellColumn + 1] = null;
                    model.FreeCellColumn++;
                    return true;

                case MoveDirection.XRight:
                    if (model.FreeCellColumn == 0)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow, model.FreeCellColumn - 1];
                    model[model.FreeCellRow, model.FreeCellColumn - 1] = null;
                    model.FreeCellColumn--;
                    return true;

                case MoveDirection.YUp:
                    if (model.FreeCellRow == GameModel.RowCount - 1)
                    {
                        return false;
                    }

                    model[model.FreeCellRow, model.FreeCellColumn] = model[model.FreeCellRow + 1, model.FreeCellColumn];
                    model[model.FreeCellRow + 1, model.FreeCellColumn] = null;
                    model.FreeCellRow++;
                    return true;

                case MoveDirection.YDown:
                    if (model.FreeCellRow == 0)
                    {
                        return false;
                    }

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
        }
    }
}
