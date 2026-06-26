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

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public MainWindowViewModel(IGameService gameService)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            CellClickCommand = new RelayCommand<CellViewModel>(OnCellClick);
            StartNewGame(1);
        }

        public MainWindowViewModel()
        {
            CellClickCommand = new RelayCommand<CellViewModel>(OnCellClick);

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

        // Свойства для вывода денег и ходов на экран WPF
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

        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(nameof(StatusText)); }
        }

        // Команда для клика по клетке мышкой
        public ICommand CellClickCommand { get; }


        private void StartNewGame(int userId)
        {
            _gameModel = _gameService.GetByUserId(userId);
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

            // Вызываем нашу шаурмичную бизнес-логику хода
            _gameService.Move(_gameModel, cell.Row, cell.Column);

            // Обновляем данные на экране после хода
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
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        PeopleCount = _gameModel.GetPeopleCount(row, column),
                        HasShop = _gameModel.GetHasShop(row, column),
                        IsVeggie = _gameModel.GetIsVeggie(row, column),
                        IsRevealed = _gameModel.GetIsRevealed(row, column)
                    });
                }
            }
        }
    }

    // Простой класс-помощник для обработки кликов (если у препода используется другой, студия подскажет)
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        public RelayCommand(Action<T> execute) => _execute = execute;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute((T)parameter);
        public event EventHandler CanExecuteChanged { add { } remove { } }
    }
}