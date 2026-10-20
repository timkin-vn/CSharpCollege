using FifteenGame.Common.Contracts;
using FifteenGame.Common.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Ninject;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameService _gameService = FifteenGame.Common.Infrastructure.NinjectKernel.Instance.Get<IGameService>();
        private GameModel _currentGame;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        private string _statusText = "Ваш ход (X)";
        public string StatusText { get => _statusText; set { _statusText = value; OnPropertyChanged(nameof(StatusText)); } }

        private int _playerScore;
        public int PlayerScore { get => _playerScore; set { _playerScore = value; OnPropertyChanged(nameof(PlayerScore)); } }

        private int _computerScore;
        public int ComputerScore { get => _computerScore; set { _computerScore = value; OnPropertyChanged(nameof(ComputerScore)); } }

        public ICommand NewGameCommand { get; }

        public MainWindowViewModel()
        {
            NewGameCommand = new RelayCommand(ResetGame);
            Initialize();
        }

        public void Initialize()
        {
            _currentGame = _gameService.GetOrCreateGame(1); // ID пользователя 1

            Cells.Clear();
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    int localRow = r, localCol = c;
                    Cells.Add(new CellViewModel
                    {
                        Row = localRow,
                        Column = localCol,
                        ClickCommand = new RelayCommand(() => PlayerMove(localRow, localCol))
                    });
                }
            }
            UpdateView();
        }

        private void PlayerMove(int r, int c)
        {
            if (_currentGame.Result != GameResult.None) return;

            var updated = _gameService.MakeMove(_currentGame.Id, r, c);
            if (updated != null)
            {
                _currentGame = updated;
                UpdateView();
            }
        }

        private void ResetGame()
        {
            _currentGame = _gameService.GetOrCreateGame(1);
            UpdateView();
        }

        private void UpdateView()
        {
            char[] board = _currentGame.BoardState.ToCharArray();
            var winningIndices = new System.Collections.Generic.HashSet<int>();

            if (_currentGame.Result == GameResult.PlayerWin || _currentGame.Result == GameResult.ComputerWin)
            {
                char winner = _currentGame.Result == GameResult.PlayerWin ? 'X' : 'O';
                int[][] wins = new int[][]
                {
                    new[] {0,1,2}, new[] {3,4,5}, new[] {6,7,8},
                    new[] {0,3,6}, new[] {1,4,7}, new[] {2,5,8},
                    new[] {0,4,8}, new[] {2,4,6}
                };

                foreach (var w in wins)
                {
                    if (board[w[0]] == winner && board[w[1]] == winner && board[w[2]] == winner)
                    {
                        winningIndices.Add(w[0]); winningIndices.Add(w[1]); winningIndices.Add(w[2]);
                        break;
                    }
                }
            }

            foreach (var cell in Cells)
            {
                int index = cell.Row * 3 + cell.Column;
                cell.Text = board[index] == ' ' ? "" : board[index].ToString();
                cell.IsWinningCell = winningIndices.Contains(index);
            }

            PlayerScore = _currentGame.PlayerScore;
            ComputerScore = _currentGame.ComputerScore;

            if (_currentGame.Result == GameResult.PlayerWin) StatusText = "Вы победили";
            else if (_currentGame.Result == GameResult.ComputerWin) StatusText = "Компьютер победил";
            else if (_currentGame.Result == GameResult.Draw) StatusText = "Ничья";
            else StatusText = "Ваш ход";
        }
    }
}