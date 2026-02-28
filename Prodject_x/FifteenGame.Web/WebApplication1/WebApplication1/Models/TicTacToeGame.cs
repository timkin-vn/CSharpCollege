using System;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class TicTacToeGame
    {
        private char[,] _board = new char[3, 3];
        
        [JsonIgnore]
        public char[,] Board 
        { 
            get => _board; 
            private set => _board = value; 
        }
        
        // Это свойство будет использоваться для сериализации в JSON
        public char[][] BoardArray 
        { 
            get
            {
                char[][] result = new char[3][];
                for (int i = 0; i < 3; i++)
                {
                    result[i] = new char[3];
                    for (int j = 0; j < 3; j++)
                    {
                        result[i][j] = _board[i, j];
                    }
                }
                return result;
            }
        }
        
        public char CurrentPlayer { get; private set; } = 'X';
        public string Status { get; private set; } = "В процессе";
        public bool IsGameOver { get; private set; } = false;

        public TicTacToeGame()
        {
            // Инициализируем доску пустыми клетками
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _board[i, j] = ' ';
                }
            }
        }

        public bool MakeMove(int row, int col)
        {
            // Проверяем, что клетка пуста и игра не окончена
            if (IsGameOver || row < 0 || row >= 3 || col < 0 || col >= 3 || _board[row, col] != ' ')
            {
                return false;
            }

            // Делаем ход
            _board[row, col] = CurrentPlayer;

            // Проверяем, есть ли победитель
            if (CheckWin())
            {
                Status = $"Игрок {CurrentPlayer} выиграл!";
                IsGameOver = true;
                return true;
            }

            // Проверяем на ничью
            if (CheckDraw())
            {
                Status = "Ничья!";
                IsGameOver = true;
                return true;
            }

            // Меняем игрока
            CurrentPlayer = CurrentPlayer == 'X' ? 'O' : 'X';
            Status = $"Ход игрока {CurrentPlayer}";
            return true;
        }

        public void Reset()
        {
            // Сбрасываем игру
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _board[i, j] = ' ';
                }
            }
            CurrentPlayer = 'X';
            Status = "В процессе";
            IsGameOver = false;
        }

        private bool CheckWin()
        {
            // Проверка строк
            for (int i = 0; i < 3; i++)
            {
                if (_board[i, 0] != ' ' && _board[i, 0] == _board[i, 1] && _board[i, 1] == _board[i, 2])
                {
                    return true;
                }
            }

            // Проверка столбцов
            for (int i = 0; i < 3; i++)
            {
                if (_board[0, i] != ' ' && _board[0, i] == _board[1, i] && _board[1, i] == _board[2, i])
                {
                    return true;
                }
            }

            // Проверка диагоналей
            if (_board[0, 0] != ' ' && _board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2])
            {
                return true;
            }

            if (_board[0, 2] != ' ' && _board[0, 2] == _board[1, 1] && _board[1, 1] == _board[2, 0])
            {
                return true;
            }

            return false;
        }

        private bool CheckDraw()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_board[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
} 