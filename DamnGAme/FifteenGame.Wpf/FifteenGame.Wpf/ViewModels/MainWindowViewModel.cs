using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private GameService _service = new GameService();
        private GameField _playerField = new GameField();
        private GameField _computerField = new GameField();

        public ObservableCollection<CellViewModel> PlayerCells { get; set; } = new ObservableCollection<CellViewModel>();
        public ObservableCollection<CellViewModel> ComputerCells { get; set; } = new ObservableCollection<CellViewModel>();

        private int _movesCount = 0;
        public int MovesCount
        {
            get => _movesCount;
            set { _movesCount = value; OnPropertyChanged(nameof(MovesCount)); }
        }

        public int PlayerShipsLeft { get; set; } = 20;
        public int ComputerShipsLeft { get; set; } = 20;

        private int _lastHitRow = -1;
        private int _lastHitColumn = -1;
        private bool _huntingMode = false;

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _service.Initialize(_playerField, _computerField);
            UpdateField(_playerField, PlayerCells);
            UpdateField(_computerField, ComputerCells);
            UpdateShipDestroyedStatus();

            MovesCount = 0;
            PlayerShipsLeft = _service.CountShipsLeft(_playerField);
            ComputerShipsLeft = _service.CountShipsLeft(_computerField);

            _lastHitRow = -1;
            _lastHitColumn = -1;
            _huntingMode = false;
        }

        private void UpdateField(GameField model, ObservableCollection<CellViewModel> cells)
        {
            cells.Clear();
            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int column = 0; column < GameField.ColumnCount; column++)
                {
                    cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        State = model[row, column],
                        Direction = MoveDirection.None
                    });
                }
            }
        }

        private void UpdateShipDestroyedStatus()
        {
            foreach (var cell in PlayerCells)
                cell.IsShipDestroyed = _playerField.IsShipDestroyed(cell.Row, cell.Column);

            foreach (var cell in ComputerCells)
                cell.IsShipDestroyed = _computerField.IsShipDestroyed(cell.Row, cell.Column);
        }

        
        public void MakeAttack(int row, int column, Action gameFinishedAction)
        {
            try
            {
                bool hit = _service.PlayerAttack(_computerField, row, column);
                UpdateField(_computerField, ComputerCells);
                UpdateShipDestroyedStatus(); 
                MovesCount++;
                ComputerShipsLeft = _service.CountShipsLeft(_computerField);

                if (_service.IsGameOver(_computerField))
                {
                    gameFinishedAction();
                    return;
                }

                _service.ComputerAttack(_playerField, ref _lastHitRow, ref _lastHitColumn, ref _huntingMode);
                UpdateField(_playerField, PlayerCells);
                UpdateShipDestroyedStatus(); 
                PlayerShipsLeft = _service.CountShipsLeft(_playerField);

                if (_service.IsGameOver(_playerField))
                    gameFinishedAction();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в MakeAttack: " + ex.Message);
            }
        }

        public void ToggleFlag(int row, int column)
        {
            _service.ToggleFlag(_computerField, row, column);
            UpdateField(_computerField, ComputerCells);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
