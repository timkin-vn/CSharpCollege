using Minesweeper.Business.Models;
using Minesweeper.Business.Services;
using System;
using System.Collections.ObjectModel;

namespace Minesweeper.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly GameService _service = new GameService();
        private readonly GameModel _model = new GameModel();
        private string _status;
        private int _moves;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public string Status
        {
            get { return _status; }
            private set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        public int MinesLeft => GameModel.MineCount - _model.FlagsCount;
        public int Moves
        {
            get { return _moves; }
            private set { _moves = value; OnPropertyChanged(nameof(Moves)); }
        }

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _service.Initialize(_model);
            Moves = 0;
            RefreshBoard();
            UpdateStatus();
        }

        public void RevealCell(int row, int column, Action<bool> gameFinishedAction)
        {
            if (_model.IsGameOver) return;
            bool changed = _service.Reveal(_model, row, column);
            if (!changed) return;

            Moves++;
            RefreshBoard();
            UpdateStatus();
            if (_model.IsGameOver) gameFinishedAction?.Invoke(_model.IsWon);
        }

        public void ToggleFlag(int row, int column)
        {
            if (_model.IsGameOver) return;
            if (_service.ToggleFlag(_model, row, column))
            {
                RefreshBoard();
                OnPropertyChanged(nameof(MinesLeft));
            }
        }

        private void UpdateStatus()
        {
            if (_model.IsGameOver)
                Status = _model.IsWon ? "Победа! Все безопасные клетки открыты." : "Вы попали на мину. Игра окончена.";
            else
                Status = "Откройте клетки и избегайте мин. Правая кнопка — поставить флажок.";
            OnPropertyChanged(nameof(MinesLeft));
        }

        private void RefreshBoard()
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
                for (int column = 0; column < GameModel.ColumnCount; column++)
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Number = _model.GetNumber(row, column),
                        IsRevealed = _model.IsRevealed(row, column),
                        IsFlagged = _model.IsFlagged(row, column),
                        IsMine = _model.HasMine(row, column)
                    });
        }
    }
}
