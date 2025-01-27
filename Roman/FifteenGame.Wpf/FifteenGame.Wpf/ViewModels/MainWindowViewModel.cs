using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged, IDisposable
    {
        private GameService _service = new GameService();
        private GameModel _model = new GameModel();

        private Timer _timer;
        private DateTime _startTime;
        private int _moveCount;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public string TimeElapsed { get; private set; } = "00:00";

        public int MoveCount
        {
            get => _moveCount;
            private set
            {
                _moveCount = value;
                OnPropertyChanged(nameof(MoveCount));
            }
        }

        public MainWindowViewModel()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += (s, e) => UpdateTimeElapsed();
            Initialize();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Initialize()
        {
            _service.Shuffle(_model);
            FromModel(_model);

            //по умолчанию
            _startTime = DateTime.Now;
            MoveCount = 0;
            TimeElapsed = "00:00";
            OnPropertyChanged(nameof(TimeElapsed));

            _timer.Start();
        }

        public void MakeMove(MoveDirection direction, Action gameFinishedAction)
        {
            if (_service.MakeMove(_model, direction))
            {
                MoveCount++;//инкапсуляция хода
            }

            FromModel(_model);

            if (_service.IsGameOver(_model))
            {
                _timer.Stop();
                gameFinishedAction();
            }
        }

        private void UpdateTimeElapsed()
        {
            var elapsed = DateTime.Now - _startTime;
            TimeElapsed = elapsed.ToString(@"mm\:ss");
            OnPropertyChanged(nameof(TimeElapsed));
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] != GameModel.FreeCellValue)
                    {
                        var direction = MoveDirection.None;
                        if (row == model.FreeCellRow)
                        {
                            if (column == model.FreeCellColumn - 1) direction = MoveDirection.Right;
                            else if (column == model.FreeCellColumn + 1) direction = MoveDirection.Left;
                        }
                        else if (column == model.FreeCellColumn)
                        {
                            if (row == model.FreeCellRow - 1) direction = MoveDirection.Down;
                            else if (row == model.FreeCellRow + 1) direction = MoveDirection.Up;
                        }

                        Cells.Add(new CellViewModel
                        {
                            Row = row,
                            Column = column,
                            Num = model[row, column],
                            Direction = direction
                        });
                    }
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Dispose();
        }
    }
}
