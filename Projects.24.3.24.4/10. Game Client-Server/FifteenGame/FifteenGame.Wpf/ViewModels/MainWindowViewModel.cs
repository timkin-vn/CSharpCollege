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
        private int _winStreak;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "<нет>";

        // Новые свойства для вывода экономики в интерфейс WPF
        public int Money => _model?.Money ?? 0;
        public int MoveCount => _model?.TurnsPlayed ?? 0;
        public int WinStreak => _winStreak;

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
            UpdateWinStreak();
            LoadViewModel(_model);
        }

        public void Initialize()
        {
            _model = _service.GetByUserId(_user.Id);
            UpdateWinStreak();
            LoadViewModel(_model);
        }

        // Клик по клетке: строим ларёк по координатам row и column
        public void MakeMove(int row, int column, Action gameFinishedAction)
        {
            if (_model == null) return;

            // Вызываем наш обновленный метод сервиса/прокси
            _service.Move(_model, row, column);

            LoadViewModel(_model);

            if (_service.IsGameOver(_model))
            {
                UpdateWinStreak(); // Обновляем стрик, так как на сервере игра завершилась
                gameFinishedAction?.Invoke();
            }
        }

        // Метод сброса игры для кнопки "Начать заново"
        public void RestartGame()
        {
            if (_user == null) return;
            _model = _service.RestartGame(_user.Id);
            UpdateWinStreak();
            LoadViewModel(_model);
        }

        private void UpdateWinStreak()
        {
            if (_user != null)
            {
                _winStreak = _service.GetUserWinStreak(_user.Id);
                OnPropertyChanged(nameof(WinStreak));
            }
        }

        private void LoadViewModel(GameModel model)
        {
            Cells.Clear();

            // Заполняем коллекцию клеток актуальными данными шаурмичной матрицы
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        PeopleCount = model.GetPeopleCount(row, column),
                        HasShop = model.GetHasShop(row, column),
                        IsVeggie = model.GetIsVeggie(row, column),
                        IsRevealed = model.GetIsRevealed(row, column)
         
                    });
                }
            }

            // Уведомляем WPF об изменении параметров, чтобы обновился экран
            OnPropertyChanged(nameof(MoveCount));
            OnPropertyChanged(nameof(Money));
        }
    }
}