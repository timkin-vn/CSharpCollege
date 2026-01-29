using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IGameService _gameService = NinjectKernel.Instance.Get<IGameService>();
        private readonly IUserService _userService = NinjectKernel.Instance.Get<IUserService>();

        private GameModel _model = new GameModel();
        private UserModel _user;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public string UserName { get { return _user != null ? _user.Name : "<нет>"; } }
        public int MoveCount { get { return _model != null ? _model.MoveCount : 0; } }
        public int Score { get { return _model != null ? _model.Score : 0; } }
        public int BestScore { get { return _user != null ? _user.BestScore : 0; } }
        public bool IsLoggedIn { get { return _user != null; } }

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void SetUser(UserModel user)
        {
            _user = user;

            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(BestScore));
            OnPropertyChanged(nameof(IsLoggedIn));

            _model = _gameService.GetByUserId(_user.Id);
            FromModel(_model);
        }

        public void Initialize()
        {
            _model = new GameModel();
            FromModel(_model);
        }

        public void ReInitialize()
        {
            if (_user == null) return;

            _model = _gameService.GetByUserId(_user.Id);
            FromModel(_model);
        }

        public void NewGame()
        {
            if (_user == null) return;

            if (_model != null && _model.Id != 0)
                _gameService.RemoveGame(_model.Id);

            _model = _gameService.GetByUserId(_user.Id);
            FromModel(_model);
        }

        public void Logout()
        {
            if (_model != null && _model.Id != 0)
                _gameService.RemoveGame(_model.Id);

            _user = null;
            _model = new GameModel();
            Cells.Clear();

            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(BestScore));
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(MoveCount));
            OnPropertyChanged(nameof(Score));
        }

        public void MakeMove(MoveDirection direction, Action gameFinishAction)
        {
            if (_user == null || _model == null || _model.Id == 0) return;

            _model = _gameService.MakeMove(_model.Id, direction);

            if (_model.Score > _user.BestScore)
            {
                _user.BestScore = _model.Score;
                _userService.UpdateBestScore(_user.Id, _user.BestScore);
                OnPropertyChanged(nameof(BestScore));
            }

            FromModel(_model);

            if (_model.IsWin || _model.IsLose)
            {
                _gameService.RemoveGame(_model.Id);
                gameFinishAction?.Invoke();
            }
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();

            for (int r = 0; r < Constants.RowCount; r++)
            {
                for (int c = 0; c < Constants.ColumnCount; c++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        Value = model[r, c]
                    });
                }
            }

            OnPropertyChanged(nameof(MoveCount));
            OnPropertyChanged(nameof(Score));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}