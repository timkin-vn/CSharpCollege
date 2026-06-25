using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MinesweeperWpf
{
    public partial class MainWindow : Window
    {
        private readonly Random _random = new Random();

        private Cell[,] _cells;
        private bool _gameOver;
        private bool _minesPlaced;
        private bool _isWindowReady;
        private int _openedCells;
        private int _flagsCount;
        private int _rows;
        private int _columns;
        private int _minesCount;
        private int _cellSize;

        public MainWindow()
        {
            InitializeComponent();

            DifficultyComboBox.SelectedIndex = 0;
            FirstMoveComboBox.SelectedIndex = 0;

            _isWindowReady = true;
            StartNewGame();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void DifficultyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isWindowReady)
            {
                return;
            }

            StartNewGame();
        }

        private void FirstMoveComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isWindowReady)
            {
                return;
            }

            StartNewGame();
        }

        private void StartNewGame()
        {
            DifficultySettings difficulty = GetSelectedDifficulty();

            _rows = difficulty.Rows;
            _columns = difficulty.Columns;
            _minesCount = difficulty.MinesCount;
            _cellSize = difficulty.CellSize;

            _gameOver = false;
            _minesPlaced = false;
            _openedCells = 0;
            _flagsCount = 0;
            _cells = new Cell[_rows, _columns];

            CreateVisualField();

            if (!IsSafeFirstMoveSelected())
            {
                PlaceMines(-1, -1);
                CalculateNumbers();
            }

            UpdateInfo("Новая игра. " + GetSelectedDifficultyName() + ". Первый ход: " + GetFirstMoveModeName() + ".");
        }

        private DifficultySettings GetSelectedDifficulty()
        {
            switch (DifficultyComboBox.SelectedIndex)
            {
                case 1:
                    return new DifficultySettings(12, 12, 25, 32);
                case 2:
                    return new DifficultySettings(16, 16, 50, 30);
                default:
                    return new DifficultySettings(9, 9, 10, 34);
            }
        }

        private string GetSelectedDifficultyName()
        {
            switch (DifficultyComboBox.SelectedIndex)
            {
                case 1:
                    return "Средний режим: поле 12×12, 25 мин";
                case 2:
                    return "Тяжёлый режим: поле 16×16, 50 мин";
                default:
                    return "Лёгкий режим: поле 9×9, 10 мин";
            }
        }

        private bool IsSafeFirstMoveSelected()
        {
            return FirstMoveComboBox.SelectedIndex != 1;
        }

        private string GetFirstMoveModeName()
        {
            return IsSafeFirstMoveSelected() ? "безопасный" : "опасный";
        }

        private void CreateVisualField()
        {
            GameGrid.Children.Clear();
            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();

            for (int row = 0; row < _rows; row++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(_cellSize)
                });
            }

            for (int column = 0; column < _columns; column++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(_cellSize)
                });
            }

            for (int row = 0; row < _rows; row++)
            {
                for (int column = 0; column < _columns; column++)
                {
                    Cell cell = new Cell(row, column);
                    Button button = new Button
                    {
                        Style = (Style)FindResource("CellButtonStyle"),
                        Tag = cell
                    };

                    button.Click += Cell_LeftClick;
                    button.MouseRightButtonUp += Cell_RightClick;

                    cell.Button = button;
                    _cells[row, column] = cell;

                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, column);
                    GameGrid.Children.Add(button);
                }
            }
        }

        private void PlaceMines(int safeRow, int safeColumn)
        {
            int placed = 0;

            while (placed < _minesCount)
            {
                int row = _random.Next(_rows);
                int column = _random.Next(_columns);

                if (_cells[row, column].IsMine)
                {
                    continue;
                }

                if (row == safeRow && column == safeColumn)
                {
                    continue;
                }

                _cells[row, column].IsMine = true;
                placed++;
            }

            _minesPlaced = true;
        }

        private void CalculateNumbers()
        {
            for (int row = 0; row < _rows; row++)
            {
                for (int column = 0; column < _columns; column++)
                {
                    _cells[row, column].MinesAround = 0;

                    if (_cells[row, column].IsMine)
                    {
                        continue;
                    }

                    int minesAround = 0;

                    for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
                    {
                        for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
                        {
                            if (rowOffset == 0 && columnOffset == 0)
                            {
                                continue;
                            }

                            int nearRow = row + rowOffset;
                            int nearColumn = column + columnOffset;

                            if (IsInsideField(nearRow, nearColumn) && _cells[nearRow, nearColumn].IsMine)
                            {
                                minesAround++;
                            }
                        }
                    }

                    _cells[row, column].MinesAround = minesAround;
                }
            }
        }

        private void Cell_LeftClick(object sender, RoutedEventArgs e)
        {
            if (_gameOver)
            {
                return;
            }

            Button button = (Button)sender;
            Cell cell = (Cell)button.Tag;

            if (cell.IsOpened || cell.IsFlagged)
            {
                return;
            }

            if (!_minesPlaced)
            {
                PlaceMines(cell.Row, cell.Column);
                CalculateNumbers();
            }

            if (cell.IsMine)
            {
                cell.Button.Background = Brushes.IndianRed;
                RevealAllCells();
                FinishGame(false);
                return;
            }

            OpenCell(cell.Row, cell.Column);

            if (CheckWin())
            {
                return;
            }

            UpdateInfo("Ход выполнен. Можно открывать следующую клетку.");
        }

        private void Cell_RightClick(object sender, MouseButtonEventArgs e)
        {
            if (_gameOver)
            {
                return;
            }

            Button button = (Button)sender;
            Cell cell = (Cell)button.Tag;

            if (cell.IsOpened)
            {
                return;
            }

            cell.IsFlagged = !cell.IsFlagged;

            if (cell.IsFlagged)
            {
                cell.Button.Content = "⚑";
                cell.Button.Foreground = Brushes.DarkRed;
                _flagsCount++;
            }
            else
            {
                cell.Button.Content = "";
                _flagsCount--;
            }

            UpdateInfo("Флажок поставлен или убран.");
        }

        private void OpenCell(int row, int column)
        {
            if (!IsInsideField(row, column))
            {
                return;
            }

            Cell cell = _cells[row, column];

            if (cell.IsOpened || cell.IsFlagged || cell.IsMine)
            {
                return;
            }

            cell.IsOpened = true;
            _openedCells++;

            cell.Button.IsEnabled = false;
            cell.Button.Background = Brushes.WhiteSmoke;
            cell.Button.BorderBrush = Brushes.LightGray;

            if (cell.MinesAround > 0)
            {
                cell.Button.Content = cell.MinesAround.ToString();
                cell.Button.Foreground = GetNumberBrush(cell.MinesAround);
                return;
            }

            cell.Button.Content = "";

            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
                {
                    if (rowOffset == 0 && columnOffset == 0)
                    {
                        continue;
                    }

                    OpenCell(row + rowOffset, column + columnOffset);
                }
            }
        }

        private void RevealAllCells()
        {
            for (int row = 0; row < _rows; row++)
            {
                for (int column = 0; column < _columns; column++)
                {
                    Cell cell = _cells[row, column];
                    cell.Button.IsEnabled = false;

                    if (cell.IsMine)
                    {
                        cell.Button.Content = "✹";
                        cell.Button.Foreground = Brushes.Black;
                    }
                    else if (cell.MinesAround > 0)
                    {
                        cell.Button.Content = cell.MinesAround.ToString();
                        cell.Button.Foreground = GetNumberBrush(cell.MinesAround);
                    }
                    else
                    {
                        cell.Button.Content = "";
                    }
                }
            }
        }

        private bool CheckWin()
        {
            int safeCells = _rows * _columns - _minesCount;

            if (_openedCells == safeCells)
            {
                RevealAllCells();
                FinishGame(true);
                return true;
            }

            return false;
        }

        private void FinishGame(bool isWin)
        {
            _gameOver = true;

            if (isWin)
            {
                UpdateInfo("Победа! Все безопасные клетки открыты.");
            }
            else
            {
                UpdateInfo("Поражение. Открыта клетка с миной.");
            }
        }

        private bool IsInsideField(int row, int column)
        {
            return row >= 0 && row < _rows && column >= 0 && column < _columns;
        }

        private void UpdateInfo(string status)
        {
            int safeCells = _rows * _columns - _minesCount;

            MinesTextBlock.Text = "Поле: " + _rows + "×" + _columns + " | Мины: " + _minesCount + " | Флажки: " + _flagsCount;
            OpenedTextBlock.Text = "Открыто: " + _openedCells + "/" + safeCells;
            StatusTextBlock.Text = status;
        }

        private Brush GetNumberBrush(int number)
        {
            switch (number)
            {
                case 1:
                    return Brushes.Blue;
                case 2:
                    return Brushes.Green;
                case 3:
                    return Brushes.Red;
                case 4:
                    return Brushes.DarkBlue;
                case 5:
                    return Brushes.Brown;
                case 6:
                    return Brushes.DarkCyan;
                case 7:
                    return Brushes.Black;
                default:
                    return Brushes.Gray;
            }
        }

        private class DifficultySettings
        {
            public DifficultySettings(int rows, int columns, int minesCount, int cellSize)
            {
                Rows = rows;
                Columns = columns;
                MinesCount = minesCount;
                CellSize = cellSize;
            }

            public int Rows { get; private set; }
            public int Columns { get; private set; }
            public int MinesCount { get; private set; }
            public int CellSize { get; private set; }
        }

        private class Cell
        {
            public Cell(int row, int column)
            {
                Row = row;
                Column = column;
            }

            public int Row { get; private set; }
            public int Column { get; private set; }
            public bool IsMine { get; set; }
            public bool IsOpened { get; set; }
            public bool IsFlagged { get; set; }
            public int MinesAround { get; set; }
            public Button Button { get; set; }
        }
    }
}
