using System.Collections.ObjectModel;
using System.Windows.Input;
using Minesweeper.Business;

namespace Minesweeper.Wpf.ViewModels
{

    public class MainWindowViewModel : ViewModelBase
    {
        private readonly GameService _gameService = new GameService();
        private GameModel _game;
        private string _status;

        public ObservableCollection<CellViewModel> Cells { get; private set; }
        public ICommand NewGameCommand { get; private set; }

        public MainWindowViewModel()
        {
            Cells = new ObservableCollection<CellViewModel>();
            for (int r = 0; r < GameModel.Size; r++)
                for (int c = 0; c < GameModel.Size; c++)
                    Cells.Add(new CellViewModel(r, c, Reveal, Flag));

            NewGameCommand = new RelayCommand(p => NewGame());
            NewGame();
        }

        public int MinesRemaining
        {
            get { return _game == null ? GameModel.MineCount : _gameService.MinesRemaining(_game); }
        }

        public int MoveCount
        {
            get { return _game == null ? 0 : _game.MoveCount; }
        }

        public string Status
        {
            get { return _status; }
            private set { _status = value; OnPropertyChanged(); }
        }

        public void NewGame()
        {
            _game = _gameService.Initialize();
            Status = "\u0418\u0433\u0440\u0430 \u0438\u0434\u0451\u0442";
            Refresh();
        }

        public void Reveal(int row, int col)
        {
            if (_game == null) return;
            if (!_gameService.Reveal(_game, row, col)) return;
            Refresh();
            UpdateStatus();
        }

        public void Flag(int row, int col)
        {
            if (_game == null) return;
            if (!_gameService.ToggleFlag(_game, row, col)) return;
            Refresh();
        }

        private void UpdateStatus()
        {
            if (_gameService.IsWon(_game))
                Status = "\u041F\u043E\u0431\u0435\u0434\u0430! \u0412\u0441\u0435 \u043C\u0438\u043D\u044B \u043E\u0431\u0435\u0437\u0432\u0440\u0435\u0436\u0435\u043D\u044B";
            else if (_gameService.IsGameOver(_game))
                Status = "\u0412\u0437\u0440\u044B\u0432! \u0418\u0433\u0440\u0430 \u043E\u043A\u043E\u043D\u0447\u0435\u043D\u0430";
            else
                Status = "\u0418\u0433\u0440\u0430 \u0438\u0434\u0451\u0442";
        }

        private void Refresh()
        {
            int n = GameModel.Size;
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    Cells[r * n + c].Update(_game.Field[r, c]);

            OnPropertyChanged(nameof(MinesRemaining));
            OnPropertyChanged(nameof(MoveCount));
        }
    }
}
