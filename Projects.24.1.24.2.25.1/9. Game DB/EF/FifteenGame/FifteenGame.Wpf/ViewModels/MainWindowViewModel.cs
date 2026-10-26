using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private int[,] _board = new int[3, 3];
        private int _currentPlayer = 1;
        private int _winner = 0;

        public ObservableCollection<CellViewModel> Cells { get; set; }
        public ICommand CellClickCommand { get; set; }
        public ICommand NewGameCommand { get; set; }

        private string _statusText;
        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            Cells = new ObservableCollection<CellViewModel>();
            for (int i = 0; i < 9; i++)
            {
                Cells.Add(new CellViewModel { Row = i / 3, Col = i % 3, Text = "" });
            }

            CellClickCommand = new RelayCommand(param => CellClick((CellViewModel)param));
            NewGameCommand = new RelayCommand(param => StartNewGame());

            StartNewGame();
        }

        private void CellClick(CellViewModel cell)
        {
            if (_winner != 0 || _board[cell.Row, cell.Col] != 0) return;

            _board[cell.Row, cell.Col] = _currentPlayer;
            cell.Text = _currentPlayer == 1 ? "X" : "O";

            CheckWinner();

            if (_winner == 0)
            {
                _currentPlayer = _currentPlayer == 1 ? 2 : 1;
                StatusText = _currentPlayer == 1 ? "Ход: X" : "Ход: O";
            }
            else
            {
                StatusText = _winner == 1 ? "Победили Крестики!" : "Победили Нолики!";
            }
        }

        private void StartNewGame()
        {
            _board = new int[3, 3];
            _currentPlayer = 1;
            _winner = 0;
            StatusText = "Ход: X";

            foreach (var cell in Cells)
            {
                cell.Text = "";
            }
        }

        private void CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_board[i, 0] != 0 && _board[i, 0] == _board[i, 1] && _board[i, 1] == _board[i, 2])
                    _winner = _board[i, 0];

                if (_board[0, i] != 0 && _board[0, i] == _board[1, i] && _board[1, i] == _board[2, i])
                    _winner = _board[0, i];
            }

            if (_board[0, 0] != 0 && _board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2])
                _winner = _board[0, 0];

            if (_board[0, 2] != 0 && _board[0, 2] == _board[1, 1] && _board[1, 1] == _board[2, 0])
                _winner = _board[0, 2];

            if (_winner != 0)
            {
                SaveGameResult();
            }
        }

        private void SaveGameResult()
        {
            try
            {
                
                using (var context = new FifteenGame.DataAccess.EF.DataContext.FifteenGameDataContext())
                {
                    
                    var newGame = new FifteenGame.DataAccess.EF.Entities.Game
                    {
                        UserId = 1,
                        MoveCount = 0,
                        Winner = _winner
                    };

                    
                    context.Games.Add(newGame);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка БД: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}