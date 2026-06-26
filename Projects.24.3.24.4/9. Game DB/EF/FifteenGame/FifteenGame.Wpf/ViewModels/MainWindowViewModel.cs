using FifteenGame.Business.Services;
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
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameService _gameService;
        private GameModel _gameModel;
        private string _statusText;
        private int _money;
        private int _turnsPlayed;
        private int _winStreak;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public MainWindowViewModel(IGameService gameService)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            CellClickCommand = new RelayCommand<CellViewModel>(OnCellClick);
            RestartCommand = new RelayCommand<object>(OnRestart);
            StartNewGame(1);
        }

        public MainWindowViewModel()
        {
            CellClickCommand = new RelayCommand<CellViewModel>(OnCellClick);
            RestartCommand = new RelayCommand<object>(OnRestart);

            try
            {
                _gameService = NinjectKernel.Instance.Get<IGameService>();
                StartNewGame(1);
            }
            catch (Exception ex)
            {
                StatusText = $"Ошибка инициализации контейнера: {ex.Message}";
            }
        }

        // Свойства для вывода денег и ходов на экран
        public int Money
        {
            get => _money;
            set { _money = value; OnPropertyChanged(nameof(Money)); }
        }

        public int TurnsPlayed
        {
            get => _turnsPlayed;
            set { _turnsPlayed = value; OnPropertyChanged(nameof(TurnsPlayed)); }
        }

        public int WinStreak
        {
            get => _winStreak;
            set { _winStreak = value; OnPropertyChanged(nameof(WinStreak)); }
        }

        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(nameof(StatusText)); }
        }

        public ICommand CellClickCommand { get; }
        public ICommand RestartCommand { get; }

        private void StartNewGame(int userId)
        {
            _gameModel = _gameService.GetByUserId(userId);
            WinStreak = _gameService.GetUserWinStreak(_gameModel.UserId);
            UpdateProperties();
            RefreshGrid();
        }

        public void CommitUser(FifteenGame.Common.BusinessModels.UserModel userModel)
        {
            if (userModel != null)
            {
                StartNewGame(userModel.Id);
            }
        }

        private void OnCellClick(CellViewModel cell)
        {
            if (cell == null || _gameService.IsGameOver(_gameModel)) return;

            _gameService.Move(_gameModel, cell.Row, cell.Column);

            WinStreak = _gameService.GetUserWinStreak(_gameModel.UserId);

            UpdateProperties();
            RefreshGrid();
        }

        // Обработчик кнопки перезапуска
        private void OnRestart(object parameter)
        {
            if (_gameModel == null) return;

            _gameModel = _gameService.RestartGame(_gameModel.UserId);

            // Перерисовываем чистый экран
            UpdateProperties();
            RefreshGrid();
        }

        // Обновляем счетчики денег и ходов
        private void UpdateProperties()
        {
            Money = _gameModel.Money;
            TurnsPlayed = _gameModel.TurnsPlayed;

            if (_gameService.IsGameOver(_gameModel))
            {
                StatusText = $"Игра окончена! Ваша финальная касса: {Money} руб.";
            }
            else
            {
                StatusText = $"Ход {TurnsPlayed} из {Constants.TargetTurns}. Развивайте сеть!";
            }
        }

        // Перерисовываем сетку кнопок на экране
        private void RefreshGrid()
        {
            Cells.Clear();

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    // Покрыта ли клетка и сколько рядом ЗОЖников
                    bool covered = _gameService.IsCellCovered(_gameModel, row, column);
                    int veggieNeighbors = _gameService.GetVeggieNeighborsCount(_gameModel, row, column);

                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        PeopleCount = _gameModel.GetPeopleCount(row, column),
                        HasShop = _gameModel.GetHasShop(row, column),
                        IsCovered = covered,
                        IsVeggie = _gameModel.GetIsVeggie(row, column),
                        IsRevealed = _gameModel.GetIsRevealed(row, column),
                        VeggieNeighborsCount = veggieNeighbors
                    });
                }
            }
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        public RelayCommand(Action<T> execute) => _execute = execute;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute((T)parameter);
        public event EventHandler CanExecuteChanged { add { } remove { } }
    }
}