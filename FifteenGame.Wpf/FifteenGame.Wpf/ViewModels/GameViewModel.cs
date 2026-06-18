using Pacman.Business;
using Pacman.Business.Models;
using Pacman.Business.Services;
using Pacman.Business.TicTacToe;
using Pacman.Business.Services; 
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Pacman.Business.Services.GameService _gameService = new Pacman.Business.Services.GameService();
        private readonly Timer _timer;

        // НОВЫЕ ПОЛЯ ДЛЯ ВНЕДРЕНИЯ ЗАВИСИМОСТЕЙ
        private readonly int _userId;
        private readonly GameDataService _dataService;

        public Pacman.Business.GameState State => _gameService.State;

        // Вычисляемое свойство для корректного отображения 147/147
        public string DotsDisplay => $"Точки: {State.CollectedDots}/{State.TotalDots}";

        // Команда для кнопки "Сохранить"
        public ICommand SaveCommand { get; }

        
        public GameViewModel(int userId, GameDataService dataService)
        {
            // Сохраняем переданные зависимости
            _userId = userId;
            _dataService = dataService;

            _timer = new Timer(50);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();

            // Инициализация команды сохранения с проверкой CanExecute
            SaveCommand = new FifteenGame.Wpf.ViewModels.RelayCommand(SaveGame, CanSaveGame);
        }

        // Добавленный метод проверки возможности выполнения команды
        private bool CanSaveGame()
        {
            // Разрешаем сохранение, только если ID пользователя действителен
            return _userId > 0;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            _gameService.Update();
            OnPropertyChanged(nameof(State));
            
            OnPropertyChanged(nameof(DotsDisplay));
        }

        public void SetDirection(Pacman.Business.Models.Direction direction)
        {
            if (State?.Player != null)
            {
                State.Player.Direction = direction;
            }
        }

        public void Restart()
        {
            _gameService.Restart();
            OnPropertyChanged(nameof(State));
            OnPropertyChanged(nameof(DotsDisplay)); // Обновляем отображение при перезапуске
        }

        
        private void SaveGame()
        {
            // Дополнительная проверка
            if (_userId <= 0 || _dataService == null)
            {
                Debug.WriteLine("SaveGame failed: User ID or Data Service is invalid.");
                return;
            }

            try
            {
                var stateToSave = GetGameStateData();

                
                _dataService.SaveGameState(_userId, stateToSave);

                Debug.WriteLine($"Команда сохранения вызвана. Успешно сохранено для User ID: {_userId}, Счет: {stateToSave.Score}");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Database Save Error from ViewModel: {ex.Message}");
                
            }
        }
        

        public Pacman.Business.Models.GameStateData GetGameStateData()
        {
            // Используем GameBoard для сериализации
            string boardStateString = string.Join(",", State.GameBoard.Cast<int>());

            int totalDotsToSave = State.TotalDots;

            // Если TotalDots = 0 (что не должно быть), пересчитываем
            if (totalDotsToSave == 0)
            {
                State.CountTotalDots();
                totalDotsToSave = State.TotalDots;
            }

            string ghostsPositionsString = string.Join(";", State.Ghosts.Select(g => $"{g.X},{g.Y},{(int)g.Color}"));

            var stateData = new GameStateData
            {
                Score = State.Score,
                Level = State.Level,
                Lives = State.Lives,
                PlayerX = State.Player.X,
                PlayerY = State.Player.Y,
                BoardState = boardStateString,
                GhostsPositions = ghostsPositionsString,
                CollectedDots = State.CollectedDots,
                TotalDots = totalDotsToSave
            };

            Debug.WriteLine($"GetGameStateData: Score {stateData.Score}, Dots {stateData.CollectedDots}/{stateData.TotalDots}");
            return stateData;
        }

        public void LoadState(Pacman.Business.Models.GameStateData data)
        {
            if (data == null || State == null || State.Player == null)
            {
                Debug.WriteLine("LoadState failed: Data or State is null.");
                return;
            }

            
            State.Score = data.Score;
            State.Level = data.Level;
            State.Lives = data.Lives;
            State.CollectedDots = data.CollectedDots;
            State.Player.X = data.PlayerX;
            State.Player.Y = data.PlayerY;

            // 3. Десериализация игрового поля
            if (!string.IsNullOrEmpty(data.BoardState))
            {
                var stringValues = data.BoardState.Split(',');

                if (stringValues.Length == GameState.GRID_HEIGHT * GameState.GRID_WIDTH)
                {
                    for (int y = 0; y < GameState.GRID_HEIGHT; y++)
                    {
                        for (int x = 0; x < GameState.GRID_WIDTH; x++)
                        {
                            if (int.TryParse(stringValues[y * GameState.GRID_WIDTH + x], out int cellValue))
                            {
                                State.GameBoard[y, x] = cellValue;
                            }
                        }
                    }
                }
            }

            
            State.CountTotalDots();

            // 4. Обновление призраков
            if (!string.IsNullOrEmpty(data.GhostsPositions) && State.Ghosts.Count > 0)
            {
                var ghostEntries = data.GhostsPositions.Split(';');

                for (int i = 0; i < ghostEntries.Length && i < State.Ghosts.Count; i++)
                {
                    var parts = ghostEntries[i].Split(',');
                    if (parts.Length == 3 && int.TryParse(parts[0], out int x) && int.TryParse(parts[1], out int y) && int.TryParse(parts[2], out int colorValue))
                    {
                        State.Ghosts[i].X = x;
                        State.Ghosts[i].Y = y;
                        State.Ghosts[i].Color = (ColorType)colorValue;
                    }
                }
            }

            
            OnPropertyChanged(nameof(State));
            OnPropertyChanged(nameof(DotsDisplay)); // Обновляем отображение точек после загрузки
            Debug.WriteLine($"LoadState finished. Current TotalDots: {State.TotalDots}");
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}