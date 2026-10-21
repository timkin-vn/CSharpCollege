using MemoryGame.Common.BusinessModels;
using MemoryGame.Common.Contracts.Services;
using MemoryGame.Common.Definitions;
using MemoryGame.Common.Infrastructure;
using Ninject;
using System;
using System.Collections.ObjectModel;

namespace MemoryGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IGameService _service = NinjectKernel.Instance.Get<IGameService>();

        private GameModel _model = new GameModel();

        private UserModel _user;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "<нет>";

        public int MoveCount => _model?.MoveCount ?? 0;

        public string PairsText => $"{_model?.MatchedPairCount ?? 0} из {Constants.PairCount}";

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
            _model = _service.GetByUserId(_user.Id);
            LoadViewModel(_model);
        }

        public void MakeMove(int row, int column, Action gameFinishedAction)
        {
            if (_user == null || _model == null || _model.Id == 0)
            {
                return;
            }

            var model = _service.MakeMove(_model.Id, row, column);
            if (model == null)
            {
                // Игра не найдена в БД - начинаем новую
                Initialize();
                return;
            }

            _model = model;
            LoadViewModel(_model);
            if (_service.IsGameOver(_model.Id) ?? false)
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
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Value = model[row, column],
                        State = model.GetState(row, column),
                    });
                }
            }

            OnPropertyChanged(nameof(MoveCount));
            OnPropertyChanged(nameof(PairsText));
        }
    }
}
