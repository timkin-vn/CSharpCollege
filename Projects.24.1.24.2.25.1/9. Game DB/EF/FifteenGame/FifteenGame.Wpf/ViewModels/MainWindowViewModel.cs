
using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Wpf.Views;
using Ninject;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

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

        public string UserName => _user?.Name ?? "<нет>";
        public int MoveCount => _model.MoveCount;
        public int Score => _model.Score;
        public int BestScore => _model.BestScore;

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

            _model = _service.NewGame(_user.Id);
            LoadViewModel(_model);
        }

        public void MakeMove(MoveDirection direction, Action gameFinishedAction = null)
        {
            if (_model == null || _model.Id == 0)
            {
                return;
            }

            _model = _service.MakeMove(_model.Id, direction);
            LoadViewModel(_model);

            if (_model != null && _service.IsGameOver(_model.Id) == true)
            {
                gameFinishedAction?.Invoke();
            }
        }

        private void LoadViewModel(GameModel model)
        {
            Cells.Clear();

            if (model == null)
            {
                OnPropertyChanged(nameof(MoveCount));
                OnPropertyChanged(nameof(Score));
                OnPropertyChanged(nameof(BestScore));
                return;
            }

            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Value = model[row, column]
                    });
                }
            }

            OnPropertyChanged(nameof(MoveCount));
            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(BestScore));
        }
    }
}
