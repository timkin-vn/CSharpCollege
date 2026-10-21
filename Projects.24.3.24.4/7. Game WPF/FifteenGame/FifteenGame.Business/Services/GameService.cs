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
        private readonly string[] _colors =
        {
            "Red",
            "Blue",
            "Green",
            "Yellow",
            "Purple",
            "Orange",
            "Pink",
            "Brown",
            "Cyan",
            "Magenta"
        };

        public void Initialize(GameModel model)
        {
            model.Cells.Clear();

            model.FirstOpenedCell = -1;
            model.SecondOpenedCell = -1;
            model.MovesCount = 0;
            model.Seconds = 0;

            model.StartTime = DateTime.Now;
            model.IsPreview = true;
            model.IsGameOver = false;

            int id = 0;

            int pairCount = model.RowCount * model.ColumnCount / 2;

            for (int i = 0; i < pairCount; i++)
            {
                string color = _colors[i];

                model.Cells.Add(new CellModel
                {
                    Id = id++,
                    Color = color,
                    IsOpened = true,
                    IsFound = false
                });

                model.Cells.Add(new CellModel
                {
                    Id = id++,
                    Color = color,
                    IsOpened = true,
                    IsFound = false
                });
            }

            Shuffle(model.Cells);
        }

        public void OpenCell(GameModel model, int index)
        {
            if (index < 0 || index >= model.Cells.Count)
            {
                return;
            }

            if (model.Cells[index].IsFound)
            {
                return;
            }

            if (model.Cells[index].IsOpened || model.Cells[index].IsFound)
            {
                return;
            }

            if (model.SecondOpenedCell != -1)
            {
                return;
            }

            if (model.FirstOpenedCell == index)
            {
                return;
            }

            model.Cells[index].IsOpened = true;
            model.MovesCount++;


            if (model.FirstOpenedCell == -1)
            {
                model.FirstOpenedCell = index;
            }
            else
            {
                model.SecondOpenedCell = index;
            }
        }

        public void CheckOpenedCells(GameModel model)
        {
            if (model.FirstOpenedCell == -1 || model.SecondOpenedCell == -1)
            {
                return;
            }

            if (model.FirstOpenedCell == model.SecondOpenedCell)
            {
                model.FirstOpenedCell = -1;
                model.SecondOpenedCell = -1;
                return;
            }

            CheckPair(model);
        }

        private void CheckPair(GameModel model)
        {
            if (model.FirstOpenedCell == -1 || model.SecondOpenedCell == -1)
            {
                return;
            }

            var first = model.Cells[model.FirstOpenedCell];
            var second = model.Cells[model.SecondOpenedCell];

            if (first.Color == second.Color)
            {
                first.IsFound = true;
                second.IsFound = true;

                first.IsOpened = true;
                second.IsOpened = true;
            }
            else
            {
                first.IsOpened = false;
                second.IsOpened = false;
            }

            model.FirstOpenedCell = -1;
            model.SecondOpenedCell = -1;
            model.IsCheckingPair = false;
        }

        private void Shuffle(List<CellModel> cells)
        {
            Random random = new Random();

            for (int i = cells.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                CellModel temp = cells[i];
                cells[i] = cells[j];
                cells[j] = temp;
            }
        }

        public bool IsGameOver(GameModel model)
        {
            return model.Cells.All(cell => cell.IsFound);
        }
    }
}
