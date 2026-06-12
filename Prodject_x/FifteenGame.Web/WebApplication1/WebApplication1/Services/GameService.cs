using MinesweeperWeb.Models;
using System;

namespace MinesweeperWeb.Services
{
    public class GameService
    {
        private GameModel _gameModel;
        private readonly int _rows;
        private readonly int _columns;
        private readonly int _mines;
        private bool _isFirstMove;
        private bool _isGameOver;
        private bool _isWin;

        public GameService(int rows, int columns, int mines)
        {
            _rows = rows;
            _columns = columns;
            _mines = mines;
            _isFirstMove = true;
            StartNewGame();
        }

        public GameModel GameModel => _gameModel;
        public bool IsGameOver => _isGameOver;
        public bool IsWin => _isWin;

        public void StartNewGame()
        {
            _gameModel = new GameModel(_rows, _columns, _mines);
            _isFirstMove = true;
            _isGameOver = false;
            _isWin = false;
        }

        public void OpenCell(int row, int col, Action updateDisplay)
        {
            if (_isGameOver) return;

            if (_isFirstMove)
            {
                _gameModel.GenerateMines(row, col);
                _isFirstMove = false;
            }

            if (_gameModel.Field[row, col].IsMine)
            {
                _isGameOver = true;
                return;
            }

            _gameModel.OpenCell(row, col, (r, c) => OpenCell(r, c, updateDisplay), updateDisplay);
            CheckWinCondition();
        }

        public void FlagCell(int row, int col)
        {
            if (_isGameOver || _gameModel.Field[row, col].IsOpen) return;

            _gameModel.Field[row, col].IsFlagged = !_gameModel.Field[row, col].IsFlagged;
            CheckWinCondition();
        }

        private void CheckWinCondition()
        {
            int openedCells = 0;
            int flaggedMines = 0;

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    if (_gameModel.Field[i, j].IsOpen)
                        openedCells++;
                    if (_gameModel.Field[i, j].IsMine && _gameModel.Field[i, j].IsFlagged)
                        flaggedMines++;
                }
            }

            if (flaggedMines == _mines && openedCells == (_rows * _columns - _mines))
            {
                _isGameOver = true;
                _isWin = true;
            }
        }
    }
}