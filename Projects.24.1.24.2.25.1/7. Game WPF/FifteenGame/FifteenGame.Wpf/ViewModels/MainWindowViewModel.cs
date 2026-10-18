
using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.ObjectModel;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly GameService _service = new GameService();
        private readonly GameModel _model = new GameModel();

        public ObservableCollection<CellViewModel> Cells { get; }
            = new ObservableCollection<CellViewModel>();

        public bool CanUndo => _service.CanUndo;

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _service.Shuffle(_model);
            LoadViewModel(_model);

            OnPropertyChanged(nameof(CanUndo));
        }

        public void MakeMove(
            MoveDirection direction,
            Action gameFinishedAction)
        {
            bool moved = _service.MakeMove(_model, direction);

            if (!moved)
            {
                return;
            }

            LoadViewModel(_model);
            OnPropertyChanged(nameof(CanUndo));

            if (_service.IsGameOver(_model))
            {
                gameFinishedAction?.Invoke();
            }
        }

        public void UndoLastMove()
        {
            if (!_service.Undo(_model))
            {
                return;
            }

            LoadViewModel(_model);
            OnPropertyChanged(nameof(CanUndo));
        }

        private void LoadViewModel(GameModel model)
        {
            Cells.Clear();

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] == GameModel.FreeCellValue)
                    {
                        continue;
                    }

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
                        Value = model[row, column],
                        Direction = direction
                    });
                }
            }
        }
    }
}
