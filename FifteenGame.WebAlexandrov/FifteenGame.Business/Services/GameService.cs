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
        public GameModel SelectedUnit;

        private bool isUnitSelected = false;
        public GameModel selectedUnit;
        public void Initialize(GameModel model)
        {



            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Модель не может быть null.");
            }

            var units = new List<GameModel>
            {
                new GameModel(" ", 0, 0, 0, 0, GameModel.UnitType.None),     //Nothing
                new GameModel("Д", 100, 20, 0, 1, GameModel.UnitType.Dragon),  // Дракон
                new GameModel("М", 80, 10, 0, 3, GameModel.UnitType.Medic),    // Медик
                new GameModel("Р", 120, 25, 0, 5, GameModel.UnitType.Knight),  // Рыцарь
                new GameModel("К", 150, 15, 0, 7, GameModel.UnitType.King),    // Король
                new GameModel("Б", 500, 40, 4, 2, GameModel.UnitType.Boss)     // Босс
            };





            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    model[row, column] = null;
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

        public bool MakeMove(GameModel model, int targetX, int targetY, GameModel currentModel)
        {
            
            var selectedX = model.FreeCellRow;
            var selectedY = model.FreeCellColumn;

            
            if (IsAdjacent(selectedX, selectedY, targetX, targetY))
            {
                
                var temp = model[targetX, targetY];
                model[targetX, targetY] = model[selectedX, selectedY]; 
                model[selectedX, selectedY] = temp; 

                model.FreeCellRow = targetX;
                model.FreeCellColumn = targetY;

                return true; 
            }

            return false; 
        }

        private bool IsAdjacent(int x1, int y1, int x2, int y2)
        {
            
            return (Math.Abs(x1 - x2) == 1 && y1 == y2) || (Math.Abs(y1 - y2) == 1 && x1 == x2);
        }
        private void HighlightAdjacentCells(GameModel model)
        {

            int row = model.FreeCellRow;
            int column = model.FreeCellColumn;


            if (row > 0) HighlightCell(row - 1, column);
            if (row < GameModel.RowCount - 1) HighlightCell(row + 1, column);
            if (column > 0) HighlightCell(row, column - 1);
            if (column < GameModel.ColumnCount - 1) HighlightCell(row, column + 1);
        }

        private void HighlightCell(int row, int column)
        {

        }
        public void Shuffle(GameModel model)
        {
            Initialize(model);


        }
    }
}
