using Pacman.Client.Infrastructure;
using Pacman.Common.Dtos;
using Pacman.Common.Enums;
using Pacman.Common.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.ComponentModel;

namespace Pacman.Client.ViewModels
{
    public class Ghost
    {
        public int Index { get; set; }
        public int SpawnIndex { get; set; }
        public CellType StoredCell { get; set; } = CellType.Empty;
        public int LastDirection { get; set; } = 0;
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IGameService _gameService;
        private readonly IUserService _userService;

        public ObservableCollection<CellVM> Cells { get; set; } = new ObservableCollection<CellVM>();
        private List<Ghost> _ghosts = new List<Ghost>();

        private int _mapWidth;
        private int _pacmanIndex;
        private int _pacmanSpawnIndex;

        private int _score;
        private int _lives;
        private int _userId;
        private string _username;
        private bool _isGameOver;
        private bool _justDied;

        private int _currentDirection = 0;
        private int _nextDirection = 0;

        private readonly DispatcherTimer _gameTimer;
        private readonly Random _rnd = new Random();

        public string Title => $"PAC-MAN | {_username} | Score: {_score} | Lives: {_lives}";

        public int MapColumns
        {
            get => _mapWidth;
            set { _mapWidth = value; OnPropertyChanged(nameof(MapColumns)); }
        }

        public MainViewModel()
        {
            _gameService = NinjectKernel.Instance.Get<IGameService>();
            _userService = NinjectKernel.Instance.Get<IUserService>();

            _gameTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
            _gameTimer.Tick += GameLoop;
        }

        public void StartGame(UserDto user)
        {
            _userId = user.Id;
            _username = user.Username;
            _score = 0;
            _lives = 3;
            _isGameOver = false;
            OnPropertyChanged(nameof(Title));

            LoadLevel(1);
        }

        private void LoadLevel(int levelId)
        {
            try
            {
                var map = _gameService.GetLevel(levelId);

                if (map == null || map.Cells == null) return;

                MapColumns = map.Width;
                Cells.Clear();
                _ghosts.Clear();

                for (int i = 0; i < map.Cells.Length; i++)
                {
                    var type = map.Cells[i];
                    if (type == CellType.Pacman)
                    {
                        _pacmanIndex = i;
                        _pacmanSpawnIndex = i;
                    }
                    else if (type == CellType.Ghost)
                    {
                        _ghosts.Add(new Ghost { Index = i, SpawnIndex = i, StoredCell = CellType.Empty });
                    }
                    Cells.Add(new CellVM { Type = type });
                }

                _currentDirection = 0;
                _nextDirection = 0;
                _gameTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки уровня: " + ex.Message);
            }
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (_isGameOver) return;
            _justDied = false;

            if (_nextDirection != 0 && CanMove(_pacmanIndex, _nextDirection))
            {
                _currentDirection = _nextDirection;
                _nextDirection = 0;
            }

            if (_currentDirection != 0 && CanMove(_pacmanIndex, _currentDirection))
            {
                MovePacman();
            }

            if (_justDied || _isGameOver) return;

            MoveGhosts();

            if (_justDied || _isGameOver) return;
        }


        private void MovePacman()
        {
            int next = _pacmanIndex + _currentDirection;
            var target = Cells[next];

            if (target.Type == CellType.Ghost)
            {
                HandleDeath();
                return;
            }

            if (target.Type == CellType.Coin)
            {
                _score += 10;
                OnPropertyChanged(nameof(Title));
            }

            Cells[_pacmanIndex].Type = CellType.Empty;
            _pacmanIndex = next;
            Cells[_pacmanIndex].Type = CellType.Pacman;

            CheckWinCondition();
        }

        private void CheckWinCondition()
        {
            bool hasCoinsOnField = Cells.Any(c => c.Type == CellType.Coin);
            bool hasCoinsUnderGhosts = _ghosts.Any(g => g.StoredCell == CellType.Coin);

            if (!hasCoinsOnField && !hasCoinsUnderGhosts)
            {
                GameWon();
            }
        }

