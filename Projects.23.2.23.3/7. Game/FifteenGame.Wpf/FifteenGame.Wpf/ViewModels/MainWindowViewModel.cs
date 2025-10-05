using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel
    {
        private GameService _service = new GameService();

        private GameField _model = new GameField();

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _service.Shuffle(_model);
            FromModel(_model);
        }

        private void FromModel(GameField model)
        {
            Cells.Clear();
            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int column = 0; column < GameField.ColumnCount; column++)
                {
                    if (model[row, column] != GameField.FreeCellValue)
                    {
                        var direction = MoveDirection.None;
                        if (row == model.FreeCellRow)
                        {
                            if (column == model.FreeCellColumn - 1)
                            {
                                direction = MoveDirection.Right;
                            }
                            else if (column == model.FreeCellColumn + 1)
                            {
                                direction = MoveDirection.Left;
                            }
                        }
                        else if (column == model.FreeCellColumn)
                        {
                            if (row == model.FreeCellRow - 1)
                            {
                                direction = MoveDirection.Down;
                            }
                            else if (row == model.FreeCellRow + 1)
                            {
                                direction = MoveDirection.Up;
                            }
                        }

                        Cells.Add(new CellViewModel
                        {
                            Row = row,
                            Column = column,
                            Num = model[row, column],
                            Direction = direction
                        });
                    }
                }
            }
        }

        public void MakeMove(MoveDirection direction, Action gameFinishedAction)
        {
            _service.MakeMove(_model, direction);
            FromModel(_model);
            if (_service.IsGameOver(_model))
            {
                gameFinishedAction();
            }
        }
    }
}
