using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly GameService _engine = new GameService();
        private readonly GameModel _boardData = new GameModel();

        public ObservableCollection<CellViewModel> GridItems { get; } = new ObservableCollection<CellViewModel>();

        private int _score;
        public int Score
        {
            get => _score;
            set { if (_score != value) { _score = value; OnPropertyChanged(nameof(Score)); } }
        }

        private string _statusMessage;
        public string GameStatus
        {
            get => _statusMessage;
            set { if (_statusMessage != value) { _statusMessage = value; OnPropertyChanged(nameof(GameStatus)); } }
        }

        public MainWindowViewModel()
        {
            RestartSession();
        }

        public void RestartSession()
        {
            _engine.ResetState(_boardData);
            Score = _boardData.Score;
            GameStatus = string.Empty;
            RefreshUI();
        }

        public void HandleDirection(MoveDirection dir)
        {
            if (_boardData.IsWin) return;

            if (!_engine.MakeMove(_boardData, dir)) return;

            Score = _boardData.Score;
            RefreshUI();

            CheckWinCondition();

            if (_engine.CheckIfLost(_boardData))
            {
                GameStatus = "GAME OVER";
                MessageBox.Show("Больше нет ходов!", "Игра окончена", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void CheckWinCondition()
        {
            if (_boardData.IsWin) return;

            foreach (var cell in _boardData.Cells)
            {
                if (cell >= 2048)
                {
                    _boardData.IsWin = true;
                    GameStatus = "YOU WIN!";
                    MessageBox.Show("Вы собрали 2048!", "Победа", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                }
            }
        }

        private void RefreshUI()
        {
            GridItems.Clear();
            for (int r = 0; r < GameModel.Size; r++)
            {
                for (int c = 0; c < GameModel.Size; c++)
                {
                    GridItems.Add(new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        Value = _boardData[r, c]
                    });
                }
            }
        }
    }
}