using Match3Game.Business.Models;
using Match3Game.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.Wpf.ViewModels
{
    public class MainWindowViewModel
    {
        private GameService _service = new GameService();

        private GameModel _model = new GameModel();

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

        public void MakeMove(MoveDirection direction, Action gameFinishAction)
        {
            _service.MakeMove(_model, direction);
            FromModel(_model);
            if (_service.IsGameOver(_model))
            {
                gameFinishAction?.Invoke();
            }
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] != GameModel.FreeCellValue)
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
    }
}
