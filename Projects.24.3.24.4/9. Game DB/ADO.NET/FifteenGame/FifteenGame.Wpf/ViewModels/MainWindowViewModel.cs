using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastucture;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private GameModel _model = new GameModel();

        private UserModel _user;

        private string _gameTheme = "Colors";

        private int _pairCount = 8;

        private int _easyRecord = 0;

        private int _mediumRecord = 0;

        private int _hardRecord = 0;

        private int _firstOpenedRow = -1;

        private int _firstOpenedColumn = -1;

        private DispatcherTimer _timer;

        private int _seconds;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "Игрок";

        public int MoveCount => _model.MoveCount;

        public string Record
        {
            get
            {
                int record = GetRecord();

                if (record == 0)
                {
                    return "—";
                }

                return record.ToString();
            }
        }

        public string Time => TimeSpan.FromSeconds(_seconds).ToString(@"mm\:ss");

        public MainWindowViewModel()
        {
            _timer = new DispatcherTimer();

            _timer.Interval = TimeSpan.FromSeconds(1);

            _timer.Tick += Timer_Tick;

            CreateGame();
        }

        public void CommitUser(UserModel userModel)
        {
            _user = userModel;
            OnPropertyChanged(nameof(UserName));
        }

        public void Initialize()
        {
            CreateGame();
        }

        private bool IsGameFinished()
        {
            int foundPairs = 0;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (_model.IsFound(row, column))
                    {
                        foundPairs++;
                    }
                }
            }

            return foundPairs == _pairCount * 2;
        }

        public async void OpenCell(int row, int column)
        {
            if (row < 0 || row >= Constants.RowCount || column < 0 || column >= Constants.ColumnCount)

            {
                return;
            }

            if (_model.IsFound(row, column))

            {
                return;
            }

            if (_model.IsOpened(row, column))

            {
                return;
            }

            _model.SetOpened(row, column, true);

            if (_firstOpenedRow == -1)

            {
                _firstOpenedRow = row;
                _firstOpenedColumn = column;

                LoadViewModel(_model);
                return;
            }

            _model.MoveCount++;

            LoadViewModel(_model);

            await Task.Delay(700);

            if (_model[_firstOpenedRow, _firstOpenedColumn] == _model[row, column])

            {
                _model.SetFound(_firstOpenedRow, _firstOpenedColumn, true);
                _model.SetFound(row, column, true);
            }

            else

            {
                _model.SetOpened(_firstOpenedRow, _firstOpenedColumn, false);
                _model.SetOpened(row, column, false);
            }

            if (IsGameFinished())
            {
                _timer.Stop();

                int record = GetRecord();

                if (record == 0 || _model.MoveCount < record)
                {
                    SetRecord(_model.MoveCount);

                    MessageBox.Show($"Поздравляем! Вы нашли все пары!\n\n" + $"Количество ходов: {_model.MoveCount}\n" + $"Время: {Time}\n\n" + $"🏆 Новый рекорд!", "Победа!", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                else

                {
                    MessageBox.Show( $"Поздравляем! Вы нашли все пары!\n\n" + $"Количество ходов: {_model.MoveCount}\n" + $"Время: {Time}\n\n" + $"Рекорд: {record} ходов", "Победа!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            _firstOpenedRow = -1;
            _firstOpenedColumn = -1;

            LoadViewModel(_model);
        }

        private void CreateGame()
        {
            _model = new GameModel();

            _seconds = 0;

            OnPropertyChanged(nameof(Time));

            _timer.Start();

            List<int> values = new List<int>();

            for (int value = 1; value <= _pairCount; value++)
            {
                values.Add(value);
                values.Add(value);
            }

            Random random = new Random();

            for (int i = values.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                int temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }

            int index = 0;

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (index < values.Count)

                    {
                        _model[row, column] = values[index];
                        index++;
                    }

                    else

                    {
                        _model[row, column] = Constants.FreeCellValue;
                    }
                }
            }

            LoadViewModel(_model);
        }

        private void LoadViewModel(GameModel model)
        {
            Cells.Clear();

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (model[row, column] == Constants.FreeCellValue)
                    {
                        continue;
                    }

                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Theme = _gameTheme,
                        Value = model[row, column],
                        Direction = MoveDirection.None,
                        IsOpened = model.IsOpened(row, column),
                        IsFound = model.IsFound(row, column)
                    });
                }
            }

            OnPropertyChanged(nameof(MoveCount));
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _seconds++;
            OnPropertyChanged(nameof(Time));
        }

        public void SetTheme(string theme)
        {
            _gameTheme = theme;
            CreateGame();
        }

        public void SetDifficulty(int pairCount)
        {
            _pairCount = pairCount;

            OnPropertyChanged(nameof(Record));

            CreateGame();
        }

        private int GetRecord()
        {
            switch (_pairCount)
            {
                case 4:
                    return _easyRecord;

                case 6:
                    return _mediumRecord;

                case 8:
                    return _hardRecord;

                default:
                    return 0;
            }
        }

        private void SetRecord(int record)
        {
            switch (_pairCount)
            {
                case 4:
                    _easyRecord = record;
                    break;

                case 6:
                    _mediumRecord = record;
                    break;

                case 8:
                    _hardRecord = record;
                    break;
            }

            OnPropertyChanged(nameof(Record));
        }
    }
}