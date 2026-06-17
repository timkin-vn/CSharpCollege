using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastucture;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IGameService _service = NinjectKernel.Instance.Get<IGameService>();

        private GameModel _model = new GameModel();

        private UserModel _user;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "<нет>";

        public int MoveCount => _model?.MoveCount ?? 0;

        public MainWindowViewModel()
        {
            _model = new GameModel();
            LoadViewModel(_model);
        }

        public void CommitUser(UserModel userModel)
        {
            _user = userModel;
            OnPropertyChanged(nameof(UserName));

            _model = _service.GetByUserId(_user.Id);
            LoadViewModel(_model);
        }

        public void Initialize()
        {
            _model = _service.GetByUserId(_user.Id);
            LoadViewModel(_model);
        }

        public void MakeMove(MoveDirection direction, Action gameFinishedAction)
        {
            _model = _service.MakeMove(_model.Id, direction);
            LoadViewModel(_model);

            if (_service.IsGameOver(_model.Id))
            {
                _service.RemoveGame(_model.Id);
                gameFinishedAction?.Invoke();
            }
        }

        private void LoadViewModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (model[row, column] == Constants.FreeCellValue)
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
                        Direction = direction,
                    });
                }
            }

            OnPropertyChanged(nameof(MoveCount));
        }
    }
}
