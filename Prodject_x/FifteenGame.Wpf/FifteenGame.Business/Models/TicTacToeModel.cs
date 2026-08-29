using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FifteenGame.Business.Models
{
    public enum CellState
    {
        Empty,
        X,
        O
    }

    public class TicTacToeModel : INotifyPropertyChanged
    {
        private CellState[] _board;
        private CellState _currentPlayer;
        private bool _gameOver;
        private CellState _winner;

        public event PropertyChangedEventHandler PropertyChanged;

        public TicTacToeModel()
        {
            Reset();
        }

        public CellState[] Board
        {
            get { return _board; }
            private set
            {
                _board = value;
                OnPropertyChanged();
            }
        }

        public CellState CurrentPlayer
        {
            get { return _currentPlayer; }
            private set
            {
                _currentPlayer = value;
                OnPropertyChanged();
            }
        }

        public bool GameOver
        {
            get { return _gameOver; }
            private set
            {
                _gameOver = value;
                OnPropertyChanged();
            }
        }

        public CellState Winner
        {
            get { return _winner; }
            private set
            {
                _winner = value;
                OnPropertyChanged();
            }
        }

        public void Reset()
        {
            Board = new CellState[9];
            CurrentPlayer = CellState.X;
            GameOver = false;
            Winner = CellState.Empty;
        }

        public bool MakeMove(int position)
        {
            if (position < 0 || position > 8 || Board[position] != CellState.Empty || GameOver)
                return false;

            var newBoard = (CellState[])Board.Clone();
            newBoard[position] = CurrentPlayer;
            Board = newBoard;

            if (CheckForWinner())
            {
                GameOver = true;
                Winner = CurrentPlayer;
            }
            else if (IsBoardFull())
            {
                GameOver = true;
            }
            else
            {
                CurrentPlayer = CurrentPlayer == CellState.X ? CellState.O : CellState.X;
            }

            return true;
        }

        private bool CheckForWinner()
        {
            // Проверяем горизонтальные линии
            for (int i = 0; i < 3; i++)
            {
                if (Board[i * 3] != CellState.Empty &&
                    Board[i * 3] == Board[i * 3 + 1] && 
                    Board[i * 3] == Board[i * 3 + 2])
                {
                    return true;
                }
            }

            // Проверяем вертикальные линии
            for (int i = 0; i < 3; i++)
            {
                if (Board[i] != CellState.Empty &&
                    Board[i] == Board[i + 3] && 
                    Board[i] == Board[i + 6])
                {
                    return true;
                }
            }

            // Проверяем диагонали
            if (Board[0] != CellState.Empty && 
                Board[0] == Board[4] && 
                Board[0] == Board[8])
            {
                return true;
            }

            if (Board[2] != CellState.Empty && 
                Board[2] == Board[4] && 
                Board[2] == Board[6])
            {
                return true;
            }

            return false;
        }

        private bool IsBoardFull()
        {
            foreach (var cell in Board)
            {
                if (cell == CellState.Empty)
                    return false;
            }
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 