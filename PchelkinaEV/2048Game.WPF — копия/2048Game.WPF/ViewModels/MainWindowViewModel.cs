using _2048Game.Business.Services;
using _2048Game.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using _2048Game.Common.BusinessModels;
using _2048Game.Common.Services;
using _2048Game.Common.Definitions;

namespace _2048Game.WPF.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IGameService _service;

        private GameModel _model = new GameModel();
        private UserModel _userModel;

        public string UserName => _userModel?.Name ?? "<нет>";

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<GameTileViewModel> Tiles { get; set; }

        public MainWindowViewModel()
        {
            _service = new GameService();
            Tiles = new ObservableCollection<GameTileViewModel>();

            _service.BoardUpdated += UpdateTiles;
            _service.GameOver += OnGameOver;
        }
        public void SetUser(UserModel user)
        {
            _userModel = user;
            OnPropertyChanged(nameof(UserName));

            _service.StartGame(_userModel.Id);
        }

        private void UpdateTiles()
        {
            Tiles.Clear();

            bool has2048 = false;

            for (int r  = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    int value = _service.Board.Tiles[r, c];
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
            if (has2048)
            {
                OnVictory();
            }
        }

        public void HandKey(Key key)
        {
            bool moved = false;

            switch(key)
            {
                case Key.Up:
                    moved = _service.Move(MoveDirections.MoveDirection.Up);
                    break;
                case Key.Down:
                    moved = _service.Move(MoveDirections.MoveDirection.Down);
                    break;
                case Key.Left:
                    moved = _service.Move(MoveDirections.MoveDirection.Left);
                    break;
                case Key.Right:
                    moved = _service.Move(MoveDirections.MoveDirection.Right);
                    break;
            }
            if (moved)
            {
                var dto = _service.Board;
                _service.Save();
            }
        }
        private void OnVictory()
        {

            var res = MessageBox.Show($"Поздравляем! Вы собрали 2048! Повторить?", "Победа!", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (res == MessageBoxResult.Yes)
            {
                _service.Restart();
                _service.StartGame(_userModel.Id);
            }

        }

        private void OnGameOver()
        {
            var res = MessageBox.Show($"Игра окончена! Повторить?", "Стоп!", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (res == MessageBoxResult.Yes)
            {
                _service.Restart();
                _service.StartGame(_userModel.Id);
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
