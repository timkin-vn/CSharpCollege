using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.ObjectModel;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly GameService _service = new GameService();

        private readonly GameModel _model = new GameModel();

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public int Score { get; private set; }

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _service.Initialize(_model);
            Score = _model.Score;
            OnPropertyChanged(nameof(Score));
            LoadViewModel(_model);
        }

        public void MakeMove(MoveDirection direction, Action gameOverAction, Action winAction)
        {
            if (!_service.MakeMove(_model, direction))
            {
                if (_service.IsGameOver(_model))
                {
                    gameOverAction?.Invoke();
                }

                return;
            }

            Score = _model.Score;
            OnPropertyChanged(nameof(Score));
            LoadViewModel(_model);

            if (_model.HasWon)
            {
                winAction?.Invoke();
            }
            else if (_service.IsGameOver(_model))
            {
                gameOverAction?.Invoke();
            }
        }

        public void ContinueAfterWin()
        {
            _service.ContinueAfterWin(_model);
        }

        private void LoadViewModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    Cells.Add(CellViewModel.Create(row, column, model[row, column]));
                }
            }
        }
    }
}
