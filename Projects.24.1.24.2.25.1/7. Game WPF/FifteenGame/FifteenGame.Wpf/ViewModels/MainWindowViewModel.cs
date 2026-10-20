using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FifteenGame.Core.BusinessLogic.Models;
using FifteenGame.Core.BusinessLogic.Services;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly GameService _game = new GameService();

        // Клетки хранятся по строкам: индекс = row * Size + column
        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }
        public ICommand MoveLeftCommand { get; }
        public ICommand MoveRightCommand { get; }
        public ICommand RestartCommand { get; }

        private string _keysText;
        public string KeysText
        {
            get => _keysText;
            private set { _keysText = value; OnPropertyChanged(nameof(KeysText)); }
        }

        private string _movesText;
        public string MovesText
        {
            get => _movesText;
            private set { _movesText = value; OnPropertyChanged(nameof(MovesText)); }
        }

        private string _hint;
        public string Hint
        {
            get => _hint;
            private set { _hint = value; OnPropertyChanged(nameof(Hint)); }
        }

        private bool _isGameOver;
        public bool IsGameOver
        {
            get => _isGameOver;
            private set { _isGameOver = value; OnPropertyChanged(nameof(IsGameOver)); }
        }

        private string _resultText;
        public string ResultText
        {
            get => _resultText;
            private set { _resultText = value; OnPropertyChanged(nameof(ResultText)); }
        }

        public MainWindowViewModel()
        {
            for (int i = 0; i < GameService.Size * GameService.Size; i++)
            {
                Cells.Add(new CellViewModel());
            }

            MoveUpCommand = new RelayCommand(() => Move(-1, 0));
            MoveDownCommand = new RelayCommand(() => Move(1, 0));
            MoveLeftCommand = new RelayCommand(() => Move(0, -1));
            MoveRightCommand = new RelayCommand(() => Move(0, 1));
            RestartCommand = new RelayCommand(() => { _game.NewGame(); Refresh(); });

            Refresh();
        }

        private void Move(int dRow, int dColumn)
        {
            _game.TryMove(dRow, dColumn);
            Refresh();
        }

        private void Refresh()
        {
            for (int row = 0; row < GameService.Size; row++)
            {
                for (int column = 0; column < GameService.Size; column++)
                {
                    var cell = Cells[row * GameService.Size + column];
                    DrawCell(cell, row, column);
                }
            }

            KeysText = $"Ключи: {GameService.KeyCount - _game.KeysLeft} / {GameService.KeyCount}";
            MovesText = $"Ходов: {_game.Moves}";
            IsGameOver = _game.State != GameState.Playing;

            switch (_game.State)
            {
                case GameState.Won:
                    ResultText = $"Победа!\nХодов: {_game.Moves}";
                    break;
                case GameState.Lost:
                    ResultText = "Вас поймал монстр";
                    break;
            }

            Hint = _game.KeysLeft > 0
                ? "Соберите все ключи — выход откроется"
                : "Все ключи собраны — бегите к выходу!";
        }

        private void DrawCell(CellViewModel vm, int row, int column)
        {
            if (row == _game.PlayerRow && column == _game.PlayerColumn)
            {
                vm.Image = Textures.Player;
                return;
            }

            if (_game.Monsters.Any(m => m.Row == row && m.Column == column))
            {
                vm.Image = Textures.Monster;
                return;
            }

            switch (_game.Cells[row, column])
            {
                case CellType.Wall:
                    vm.Image = Textures.Wall;
                    break;
                case CellType.Key:
                    vm.Image = Textures.Key;
                    break;
                case CellType.Exit:
                    vm.Image = _game.KeysLeft == 0 ? Textures.ExitOpen : Textures.ExitClosed;
                    break;
                default:
                    vm.Image = Textures.Floor;
                    break;
            }
        }
    }
}
