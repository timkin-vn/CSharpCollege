using TicTacToe.Business.Models;

namespace TicTacToe.Business.Services
{
    public class GameService
    {
        private readonly GameModel _model;
        private string _currentPlayer;

        public GameService(GameModel model)
        {
            _model = model;
            _currentPlayer = "X"; // Начинает игрок с крестиком
        }

        public void MakeMove(int row, int column)
        {
            if (string.IsNullOrEmpty(_model[row, column]) && !IsGameOver())
            {
                _model[row, column] = _currentPlayer;
                if (IsGameOver())
                {
                  
                }
                else if (IsDraw())
                {
                   
                }
                else
                {
                    _currentPlayer = _currentPlayer == "X" ? "O" : "X"; // Смена игрока
                }
            }
        }

        public bool IsGameOver()
        {
            return IsOver();
        }
        private bool IsOver()
        {

            for (int row = 0; row <GameModel.RowCount; row++)
            {
                if (_model[row, 0] == _model[row, 1] && _model[row, 1] == _model[row, 2] && !string.IsNullOrEmpty(_model[row, 0]))
                    return true;
            }

            for (int col = 0; col < GameModel.ColumnCount; col++)
            {
                if (_model[0, col] == _model[1, col] && _model[1, col] == _model[2, col] && !string.IsNullOrEmpty(_model[0, col]))
                    return true;
            }

            if (_model[0, 0] == _model[1, 1] && _model[1, 1] == _model[2, 2] && !string.IsNullOrEmpty(_model[0, 0]))
                return true;

            if (_model[0, 2] == _model[1, 1] && _model[1, 1] == _model[2, 0] && !string.IsNullOrEmpty(_model[0, 2]))
                return true;

            return false;
        }

        public bool IsDraw()
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int col = 0; col < GameModel.ColumnCount; col++)
                {
                    if (string.IsNullOrEmpty(_model[row, col]))
                        return false;
                }
            }
            return true;
        }

        public void Reset()
        {
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int col = 0; col < GameModel.ColumnCount; col++)
                {
                    _model[row, col] = string.Empty;
                }
            }
        }
        public void ResetGame()
        {
            Reset();
            _currentPlayer = "X"; 
        }
    }
}
