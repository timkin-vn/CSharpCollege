using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastructure;
using Ninject;
using System;
using System.Collections.ObjectModel;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameService _service =
            NinjectKernel.Instance.Get<IGameService>();

        private GameModel _model = new GameModel();
        private UserModel _user;

        public ObservableCollection<CellViewModel> Cells { get; } =
            new ObservableCollection<CellViewModel>();

        public string UserName
        {
            get { return _user == null ? "<нет>" : _user.Name; }
        }

        public int MoveCount
        {
            get { return _model.MoveCount; }
        }

        public int Score
        {
            get { return _model.Score; }
        }

        public int BestScore
        {
            get { return _model.BestScore; }
        }

        public bool IsGameOver
        {
            get { return _model.IsGameOver; }
        }

        public MainWindowViewModel()
        {
            LoadViewModel();
        }

        public void CommitUser(UserModel user)
        {
            _user = user;
            OnPropertyChanged(nameof(UserName));

            _model = _service.GetByUserId(_user.Id);
            LoadViewModel();
        }

        public void Initialize()
        {
            if (_user == null)
                return;

            _model = _service.NewGame(_user.Id);
            LoadViewModel();
        }

        public void MakeMove(
            MoveDirection direction,
            Action gameFinishedAction)
        {
            if (_user == null || _model.IsGameOver)
                return;

            int previousMoveCount = _model.MoveCount;

            _model = _service.MakeMove(_model.Id, direction);
            LoadViewModel();

            if (_model.IsGameOver &&
                previousMoveCount != _model.MoveCount)
            {
                gameFinishedAction?.Invoke();
            }
        }

        private void LoadViewModel()
        {
            Cells.Clear();

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0;
                     column < Constants.ColumnCount;
                     column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Value = _model[row, column]
                    });
                }
            }

            OnPropertyChanged(nameof(MoveCount));
            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(BestScore));
            OnPropertyChanged(nameof(IsGameOver));
        }
    }
}