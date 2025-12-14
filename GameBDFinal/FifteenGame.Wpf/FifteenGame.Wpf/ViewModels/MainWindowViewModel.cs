using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Data.Repositories; // <-- Подключили репозиторий
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace FifteenGame.Wpf.ViewModels
{
    // --- ViewModel для одной клетки ---
    public class CellVM : INotifyPropertyChanged
    {
        public Cell Model { get; private set; }
        private readonly bool _showShips;

        public int X { get { return Model.X; } }
        public int Y { get { return Model.Y; } }

        public Brush Background
        {
            get
            {
                if (Model.State == CellState.Sunk) return Brushes.DarkRed;
                if (Model.State == CellState.Hit) return Brushes.OrangeRed;
                if (Model.State == CellState.Miss) return Brushes.LightSteelBlue;
                if (Model.State == CellState.Ship && _showShips) return Brushes.Gray;
                return Brushes.CornflowerBlue;
            }
        }

        public string Symbol
        {
            get
            {
                if (Model.State == CellState.Miss) return "•";
                if (Model.State == CellState.Hit) return "×";
                if (Model.State == CellState.Sunk) return "✖";
                if (Model.State == CellState.Ship && _showShips) return "■";
                return "";
            }
        }

        public Brush TextColor
        {
            get { return Model.State == CellState.Sunk ? Brushes.White : Brushes.Yellow; }
        }

        public CellVM(Cell model, bool showShips)
        {
            Model = model;
            _showShips = showShips;
        }

        public void Refresh()
        {
            OnPropertyChanged("Background");
            OnPropertyChanged("Symbol");
            OnPropertyChanged("TextColor");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // --- Главная ViewModel окна ---
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _service = new GameService();

        // РЕПОЗИТОРИЙ
        private readonly IUserRepository _userRepository;

        private Field _playerField;
        private Field _computerField;

        public ObservableCollection<CellVM> PlayerCells { get; private set; }
        public ObservableCollection<CellVM> ComputerCells { get; private set; }

        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private TimeSpan _elapsed = TimeSpan.Zero;

        public string ElapsedTime
        {
            get { return string.Format("{0:D2}:{1:D2}", _elapsed.Minutes, _elapsed.Seconds); }
        }

        private int _lastHitX = -1;
        private int _lastHitY = -1;
        private bool _hunting = false;

        private string _playerName = "Игрок";
        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                OnPropertyChanged("PlayerName");
                LoadBestTime();
            }
        }

        private string _bestTime = "--:--";
        public string BestTime
        {
            get { return _bestTime; }
            set
            {
                _bestTime = value;
                OnPropertyChanged("BestTime");
            }
        }

        public MainWindowViewModel()
        {
            // Инициализируем репозиторий
            _userRepository = new UserRepository();

            PlayerCells = new ObservableCollection<CellVM>();
            ComputerCells = new ObservableCollection<CellVM>();

            NewGame();

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _elapsed = _elapsed.Add(TimeSpan.FromSeconds(1));
            OnPropertyChanged("ElapsedTime");
        }

        public void LoadBestTime()
        {
            try
            {
                // Используем репозиторий для получения данных
                var user = _userRepository.GetOrCreate(PlayerName);

                if (user != null && user.BestTimeSeconds.HasValue)
                {
                    var ts = TimeSpan.FromSeconds(user.BestTimeSeconds.Value);
                    BestTime = string.Format("{0:D2}:{1:D2}", ts.Minutes, ts.Seconds);
                }
                else
                {
                    BestTime = "--:--";
                }
            }
            catch
            {
                BestTime = "Error";
            }
        }

        public void NewGame()
        {
            _playerField = new Field();
            _computerField = new Field();
            _service.Initialize(_playerField, _computerField);

            PlayerCells.Clear();
            ComputerCells.Clear();

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    PlayerCells.Add(new CellVM(_playerField.Cells[x, y], true));
                    ComputerCells.Add(new CellVM(_computerField.Cells[x, y], false));
                }
            }

            _elapsed = TimeSpan.Zero;
            OnPropertyChanged("ElapsedTime");

            _lastHitX = -1;
            _lastHitY = -1;
            _hunting = false;
        }

        public void ShootAtEnemy(int x, int y)
        {
            var result = _service.PlayerAttack(_computerField, x, y);

            if (y * 10 + x < ComputerCells.Count)
                ComputerCells[y * 10 + x].Refresh();

            if (result.GameOver)
            {
                _timer.Stop();
                HandleGameOver(true);
                MessageBox.Show("ВЫ ПОБЕДИЛИ за " + ElapsedTime + "!", "Победа!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _service.ComputerAttack(_playerField, ref _lastHitX, ref _lastHitY, ref _hunting);

            foreach (CellVM cell in PlayerCells)
                cell.Refresh();

            if (_service.IsGameOver(_playerField))
            {
                _timer.Stop();
                HandleGameOver(false);
                MessageBox.Show("Вы проиграли за " + ElapsedTime + "...", "Поражение", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void HandleGameOver(bool isWin)
        {
            try
            {
                if (isWin)
                {
                    // Обновляем рекорд через репозиторий
                    _userRepository.UpdateBestTime(PlayerName, _elapsed.TotalSeconds);
                    LoadBestTime(); // Обновить цифры на экране
                }

                // Удаляем сохранение, так как игра закончена
                _userRepository.ClearSavedGame(PlayerName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка работы с данными: " + ex.Message);
            }
        }

        // --- Сериализация (Сохранение) ---

        public string GetSerializedGame()
        {
            var data = new GameSaveData
            {
                ElapsedSeconds = _elapsed.TotalSeconds,
                AiLastX = _lastHitX,
                AiLastY = _lastHitY,
                AiHunting = _hunting,
                PlayerShips = GetShipsCoords(_playerField),
                EnemyShips = GetShipsCoords(_computerField),
                PlayerFieldStates = GetFieldStates(_playerField),
                EnemyFieldStates = GetFieldStates(_computerField)
            };

            return JsonSerializer.Serialize(data);
        }

        public void LoadGameFromSave(string json)
        {
            try
            {
                var data = JsonSerializer.Deserialize<GameSaveData>(json);

                if (data == null) return;

                _playerField = new Field();
                _computerField = new Field();

                RestoreShips(_playerField, data.PlayerShips);
                RestoreShips(_computerField, data.EnemyShips);
                RestoreStates(_playerField, data.PlayerFieldStates);
                RestoreStates(_computerField, data.EnemyFieldStates);

                _elapsed = TimeSpan.FromSeconds(data.ElapsedSeconds);
                _lastHitX = data.AiLastX;
                _lastHitY = data.AiLastY;
                _hunting = data.AiHunting;

                OnPropertyChanged("ElapsedTime");
                _timer.Start();

                // Полная перерисовка
                PlayerCells.Clear();
                ComputerCells.Clear();

                for (int y = 0; y < 10; y++)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        PlayerCells.Add(new CellVM(_playerField.Cells[x, y], true));
                        ComputerCells.Add(new CellVM(_computerField.Cells[x, y], false));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message);
                NewGame();
            }
        }

        // --- Вспомогательные методы ---

        private List<List<PointDto>> GetShipsCoords(Field field)
        {
            var list = new List<List<PointDto>>();
            foreach (var ship in field.Ships)
            {
                var coords = new List<PointDto>();
                foreach (var cell in ship.Cells)
                {
                    coords.Add(new PointDto { X = cell.X, Y = cell.Y });
                }
                list.Add(coords);
            }
            return list;
        }

        private List<CellStateDto> GetFieldStates(Field field)
        {
            var list = new List<CellStateDto>();
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    var state = field.Cells[x, y].State;
                    if (state == CellState.Hit || state == CellState.Miss || state == CellState.Sunk)
                    {
                        list.Add(new CellStateDto { X = x, Y = y, State = (int)state });
                    }
                }
            }
            return list;
        }

        private void RestoreShips(Field field, List<List<PointDto>> shipsData)
        {
            foreach (var shipCoords in shipsData)
            {
                var ship = new Ship(shipCoords.Count);
                foreach (var coord in shipCoords)
                {
                    var cell = field.Cells[coord.X, coord.Y];
                    cell.Ship = ship;
                    cell.State = CellState.Ship;
                    ship.Cells.Add(cell);
                }
                field.Ships.Add(ship);
            }
        }

        private void RestoreStates(Field field, List<CellStateDto> states)
        {
            foreach (var s in states)
            {
                field.Cells[s.X, s.Y].State = (CellState)s.State;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // --- DTO Классы для сохранения ---
    public class GameSaveData
    {
        public double ElapsedSeconds { get; set; }
        public int AiLastX { get; set; }
        public int AiLastY { get; set; }
        public bool AiHunting { get; set; }
        public List<List<PointDto>> PlayerShips { get; set; } = new List<List<PointDto>>();
        public List<List<PointDto>> EnemyShips { get; set; } = new List<List<PointDto>>();
        public List<CellStateDto> PlayerFieldStates { get; set; } = new List<CellStateDto>();
        public List<CellStateDto> EnemyFieldStates { get; set; } = new List<CellStateDto>();
    }

    public class PointDto
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class CellStateDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int State { get; set; }
    }
}