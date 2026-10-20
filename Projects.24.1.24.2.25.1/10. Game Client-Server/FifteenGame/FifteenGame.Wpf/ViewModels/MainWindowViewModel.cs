using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Core.BusinessLogic.Models;
using FifteenGame.Core.BusinessLogic.Services;
using Ninject;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameService _gameService = NinjectKernel.Instance.Get<IGameService>();

        private UserModel _user;
        private GameSnapshot _snapshot;

        // Клетки хранятся по строкам: индекс = row * Size + column
        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }
        public ICommand MoveLeftCommand { get; }
        public ICommand MoveRightCommand { get; }
        public ICommand RestartCommand { get; }

        public string UserName => _user?.Name ?? "—";

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
                Cells.Add(new CellViewModel { Image = Textures.Floor });
            }

            MoveUpCommand = new RelayCommand(() => Move(-1, 0));
            MoveDownCommand = new RelayCommand(() => Move(1, 0));
            MoveLeftCommand = new RelayCommand(() => Move(0, -1));
            MoveRightCommand = new RelayCommand(() => Move(0, 1));
            RestartCommand = new RelayCommand(Restart);

            Refresh();
        }

        /// <summary>Вызывается после входа пользователя: загружает его сохранённую игру.</summary>
        public void CommitUser(UserModel user)
        {
            _user = user;
            OnPropertyChanged(nameof(UserName));
            Execute(() => _gameService.GetByUserId(_user.Id));
        }

        private void Move(int dRow, int dColumn)
        {
            if (_user == null) return;
            Execute(() => _gameService.MakeMove(_user.Id, dRow, dColumn));
        }

        private void Restart()
        {
            if (_user == null) return;
            Execute(() => _gameService.NewGame(_user.Id));
        }

        private void Execute(Func<GameSnapshot> action)
        {
            try
            {
                _snapshot = action();
                Refresh();
            }
            catch (Exception ex)
            {
                Hint = "Ошибка: " + (ex.InnerException?.Message ?? ex.Message);
            }
        }

        private void Refresh()
        {
            var s = _snapshot;

            for (int row = 0; row < GameService.Size; row++)
            {
                for (int column = 0; column < GameService.Size; column++)
                {
                    Cells[row * GameService.Size + column].Image = GetImage(s, row, column);
                }
            }

            int keysLeft = s?.KeysLeft ?? GameService.KeyCount;
            KeysText = $"Ключи: {GameService.KeyCount - keysLeft} / {GameService.KeyCount}";
            MovesText = $"Ходов: {s?.Moves ?? 0}";
            IsGameOver = s != null && s.State != GameState.Playing;

            if (s != null && s.State == GameState.Won)
            {
                ResultText = $"Победа!\nХодов: {s.Moves}";
            }
            else if (s != null && s.State == GameState.Lost)
            {
                ResultText = "Вас поймал монстр";
            }

            Hint = s == null ? "Войдите в систему, чтобы начать игру"
                : keysLeft > 0 ? "Соберите все ключи — выход откроется"
                : "Все ключи собраны — бегите к выходу!";
        }

        private static ImageSource GetImage(GameSnapshot s, int row, int column)
        {
            if (s == null)
            {
                return Textures.Floor;
            }

            if (row == s.PlayerRow && column == s.PlayerColumn)
            {
                return Textures.Player;
            }

            if (s.Monsters.Any(m => m.Row == row && m.Column == column))
            {
                return Textures.Monster;
            }

            switch (s.Cells[row][column])
            {
                case CellType.Wall: return Textures.Wall;
                case CellType.Key: return Textures.Key;
                case CellType.Exit: return s.KeysLeft == 0 ? Textures.ExitOpen : Textures.ExitClosed;
                default: return Textures.Floor;
            }
        }
    }
}
