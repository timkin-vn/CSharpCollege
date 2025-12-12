using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Linq;


namespace FifteenGame.Wpf.ViewModels
{
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

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _service = new GameService();
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

        public MainWindowViewModel()
        {
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
        private string _playerName = "Игрок";
        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                OnPropertyChanged("PlayerName");
            }
        }

        public void ShootAtEnemy(int x, int y)
        {
            var result = _service.PlayerAttack(_computerField, x, y);
            ComputerCells[y * 10 + x].Refresh();

            if (result.GameOver)
            {
                _timer.Stop();

                // Сохраняем рекорд
                using (var db = new GameDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == PlayerName);
                    if (user != null)
                    {
                        double seconds = _elapsed.TotalSeconds;

                        if (user.BestTimeSeconds == null || seconds < user.BestTimeSeconds)
                        {
                            user.BestTimeSeconds = seconds;
                            db.SaveChanges();
                        }
                    }
                }

                MessageBox.Show("ВЫ ПОБЕДИЛИ за " + ElapsedTime + "!", "Победа!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            _service.ComputerAttack(_playerField, ref _lastHitX, ref _lastHitY, ref _hunting);

            foreach (CellVM cell in PlayerCells)
                cell.Refresh();

            if (_service.IsGameOver(_playerField))
            {
                _timer.Stop();
                MessageBox.Show("Вы проиграли за " + ElapsedTime + "...", "Поражение", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}