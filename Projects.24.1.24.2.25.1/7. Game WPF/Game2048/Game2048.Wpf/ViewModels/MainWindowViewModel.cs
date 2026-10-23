using System.Collections.ObjectModel;
using System.Windows.Input;
using Game2048.Business;

namespace Game2048.Wpf.ViewModels
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
            for (int i = 0; i < GameModel.Size * GameModel.Size; i++)
                Cells.Add(new CellViewModel());

            NewGameCommand = new RelayCommand(p => NewGame());
            NewGame();
        }

        public int Score
        {
            get { return _game == null ? 0 : _game.Score; }
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
            Status = "Игра идёт";
            Refresh();
        }

        public void Move(MoveDirection direction)
        {
            if (_game == null) return;
            if (_gameService.IsWon(_game) || _gameService.IsGameOver(_game)) return;

            bool changed = _gameService.MakeMove(_game, direction);
            if (!changed) return;

            Refresh();

            if (_gameService.IsWon(_game))
                Status = "Победа! Собрана плитка 2048";
            else if (_gameService.IsGameOver(_game))
                Status = "Игра окончена — ходов больше нет";
        }

        private void Refresh()
        {
            int n = GameModel.Size;
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    Cells[r * n + c].Value = _game.Field[r, c];

            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(MoveCount));
        }
    }
}
