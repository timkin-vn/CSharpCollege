using System.Collections.ObjectModel;
using Game2048.Wpf.Models;

namespace Game2048.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly GameModel _gameModel;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        private int _score;
        public int Score
        {
            get => _score;
            set { _score = value; OnPropertyChanged(nameof(Score)); }
        }

        private string _statusText;
        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(nameof(StatusText)); }
        }

        public MainWindowViewModel()
        {
            _gameModel = new GameModel();

            
            for (int i = 0; i < 16; i++)
            {
                Cells.Add(new CellViewModel());
            }

            UpdateView();
        }

        public void HandleKeyPress(MoveDirection direction)
        {
            if (_gameModel.MakeMove(direction))
            {
                UpdateView();
            }
        }

        public void RestartGame()
        {
            _gameModel.Reset();
            UpdateView();
        }

        private void UpdateView()
        {
            
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    int index = r * 4 + c;
                    Cells[index].Value = _gameModel.Board[r, c];
                }
            }

            Score = _gameModel.Score;

            if (_gameModel.IsGameOver)
            {
                StatusText = _gameModel.IsWin ? "ВЫ ПОБЕДИЛИ (2048)!" : "ИГРА ОКОНЧЕНА!";
            }
            else
            {
                StatusText = "Используйте стрелки для хода";
            }
        }
    }
}
