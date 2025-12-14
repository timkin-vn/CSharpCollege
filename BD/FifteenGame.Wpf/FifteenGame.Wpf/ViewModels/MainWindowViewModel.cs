using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using FifteenGame.Wpf.ViewModels;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _service = new GameService();
        private Field _playerField;
        private Field _computerField;

        public ObservableCollection<CellVM> PlayerCells { get; private set; }
        public ObservableCollection<CellVM> ComputerCells { get; private set; }

        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private TimeSpan _elapsed = TimeSpan.Zero;

        public string ElapsedTime =>
            $"{_elapsed.Minutes:D2}:{_elapsed.Seconds:D2}";

        private double? _bestTimeSeconds;
        public string BestTime
        {
            get
            {
                if (_bestTimeSeconds == null)
                    return "—";
                var ts = TimeSpan.FromSeconds(_bestTimeSeconds.Value);
                return $"{ts.Minutes:D2}:{ts.Seconds:D2}";
            }
        }

        private int _lastHitX = -1;
        private int _lastHitY = -1;
        private bool _hunting = false;

        private string _playerName = "Игрок";
        public string PlayerName
        {
            get => _playerName;
            set
            {
                _playerName = value;
                OnPropertyChanged(nameof(PlayerName));
            }
        }

        public MainWindowViewModel()
        {
            PlayerCells = new ObservableCollection<CellVM>();
            ComputerCells = new ObservableCollection<CellVM>();

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) =>
            {
                _elapsed = _elapsed.Add(TimeSpan.FromSeconds(1));
                OnPropertyChanged(nameof(ElapsedTime));
            };

            NewGame();
            _timer.Start();
        }

        public void LoadBestTime(string playerName)
        {
            using (var db = new GameDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == playerName);
                _bestTimeSeconds = user?.BestTimeSeconds;
                OnPropertyChanged(nameof(BestTime));
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
            OnPropertyChanged(nameof(ElapsedTime));

            _lastHitX = -1;
            _lastHitY = -1;
            _hunting = false;
        }

        public void ShootAtEnemy(int x, int y)
        {
            var result = _service.PlayerAttack(_computerField, x, y);
            ComputerCells[y * 10 + x].Refresh();

            if (result.GameOver)
            {
                _timer.Stop();

                using (var db = new GameDbContext())
                {
                    var user = db.Users.First(u => u.Username == PlayerName);
                    double seconds = _elapsed.TotalSeconds;

                    if (user.BestTimeSeconds == null || seconds < user.BestTimeSeconds)
                    {
                        user.BestTimeSeconds = seconds;
                        db.SaveChanges();
                        LoadBestTime(PlayerName); // 🔥 ОБНОВЛЕНИЕ РЕКОРДА В UI
                    }
                }

                MessageBox.Show($"ВЫ ПОБЕДИЛИ за {ElapsedTime}!", "Победа");
                return;
            }

            _service.ComputerAttack(_playerField, ref _lastHitX, ref _lastHitY, ref _hunting);
            foreach (var cell in PlayerCells)
                cell.Refresh();

            if (_service.IsGameOver(_playerField))
            {
                _timer.Stop();
                MessageBox.Show($"Вы проиграли за {ElapsedTime}...", "Поражение");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
