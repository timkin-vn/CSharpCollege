using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.ObjectModel;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel
    {
        private GameService _service = new GameService();
        private GameField _playerField;
        private GameField _computerField;

        public ObservableCollection<CellViewModel> PlayerCells { get; set; } = new ObservableCollection<CellViewModel>();
        public ObservableCollection<CellViewModel> ComputerCells { get; set; } = new ObservableCollection<CellViewModel>();

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _playerField = new GameField();  // Создаем новые поля
            _computerField = new GameField();
            _service.Initialize(_playerField, _computerField);
            FromModel(_playerField, PlayerCells);
            FromModel(_computerField, ComputerCells);
        }

        private void FromModel(GameField model, ObservableCollection<CellViewModel> cells)
        {
            cells.Clear();
            for (int row = 0; row < GameField.RowCount; row++)
            {
                for (int column = 0; column < GameField.ColumnCount; column++)
                {
                    CellViewModel cell = new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        State = model[row, column] == 'S' && cells == ComputerCells ? ' ' : model[row, column], // Скрываем корабли компьютера
                        Direction = MoveDirection.None
                    };
                    cell.UpdateDisplay();
                    cells.Add(cell);
                }
            }
        }

        public void MakeAttack(int row, int column, Action playerWonAction, Action computerWonAction)
        {
            if (_service.PlayerAttack(_computerField, row, column))
            {
                // Попадание, игрок может атаковать снова (для простоты)
            }
            FromModel(_computerField, ComputerCells);

            if (_service.IsGameOver(_computerField))
            {
                playerWonAction();
                return;
            }

            _service.ComputerAttack(_playerField);
            FromModel(_playerField, PlayerCells); // Обновляем поле игрока
            if (_service.IsGameOver(_playerField))
            {
                computerWonAction();
            }
        }
    }
}