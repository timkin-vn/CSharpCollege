using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;
using FifteenGame.Business.Models;
using FifteenGame.Business.Services;

namespace FifteenGame.Wpf.ViewModels
{
    public class TicTacToeViewModel : INotifyPropertyChanged
    {
        private readonly TicTacToeService _gameService;
        private string _statusText;
        private bool _isGameActive;

        public event PropertyChangedEventHandler PropertyChanged;

        public TicTacToeViewModel()
        {
            _gameService = new TicTacToeService();
            Cells = new ObservableCollection<TicTacToeCellViewModel>(
                Enumerable.Range(0, 9).Select(i => new TicTacToeCellViewModel(i, MakeMove)));
            
            ResetCommand = new RelayCommand(Reset);
            IsGameActive = true;
            UpdateBoard();
            UpdateStatus();
        }

        public ObservableCollection<TicTacToeCellViewModel> Cells { get; }

        public string StatusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                OnPropertyChanged();
            }
        }

        public bool IsGameActive
        {
            get { return _isGameActive; }
            set
            {
                _isGameActive = value;
                OnPropertyChanged();
            }
        }

        public ICommand ResetCommand { get; }

        private void MakeMove(int position)
        {
            if (_gameService.MakeMove(position))
            {
                UpdateBoard();
                UpdateStatus();
                
                // Если игрок сделал ход и игра продолжается, проверяем ход компьютера
                if (!_gameService.Model.GameOver && _gameService.Model.CurrentPlayer == CellState.O)
                {
                    MakeComputerMove();
                }
            }
        }

        private void MakeComputerMove()
        {
            // Используем небольшую задержку для компьютерного хода, чтобы игра выглядела естественнее
            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                ComputeNextMove();
                UpdateBoard();
                UpdateStatus();
            };
            timer.Start();
        }

        private void ComputeNextMove()
        {
            var board = _gameService.Model.Board;
            
            // Простая логика для компьютерного хода:
            // 1. Если можно выиграть за один ход, делаем этот ход
            // 2. Если игрок может выиграть следующим ходом, блокируем его
            // 3. Выбираем центр
            // 4. Выбираем углы
            // 5. Выбираем любую свободную клетку
            
            // Проверяем, можем ли мы выиграть за следующий ход
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == CellState.Empty)
                {
                    // Симулируем ход
                    var testBoard = (CellState[])board.Clone();
                    testBoard[i] = CellState.O;
                    
                    // Проверяем, выигрывает ли этот ход
                    if (CheckWin(testBoard, CellState.O))
                    {
                        _gameService.MakeMove(i);
                        return;
                    }
                }
            }
            
            // Проверяем, нужно ли блокировать ход игрока
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == CellState.Empty)
                {
                    // Симулируем ход игрока
                    var testBoard = (CellState[])board.Clone();
                    testBoard[i] = CellState.X;
                    
                    // Проверяем, выигрывает ли этот ход у игрока
                    if (CheckWin(testBoard, CellState.X))
                    {
                        _gameService.MakeMove(i);
                        return;
                    }
                }
            }
            
            // Выбираем центр
            if (board[4] == CellState.Empty)
            {
                _gameService.MakeMove(4);
                return;
            }
            
            // Выбираем углы
            int[] corners = { 0, 2, 6, 8 };
            foreach (int corner in corners)
            {
                if (board[corner] == CellState.Empty)
                {
                    _gameService.MakeMove(corner);
                    return;
                }
            }
            
            // Выбираем любую свободную клетку
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == CellState.Empty)
                {
                    _gameService.MakeMove(i);
                    return;
                }
            }
        }

        private bool CheckWin(CellState[] board, CellState player)
        {
            // Проверяем горизонтали
            for (int i = 0; i < 3; i++)
            {
                if (board[i * 3] == player && board[i * 3 + 1] == player && board[i * 3 + 2] == player)
                    return true;
            }
            
            // Проверяем вертикали
            for (int i = 0; i < 3; i++)
            {
                if (board[i] == player && board[i + 3] == player && board[i + 6] == player)
                    return true;
            }
            
            // Проверяем диагонали
            if (board[0] == player && board[4] == player && board[8] == player)
                return true;
            
            if (board[2] == player && board[4] == player && board[6] == player)
                return true;
            
            return false;
        }

        private void Reset()
        {
            _gameService.Reset();
            IsGameActive = true;
            UpdateBoard();
            UpdateStatus();
        }

        private void UpdateBoard()
        {
            var board = _gameService.Model.Board;
            for (int i = 0; i < 9; i++)
            {
                Cells[i].State = board[i];
            }
        }

        private void UpdateStatus()
        {
            var model = _gameService.Model;
            if (model.GameOver)
            {
                IsGameActive = false;
                if (model.Winner != CellState.Empty)
                {
                    if (model.Winner == CellState.X)
                    {
                        StatusText = "Вы победили!";
                    }
                    else
                    {
                        StatusText = "Компьютер победил!";
                    }
                }
                else
                {
                    StatusText = "Ничья!";
                }
            }
            else
            {
                if (model.CurrentPlayer == CellState.X)
                {
                    StatusText = "Ваш ход";
                }
                else
                {
                    StatusText = "Ход компьютера...";
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 