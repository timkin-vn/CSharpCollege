using System.Collections.ObjectModel;
using System.Windows.Input;
using Game2048.Common;
using Game2048.Common.Interfaces;
using Game2048.Common.Models;

namespace Game2048.Wpf.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        private readonly IGameService _gameService;
        private readonly int _userId;
        private GameModel _game;
        private string _status;

        public ObservableCollection<CellViewModel> Cells { get; private set; }
        public ICommand NewGameCommand { get; private set; }
        public string PlayerName { get; private set; }

        public MainViewModel(IGameService gameService, User user)
        {
            _gameService = gameService;
            _userId = user.Id;
            PlayerName = user.Name;

            Cells = new ObservableCollection<CellViewModel>();
            for (int i = 0; i < Constants.Size * Constants.Size; i++)
                Cells.Add(new CellViewModel());

            NewGameCommand = new RelayCommand(p => NewGame());

            _game = _gameService.GetByUserId(_userId);
            UpdateStatus();
            Refresh();
        }

        public int Score { get { return _game == null ? 0 : _game.Score; } }
        public int MoveCount { get { return _game == null ? 0 : _game.MoveCount; } }
        public string Status { get { return _status; } private set { _status = value; OnPropertyChanged(); } }

        public void Move(MoveDirection direction)
        {
            if (_game == null) return;
            if (_gameService.IsWon(_userId) || _gameService.IsGameOver(_userId)) return;

            _game = _gameService.MakeMove(_userId, direction);
            Refresh();
            UpdateStatus();
        }

        private void NewGame()
        {
            if (_game != null) _gameService.Remove(_game.Id);
            _game = _gameService.GetByUserId(_userId);
            Refresh();
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (_gameService.IsWon(_userId)) Status = "Победа! Собрана плитка 2048";
            else if (_gameService.IsGameOver(_userId)) Status = "Игра окончена — ходов больше нет";
            else Status = "Игра идёт";
        }

        private void Refresh()
        {
            int n = Constants.Size;
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    Cells[r * n + c].Value = _game.Field[r, c];
            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(MoveCount));
        }
    }
}
