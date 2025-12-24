using FifteenGame.Common.BusinessModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace FifteenGame.Wpf.ViewModels
{
    public sealed class TicTacToeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        private UserModel _user;
        private string _currentPlayer = "X";
        private string _statusText = "Первым ходит X";
        private bool _isGameLocked;

        public string UserName => _user?.Name ?? "<нет>";

        public string CurrentPlayer
        {
            get => _currentPlayer;
            private set
            {
                if (_currentPlayer == value) return;
                _currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }

        public string StatusText
        {
            get => _statusText;
            private set
            {
                if (_statusText == value) return;
                _statusText = value;
                OnPropertyChanged(nameof(StatusText));
            }
        }

        public TicTacToeViewModel()
        {
            Reset();
        }

        public void SetUser(UserModel user)
        {
            _user = user;
            OnPropertyChanged(nameof(UserName));
        }

        public MoveResult MakeMove(int[] colRow)
        {
            if (_isGameLocked) return MoveResult.NotFinished;
            if (colRow == null || colRow.Length < 2) return MoveResult.NotFinished;

            var r = colRow[0];
            var c = colRow[1];
            var cell = Cells.FirstOrDefault(x => x.Row == r && x.Column == c);
            if (cell == null || !cell.IsEnabled) return MoveResult.NotFinished;

            cell.Symbol = CurrentPlayer;
            cell.IsEnabled = false;

            var winLine = FindWinningLine();
            if (winLine != null)
            {
                foreach (var idx in winLine)
                    Cells[idx].IsWinningCell = true;

                _isGameLocked = true;
                StatusText = $"Победа: {CurrentPlayer}";
                return new MoveResult(isFinished: true, winnerSymbol: CurrentPlayer);
            }

            if (Cells.All(x => !string.IsNullOrEmpty(x.Symbol)))
            {
                _isGameLocked = true;
                StatusText = "Ничья";
                return new MoveResult(isFinished: true, winnerSymbol: null);
            }

            CurrentPlayer = CurrentPlayer == "X" ? "O" : "X";
            StatusText = $"Ход игрока {CurrentPlayer}";
            return MoveResult.NotFinished;
        }

        public void Reset()
        {
            Cells.Clear();
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = col,
                        ColumnRow = new[] { row, col },
                        Symbol = string.Empty,
                        IsEnabled = true,
                        IsWinningCell = false
                    });
                }
            }

            _isGameLocked = false;
            CurrentPlayer = "X";
            StatusText = "Первым ходит X";
        }

        private int[] FindWinningLine()
        {
            int[][] lines =
            {
                new[] {0,1,2}, new[] {3,4,5}, new[] {6,7,8},
                new[] {0,3,6}, new[] {1,4,7}, new[] {2,5,8},
                new[] {0,4,8}, new[] {2,4,6}
            };

            foreach (var line in lines)
            {
                var a = Cells[line[0]].Symbol;
                if (string.IsNullOrEmpty(a)) continue;
                if (Cells[line[1]].Symbol == a && Cells[line[2]].Symbol == a)
                    return line;
            }
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public readonly struct MoveResult
    {
        public bool IsFinished { get; }
        public string WinnerSymbol { get; }

        public MoveResult(bool isFinished, string winnerSymbol)
        {
            IsFinished = isFinished;
            WinnerSymbol = winnerSymbol;
        }

        public static MoveResult NotFinished => new MoveResult(false, null);
    }
}
