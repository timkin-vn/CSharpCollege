using FifteenGame.BusinessProxy.Services;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Dto; 
using FifteenGame.Common.Services;
using FifteenGame.Wpf.Infrastructure;
using Ninject;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IGameService _gameService;
        private readonly IUserService _userService;

        private int _gameId;
        private int _currentUserId;

        public ObservableCollection<CellVM> PlayerCells { get; set; }
        public ObservableCollection<CellVM> ComputerCells { get; set; }

        
        private readonly DispatcherTimer _timer;
        private TimeSpan _elapsed;
        public string ElapsedTime => $"{_elapsed.Minutes:D2}:{_elapsed.Seconds:D2}";

        
        private string _playerName;
        public string PlayerName
        {
            get => _playerName;
            set { _playerName = value; OnPropertyChanged(nameof(PlayerName)); }
        }

        private string _bestTime = "--:--";
        public string BestTime
        {
            get => _bestTime;
            set { _bestTime = value; OnPropertyChanged(nameof(BestTime)); }
        }

        public MainWindowViewModel()
        {
            
            _gameService = NinjectKernel.Instance.Get<IGameService>();
            _userService = NinjectKernel.Instance.Get<IUserService>();

            PlayerCells = new ObservableCollection<CellVM>();
            ComputerCells = new ObservableCollection<CellVM>();

            
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (s, e) =>
            {
                _elapsed = _elapsed.Add(TimeSpan.FromSeconds(1));
                OnPropertyChanged(nameof(ElapsedTime));
            };

            
            InitializeEmptyField(PlayerCells, true);
            InitializeEmptyField(ComputerCells, false);
        }

       
        public void SetUser(string username, double? bestTimeSeconds, bool hasSavedGame, int userId)
        {
            PlayerName = username;
            _currentUserId = userId;

            
            if (bestTimeSeconds.HasValue)
            {
                var ts = TimeSpan.FromSeconds(bestTimeSeconds.Value);
                BestTime = $"{ts.Minutes:D2}:{ts.Seconds:D2}";
            }
            else
            {
                BestTime = "--:--";
            }

            
            StartGameSequence();

            if (hasSavedGame)
            {
                MessageBox.Show($"Добро пожаловать назад, {username}!\nВаша игра была успешно загружена.",
                                "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        
        public void StartNewGame()
        {
            
            StartGameSequence();
        }

        
        private void StartGameSequence()
        {
            try
            {
              
                var reply = _gameService.StartNewGame(_currentUserId);

                _gameId = reply.GameId;

                
                _elapsed = TimeSpan.Zero;
                OnPropertyChanged(nameof(ElapsedTime));
                _timer.Start();

               
                InitializeEmptyField(PlayerCells, true);
                InitializeEmptyField(ComputerCells, false);

                
                if (reply.PlayerField != null)
                {
                    foreach (var cellDto in reply.PlayerField)
                    {
                        UpdateCell(PlayerCells, cellDto.X, cellDto.Y, cellDto.State);
                    }
                }

                
                if (reply.EnemyField != null)
                {
                    foreach (var cellDto in reply.EnemyField)
                    {
                        UpdateCell(ComputerCells, cellDto.X, cellDto.Y, cellDto.State);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка соединения с сервером при старте игры:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

       
        public void ShootAtEnemy(int x, int y)
        {
            try
            {
                var request = new MakeMoveRequest
                {
                    GameId = _gameId,
                    UserId = _currentUserId,
                    X = x,
                    Y = y
                };

               
                var reply = _gameService.MakeMove(request);

                
                foreach (var change in reply.ChangedEnemyCells)
                {
                    UpdateCell(ComputerCells, change.X, change.Y, change.State);
                }

               
                foreach (var change in reply.ChangedPlayerCells)
                {
                    UpdateCell(PlayerCells, change.X, change.Y, change.State);
                }

               
                if (reply.IsGameOver)
                {
                    _timer.Stop();

                    string msg = reply.Winner == "Player" ? "Поздравляем! Вы победили!" : "Вы проиграли. Попробуйте еще раз.";
                    MessageBoxImage icon = reply.Winner == "Player" ? MessageBoxImage.Information : MessageBoxImage.Exclamation;

                    MessageBox.Show(msg, "Игра окончена", MessageBoxButton.OK, icon);

                    if (reply.Winner == "Player")
                    {
                        UpdateBestTimeOnServer();
                    }

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка во время хода: " + ex.Message);
            }
        }

        private void UpdateBestTimeOnServer()
        {
            try
            {
                _userService.UpdateBestTime(new Common.Dto.UserDto
                {
                    Username = PlayerName,
                    BestTimeSeconds = _elapsed.TotalSeconds
                });

                
                BestTime = ElapsedTime;
            }
            catch
            {
                
            }
        }

        private void InitializeEmptyField(ObservableCollection<CellVM> collection, bool isMy)
        {
            collection.Clear();
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    collection.Add(new CellVM
                    {
                        Row = y,
                        Column = x,
                        IsMyField = isMy,
                        State = CellState.Empty
                    });
                }
            }
        }

        private void UpdateCell(ObservableCollection<CellVM> collection, int x, int y, CellState state)
        {
            
            int index = y * 10 + x;
            if (index >= 0 && index < collection.Count)
            {
                collection[index].State = state;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}