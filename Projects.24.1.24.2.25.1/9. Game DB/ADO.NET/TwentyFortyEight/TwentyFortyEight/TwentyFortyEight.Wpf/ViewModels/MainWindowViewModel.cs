using System;
using System.Collections.ObjectModel;
using Ninject;
using TwentyFortyEight.Common.BusinessModels;
using TwentyFortyEight.Common.Contracts.Services;
using TwentyFortyEight.Common.Definitions;
using TwentyFortyEight.Common.Infrastructure;

namespace TwentyFortyEight.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameService _service =
            NinjectKernel.Instance.Get<IGameService>();

        private GameModel _model;
        private UserModel _user;

        public ObservableCollection<CellViewModel> Cells { get; }
            = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "Гость";

        public int Score => _model?.Score ?? 0;

        public int BestTile => _model?.BestTile ?? 0;

        public bool IsGameActive => _user != null;

        public MainWindowViewModel()
        {
        }

        public void CommitUser(UserModel user)
        {
            _user = user;
            _model = _service.GetByUserId(user.Id);
            RefreshBoard();
            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(IsGameActive));
        }

        public void MakeMove(MoveDirection direction, Action gameFinishedAction, Action gameWonAction)
        {
            if (_model == null) return;

            _model = _service.MakeMove(_model.Id, direction);
            RefreshBoard();

            if (_service.IsGameWon(_model.Id) && _model.IsWon)
            {
                gameWonAction?.Invoke();
                _model.IsWon = false; // allow continuing
            }
            else if (_service.IsGameOver(_model.Id))
            {
                gameFinishedAction?.Invoke();
            }
        }

        public void NewGame()
        {
            if (_user == null) return;
            _service.ResetGame(_user.Id);
            _model = _service.GetByUserId(_user.Id);
            RefreshBoard();
        }

        private void RefreshBoard()
        {
            Cells.Clear();

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Value = _model[row, column]
                    });
                }
            }

            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(BestTile));
        }
    }
}