        private void GameWon()
        {
            _isGameOver = true;
            _gameTimer.Stop();

            MessageBox.Show($"ПОБЕДА! Вы собрали всю еду.\nВаш счёт: {_score}", "Pac-Man Wins!");

            try
            {
                _userService.UpdateScore(new ScoreRequest { UserId = _userId, Score = _score });
            }
            catch { }

            _score += 1000;
            _isGameOver = false;
            OnPropertyChanged(nameof(Title));
            LoadLevel(1);
        }

        private void MoveGhosts()
        {
            foreach (var ghost in _ghosts)
            {
                if (_justDied) return;

                Cells[ghost.Index].Type = ghost.StoredCell;

                int move = GetGhostMove(ghost);
                int next = ghost.Index + move;

                if (next == _pacmanIndex)
                {
                    Cells[next].Type = CellType.Ghost;
                    HandleDeath();
                    return;
                }

                var nextType = Cells[next].Type;

                if (nextType == CellType.Ghost)
                    ghost.StoredCell = CellType.Empty;
                else
                    ghost.StoredCell = nextType;

                ghost.Index = next;
                Cells[ghost.Index].Type = CellType.Ghost;
            }
        }

        private void HandleDeath()
        {
            _gameTimer.Stop();
            _justDied = true;
            _lives--;
            OnPropertyChanged(nameof(Title));

            if (_lives > 0)
            {
                MessageBox.Show($"Вас поймали! Жизней осталось: {_lives}", "Ой!");
                ResetPositions();
                _gameTimer.Start();
            }
            else
            {
                GameOver();
            }
        }

        private void ResetPositions()
        {
            Cells[_pacmanIndex].Type = CellType.Empty;
            foreach (var ghost in _ghosts)
            {
                if (Cells[ghost.Index].Type != CellType.Pacman)
                    Cells[ghost.Index].Type = ghost.StoredCell;
            }

            _pacmanIndex = _pacmanSpawnIndex;
            Cells[_pacmanIndex].Type = CellType.Pacman;
            _currentDirection = 0;
            _nextDirection = 0;

            foreach (var ghost in _ghosts)
            {
                ghost.Index = ghost.SpawnIndex;
                ghost.StoredCell = CellType.Empty;
                ghost.LastDirection = 0;
                Cells[ghost.Index].Type = CellType.Ghost;
            }
        }

        private int GetGhostMove(Ghost ghost)
        {
            var directions = new[] { -1, 1, -_mapWidth, _mapWidth };
            var possibleMoves = directions.Where(d => CanMove(ghost.Index, d)).ToList();

            if (possibleMoves.Count == 0) return 0;

            if (possibleMoves.Count > 1 && ghost.LastDirection != 0)
            {
                if (_rnd.Next(100) < 90)
                    possibleMoves.Remove(-ghost.LastDirection);
            }


            int choice = possibleMoves[_rnd.Next(possibleMoves.Count)];
            ghost.LastDirection = choice;
            return choice;
        }

        private bool CanMove(int current, int offset)
        {
            int next = current + offset;
            if (next < 0 || next >= Cells.Count) return false;
            if (Cells[next].Type == CellType.Wall) return false;

            if (offset == 1 && next % _mapWidth == 0) return false;
            if (offset == -1 && current % _mapWidth == 0) return false;

            return true;
        }

        private void GameOver()
        {
            _isGameOver = true;
            MessageBox.Show($"GAME OVER! Ваш счет: {_score}", "Конец игры");

            try
            {
                _userService.UpdateScore(new ScoreRequest { UserId = _userId, Score = _score });
            }
            catch { }

            _score = 0;
            _lives = 3;
            _isGameOver = false;
            OnPropertyChanged(nameof(Title));
            LoadLevel(1);
        }

        public void HandleInput(Key key)
        {
            switch (key)
            {
                case Key.Up: _nextDirection = -_mapWidth; break;
                case Key.Down: _nextDirection = _mapWidth; break;
                case Key.Left: _nextDirection = -1; break;
                case Key.Right: _nextDirection = 1; break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}

