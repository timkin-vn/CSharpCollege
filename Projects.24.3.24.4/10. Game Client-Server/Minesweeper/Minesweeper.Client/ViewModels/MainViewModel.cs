using System.Collections.ObjectModel;
using System.Windows.Input;
using Minesweeper.Common;
using Minesweeper.Common.Interfaces;
using Minesweeper.Common.Models;

namespace Minesweeper.Client.ViewModels
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
            for (int r = 0; r < Constants.Size; r++)
                for (int c = 0; c < Constants.Size; c++)
                    Cells.Add(new CellViewModel(r, c, Reveal, Flag));

            NewGameCommand = new RelayCommand(p => NewGame());

            _game = _gameService.GetByUserId(_userId);
            Refresh();
            UpdateStatus();
        }

        public int MinesRemaining
        {
            get
            {
                if (_game == null) return Constants.MineCount;
                int flags = 0;
                int n = Constants.Size;
                for (int r = 0; r < n; r++)
                    for (int c = 0; c < n; c++)
                        if (_game.Field[r, c].IsFlagged) flags++;
                return Constants.MineCount - flags;
            }
        }

        public int MoveCount { get { return _game == null ? 0 : _game.MoveCount; } }
        public string Status { get { return _status; } private set { _status = value; OnPropertyChanged(); } }

        public void Reveal(int row, int col)
        {
            if (_game == null) return;
            if (_gameService.IsWon(_userId) || _gameService.IsGameOver(_userId)) return;

            _game = _gameService.Apply(_userId, row, col, CellAction.Reveal);
            Refresh();
            UpdateStatus();
        }

        public void Flag(int row, int col)
        {
            if (_game == null) return;
            if (_gameService.IsWon(_userId) || _gameService.IsGameOver(_userId)) return;

            _game = _gameService.Apply(_userId, row, col, CellAction.Flag);
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
            if (_gameService.IsWon(_userId))
                Status = "\u041F\u043E\u0431\u0435\u0434\u0430! \u0412\u0441\u0435 \u043C\u0438\u043D\u044B \u043E\u0431\u0435\u0437\u0432\u0440\u0435\u0436\u0435\u043D\u044B";
            else if (_gameService.IsGameOver(_userId))
                Status = "\u0412\u0437\u0440\u044B\u0432! \u0418\u0433\u0440\u0430 \u043E\u043A\u043E\u043D\u0447\u0435\u043D\u0430";
            else
                Status = "\u0418\u0433\u0440\u0430 \u0438\u0434\u0451\u0442";
        }

        private void Refresh()
        {
            int n = Constants.Size;
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    Cells[r * n + c].Update(_game.Field[r, c]);
            OnPropertyChanged(nameof(MinesRemaining));
            OnPropertyChanged(nameof(MoveCount));
        }
    }
}
