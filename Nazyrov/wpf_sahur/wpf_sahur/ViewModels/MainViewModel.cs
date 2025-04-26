using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wpf_sahur_business.Models;

namespace wpf_sahur.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private GameBoard _gameBoard;
        private string _statusMessage;
        private bool _isGameOver;

        public MainViewModel()
        {
            _gameBoard = new GameBoard();
            CellClickCommand = new RelayCommand(OnCellClick);
            ResetGameCommand = new RelayCommand(ResetGame);
            StatusMessage = "Ход игрока X";
        }

        public ICommand CellClickCommand { get; }
        public ICommand ResetGameCommand { get; }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public string[] Board => _gameBoard.Board;

        private void OnCellClick(object parameter)
        {
            if (_isGameOver)
                return;

            if (parameter == null)
                return;

            if (!int.TryParse(parameter.ToString(), out int index))
                return;

            if (_gameBoard.MakeMove(index))
            {
                OnPropertyChanged(nameof(Board));

                if (_gameBoard.CheckWinner(out string winner))
                {
                    StatusMessage = $"Победил игрок {winner}!";
                    _isGameOver = true;
                }
                else if (_gameBoard.IsBoardFull())
                {
                    StatusMessage = "Ничья!";
                    _isGameOver = true;
                }
                else
                {
                    StatusMessage = $"Ход игрока {(_gameBoard.IsXTurn ? "X" : "O")}";
                }
            }
        }

        private void ResetGame(object parameter)
        {
            _gameBoard.Reset();
            _isGameOver = false;
            StatusMessage = "Ход игрока X";
            OnPropertyChanged(nameof(Board));
        }
    }
}
