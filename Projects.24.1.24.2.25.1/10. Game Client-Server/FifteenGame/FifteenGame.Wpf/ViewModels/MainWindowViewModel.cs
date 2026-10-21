
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Wpf.Views;
using Ninject;
using System;
using System.Collections.ObjectModel;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameService _service = NinjectKernel.Instance.Get<IGameService>();
        private GameModel _model = new GameModel();
        private UserModel _user;

        public ObservableCollection<CellViewModel> Cells { get; } =
            new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "<нет>";
        public int MoveCount => _model?.MoveCount ?? 0;
        public int Score => _model?.Score ?? 0;
        public int BestScore => _model?.BestScore ?? 0;

        public bool IsGameOver
        {
            get
            {
                if (_model == null)
                {
                    return false;
                }

                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        int value = _model[row, column];

                        if (value == 0)
                        {
                            return false;
                        }

                        if (column + 1 < Constants.ColumnCount &&
                            value == _model[row, column + 1])
                        {
                            return false;
                        }

                        if (row + 1 < Constants.RowCount &&
                            value == _model[row + 1, column])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }


        public MainWindowViewModel()
        {
            LoadViewModel(_model);
        }

        public void CommitUser(UserModel user)
        {
            _user = user;
            OnPropertyChanged(nameof(UserName));

            _model = _service.GetByUserId(_user.Id);
            LoadViewModel(_model);
        }

        public void Initialize()
        {
            if (_user == null)
            {
                return;
            }

            _model = _service.GetByUserId(_user.Id);
            LoadViewModel(_model);
        }

        public void MakeMove(MoveDirection direction, Action gameFinishedAction)
        {
            if (_model == null || IsGameOver)
            {
                gameFinishedAction?.Invoke();
                return;
            }

            _model = _service.MakeMove(_model.Id, direction);
            LoadViewModel(_model);

            if (IsGameOver)
            {
                gameFinishedAction?.Invoke();
            }
        }

        public void NewGame()
        {
            if (_user == null)
                return;

            _model = _service.NewGame(_user.Id);
            LoadViewModel(_model);
        }

        private void LoadViewModel(GameModel model)
        {
            
            Cells.Clear();

            if (model != null)
            {
                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int column = 0; column < Constants.ColumnCount; column++)
                    {
                        Cells.Add(new CellViewModel
                        {
                            Row = row,
                            Column = column,
                            Value = model[row, column]
                        });
                    }
                }
            }

            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(MoveCount));
            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(BestScore));
            OnPropertyChanged(nameof(IsGameOver));
        }
    }
}
