using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private GameService _service = new GameService();

        private GameModel _model = new GameModel();

        private DispatcherTimer _timer;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public int MovesCount => _model.MovesCount;

        public string TimeText
        {
            get
            {
                int minutes = _model.Seconds / 60;
                int seconds = _model.Seconds % 60;

                return $"Время: {minutes:00}:{seconds:00}";
            }
        }

        public string BestText
        {
            get
            {
                if (_model.BestMoves == int.MaxValue)
                {
                    return "Рекорд: нет";
                }

                int minutes = _model.BestSeconds / 60;
                int seconds = _model.BestSeconds % 60;

                return $"Рекорд: {_model.BestMoves} ходов, {minutes:00}:{seconds:00}";
            }
        }

        public MainWindowViewModel()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            Initialize();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _model.Seconds++;

            OnPropertyChanged(nameof(TimeText));
        }

        public void Initialize()
        {
            _service.Initialize(_model);

            _timer.Stop();
            _timer.Start();

            OnPropertyChanged(nameof(MovesCount));
            OnPropertyChanged(nameof(TimeText));

            LoadViewModel(_model);
        }

        public async Task OpenCell(int index)
        {
            if (_model.IsCheckingPair)
            {
                return;
            }

            _service.OpenCell(_model, index);

            OnPropertyChanged(nameof(MovesCount));

            LoadViewModel(_model);

            if (_model.SecondOpenedCell != -1)
            {
                _model.IsCheckingPair = true;

                await Task.Delay(700);

                _service.CheckOpenedCells(_model);

                _model.IsCheckingPair = false;

                LoadViewModel(_model);

                if (_service.IsGameOver(_model))
                {
                    _timer.Stop();

                    bool newRecord = false;

                    if (_model.MovesCount < _model.BestMoves)
                    {
                        _model.BestMoves = _model.MovesCount;
                        newRecord = true;
                    }

                    if (_model.Seconds < _model.BestSeconds)
                    {
                        _model.BestSeconds = _model.Seconds;
                        newRecord = true;
                    }

                    OnPropertyChanged(nameof(BestText));

                    string message = newRecord
                        ? "Новый рекорд!" : "Поздравляю вас";

                    System.Windows.MessageBox.Show(
                        $"{message}\n\nХоды: {_model.MovesCount}\nВремя: {TimeText}",
                        "Победа");
                }
            }
        }

        private void LoadViewModel(GameModel model)
        {
            Cells.Clear();

            for (int i = 0; i < model.Cells.Count; i++)
            {
                Cells.Add(new CellViewModel
                {
                    Row = i / GameModel.ColumnCount,
                    Column = i % GameModel.ColumnCount,
                    Color = model.Cells[i].Color,
                    IsOpened = model.Cells[i].IsOpened,
                    IsFound = model.Cells[i].IsFound,
                    Index = i,
                });
            }
        }
    }
}
