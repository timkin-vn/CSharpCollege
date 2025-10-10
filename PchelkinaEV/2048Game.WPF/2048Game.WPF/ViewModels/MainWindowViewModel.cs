using _2048Game.Business.Services;
using _2048Game.Business.Models;
using _2048Game.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace _2048Game.WPF.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _gameService;
        private int _bestScore;
        private int _score;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<GameTileViewModel> Tiles { get; set; }

        public int Score
        {
            get => _score;
            private set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged();
                }
            }
        }

        public int BestScore
        {
            get => _bestScore;
            private set
            {
                if (_bestScore != value)
                {
                    _bestScore = value;
                    OnPropertyChanged();
                }
            }
        }
        public MainWindowViewModel()
        {
            _gameService = new GameService();
            Tiles = new ObservableCollection<GameTileViewModel>();

            BestScore = Properties.Settings.Default.BestScore;
            Score = 0;

            _gameService.BoardUpdated += UpdateTiles;
            _gameService.GameOver += OnGameOver;

            _gameService.StartGame();
        }

        private void UpdateTiles()
        {
            Tiles.Clear();

            bool has2048 = false;

            for (int r  = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    int value = _gameService.Board.Tiles[r, c];
                    Tiles.Add(new GameTileViewModel
                    {
                        Value = value
                    });

                    if (value == 2048)
                    {
                        has2048 = true;
                    }
                }
            }

            Score = _gameService.CurrentScore;
            UpdateBestScore();

            if (has2048)
            {
                OnVictory();
            }
        }

        private void UpdateBestScore()
        {
            if (Score > BestScore)
            {
                BestScore = Score;

                Properties.Settings.Default.BestScore = BestScore;
                Properties.Settings.Default.Save();
            }
        }

        public void HandKey(Key key)
        {
            switch(key)
            {
                case Key.Up:
                    _gameService.Move(MoveDirection.Direction.Up);
                    break;
                case Key.Down:
                    _gameService.Move(MoveDirection.Direction.Down);
                    break;
                case Key.Left:
                    _gameService.Move(MoveDirection.Direction.Left);
                    break;
                case Key.Right:
                    _gameService.Move(MoveDirection.Direction.Right);
                    break;
            }
        }

        private void OnVictory()
        {
            UpdateBestScore();

            var res = MessageBox.Show($"Поздравляем! Вы собрали 2048! \nВаш счет: {Score}\nНаилучший счет: {BestScore}\nЖелаете продолжить?", "Победа!", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (res == MessageBoxResult.Yes)
            {
                _gameService.Restart();
                _gameService.StartGame();
            }

        }

        private void OnGameOver()
        {
            UpdateBestScore();
            var res = MessageBox.Show($"Игра окончена!\nТекущий счет: {Score}\nЛучший счет: {BestScore}\nЖелаете перезапустить игру?", "Стоп!", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (res == MessageBoxResult.Yes)
            {
                _gameService.Restart();
                _gameService.StartGame();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
