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

        private GameModel _gameModel = new GameModel();

        private UserModel _user;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "<нет>";

        public string MoveCountText => (_gameModel?.CountRevealed ?? 0).ToString();
        private int _countFlags;
        public int CountFlags
        {
            get => _countFlags;
            set
            {
                _countFlags = value;
                OnPropertyChanged(nameof(CountFlags));
            }
        }

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void SetUser(UserModel user)
        {
            _user = user;
            OnPropertyChanged(nameof(UserName));

            _gameModel = _gameService.GetByUserId(_user.Id);
            FromModel(_gameModel);
        }

        public void MakeMove(int x, int y, Action gameFinishedAction)
        {
            _gameModel = _gameService.RevealCell(_gameModel.Id, x, y);
            FromModel(_gameModel);

            if (_gameService.IsGameOver(_gameModel.Id))
            {
                _gameService.RemoveGame(_gameModel.Id);
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
            _gameModel = new GameModel { CountRevealed = 0, };
            FromModel(_gameModel);
        }

        public void ReInitialize()
        {
            _gameModel = _gameService.GetByUserId(_user.Id);
            FromModel(_gameModel);
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();
            CountFlags = model.RedFlags;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    var cellModel = model[row, column];

                    var cellViewModel = new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Num = cellModel.NeightborMines,
                        IsRevealed = cellModel.IsRevealed,
                        IsFlagged = cellModel.Isflag,
                        IsmIne = cellModel.IsMine, 
                    };

                    Cells.Add(cellViewModel);
                }
            }

            // Обновление текста количества ходов
            OnPropertyChanged(nameof(MoveCountText));
        }
    }
}
