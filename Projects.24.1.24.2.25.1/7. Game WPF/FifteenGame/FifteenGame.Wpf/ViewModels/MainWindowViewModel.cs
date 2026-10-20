using System.Collections.ObjectModel;
using System.Windows.Media;
using FifteenGame.Business.Models;
using FifteenGame.Business.Services;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly MinesweeperService _service = new MinesweeperService();
        private MinesweeperGame _game;
        private int _lastMineRow = -1;
        private int _lastMineColumn = -1;
        private int _rows;
        private int _columns;
        private int _cellSize;
        private string _minesInfo;
        private string _openedInfo;
        private string _status;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public int Rows { get { return _rows; } private set { _rows = value; OnPropertyChanged(nameof(Rows)); } }
        public int Columns { get { return _columns; } private set { _columns = value; OnPropertyChanged(nameof(Columns)); } }
        public int CellSize { get { return _cellSize; } private set { _cellSize = value; OnPropertyChanged(nameof(CellSize)); } }
        public string MinesInfo { get { return _minesInfo; } private set { _minesInfo = value; OnPropertyChanged(nameof(MinesInfo)); } }
        public string OpenedInfo { get { return _openedInfo; } private set { _openedInfo = value; OnPropertyChanged(nameof(OpenedInfo)); } }
        public string Status { get { return _status; } private set { _status = value; OnPropertyChanged(nameof(Status)); } }

        public MainWindowViewModel()
        {
            NewGame(0, true);
        }

        public void NewGame(int difficultyIndex, bool safeFirstMove)
        {
            int mines;
            string difficulty;
            switch (difficultyIndex)
            {
                case 1:
                    Rows = 12;
                    Columns = 12;
                    mines = 25;
                    CellSize = 32;
                    difficulty = "Средний режим: поле 12×12, 25 мин";
                    break;
                case 2:
                    Rows = 16;
                    Columns = 16;
                    mines = 50;
                    CellSize = 30;
                    difficulty = "Тяжёлый режим: поле 16×16, 50 мин";
                    break;
                default:
                    Rows = 9;
                    Columns = 9;
                    mines = 10;
                    CellSize = 34;
                    difficulty = "Лёгкий режим: поле 9×9, 10 мин";
                    break;
            }

            _game = _service.CreateGame(Rows, Columns, mines, safeFirstMove);
            _lastMineRow = -1;
            _lastMineColumn = -1;
            UpdateView("Новая игра. " + difficulty + ". Первый ход: " + (safeFirstMove ? "безопасный" : "опасный") + ".");
        }

        public void OpenCell(int row, int column)
        {
            if (!_service.OpenCell(_game, row, column))
            {
                return;
            }

            if (_game.IsFinished && !_game.IsWon)
            {
                _lastMineRow = row;
                _lastMineColumn = column;
            }

            string status = _game.IsFinished
                ? (_game.IsWon ? "Победа! Все безопасные клетки открыты." : "Поражение. Открыта клетка с миной.")
                : "Ход выполнен. Можно открывать следующую клетку.";
            UpdateView(status);
        }

        public void ToggleFlag(int row, int column)
        {
            if (_service.ToggleFlag(_game, row, column))
            {
                UpdateView("Флажок поставлен или убран.");
            }
        }

        private void UpdateView(string status)
        {
            Cells.Clear();
            for (int row = 0; row < _game.Rows; row++)
            {
                for (int column = 0; column < _game.Columns; column++)
                {
                    MinesweeperCell cell = _game[row, column];
                    bool revealAll = _game.IsFinished;
                    bool shownOpen = cell.IsOpened || revealAll;
                    string text = "";
                    Brush foreground = Brushes.Black;
                    Brush background = shownOpen ? Brushes.WhiteSmoke : Brushes.Gainsboro;
                    Brush border = shownOpen ? Brushes.LightGray : Brushes.Gray;

                    if (revealAll && cell.IsMine)
                    {
                        text = "✹";
                        if (row == _lastMineRow && column == _lastMineColumn)
                        {
                            background = Brushes.IndianRed;
                        }
                    }
                    else if (shownOpen && cell.MinesAround > 0)
                    {
                        text = cell.MinesAround.ToString();
                        foreground = GetNumberBrush(cell.MinesAround);
                    }
                    else if (!shownOpen && cell.IsFlagged)
                    {
                        text = "⚑";
                        foreground = Brushes.DarkRed;
                    }

                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Text = text,
                        Background = background,
                        Foreground = foreground,
                        BorderBrush = border,
                        IsEnabled = !shownOpen && !_game.IsFinished
                    });
                }
            }

            MinesInfo = "Поле: " + Rows + "×" + Columns + " | Мины: " + _game.MinesCount + " | Флажки: " + _game.FlagsCount;
            OpenedInfo = "Открыто: " + _game.OpenedCells + "/" + (Rows * Columns - _game.MinesCount);
            Status = status;
        }

        private Brush GetNumberBrush(int number)
        {
            switch (number)
            {
                case 1: return Brushes.Blue;
                case 2: return Brushes.Green;
                case 3: return Brushes.Red;
                case 4: return Brushes.DarkBlue;
                case 5: return Brushes.Brown;
                case 6: return Brushes.DarkCyan;
                case 7: return Brushes.Black;
                default: return Brushes.Gray;
            }
        }
    }
}
