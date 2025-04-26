using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Services;
using Ninject;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IGameService _service;
        private GameModel _model = new GameModel();
        private UserModel _userModel;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public string UserName => _userModel?.Name ?? "<нет>";
        public string MoveCountText => (_model?.MoveCount ?? 0).ToString();
        public string ScoreText => (_model?.Score ?? 0).ToString();

        public MainWindowViewModel()
        {
            _service = NinjectKernel.Instance.Get<IGameService>();
        }

        public void MakeMove(int direction, Action gameFinishedAction)
        {
            _model = _service.CheckMatch(_model.GameId, new int[] { direction }, null);

            FromModel(_model);

            if (_service.IsGameOver(_model.GameId))
            {
                _service.RemoveGame(_model.GameId);
                gameFinishedAction();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize()
        {
            _model = _service.GetByUserId(_userModel.Id);
            FromModel(_model);
        }

        public void SetUser(UserModel user)
        {
            _userModel = user;
            Initialize();
            OnPropertyChanged(nameof(UserName));
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Number = model.Field[row, column]
                    });
                }
            }
            OnPropertyChanged(nameof(MoveCountText));
            OnPropertyChanged(nameof(ScoreText));
        }
    }
}
