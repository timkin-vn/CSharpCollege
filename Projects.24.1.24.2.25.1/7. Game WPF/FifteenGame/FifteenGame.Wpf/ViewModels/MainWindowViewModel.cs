using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private GameService _service = new GameService();
        private GameModel _model = new GameModel();

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        private string _gameStatus;
        public string GameStatus
        {
            get => _gameStatus;
            set
            {
                _gameStatus = value;
                OnPropertyChanged(nameof(GameStatus));
            }
        }

        public MainWindowViewModel()
        {
            NewGame();
        }

        public void NewGame()
        {
            _service.Initialize(_model);
            Score = _model.Score;
            GameStatus = "";
            LoadCells();
        }

        public void MakeMove(MoveDirection direction)
        {
            if (_model.IsWin)
            {
                GameStatus = "You won! Press New Game.";
                return;
            }

            bool moved = _service.MakeMove(_model, direction);
            if (!moved) return;

            Score = _model.Score;
            LoadCells();

            // Проверка победы
            for (int i = 0; i < GameModel.Size; i++)
                for (int j = 0; j < GameModel.Size; j++)
                    if (_model[i, j] == 2048 && !_model.IsWin)
                    {
                        _model.IsWin = true;
                        GameStatus = "Congratulations! You've reached 2048!";
                        MessageBox.Show("Вы победили! Нажмите New Game для продолжения.", "Победа!", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

            if (_service.IsGameOver(_model))
            {
                GameStatus = "Game Over! No moves left.";
                MessageBox.Show("Игра окончена! Нет доступных ходов.", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void LoadCells()
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.Size; row++)
            {
                for (int col = 0; col < GameModel.Size; col++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = col,
                        Value = _model[row, col]
                    });
                }
            }
        }
    }
}