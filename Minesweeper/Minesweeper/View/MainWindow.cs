using Minesweeper.Business.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace Minesweeper.View
{
    public partial class MainWindow : Window
    {
        private Field Field;
        private Border[,] Border;
        private bool _debugMode = false;

        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            Field = new Field(10, 15);
            Border = new Border[10, 10];
            _debugMode = false;
            CreateGrid();
        }

        public void CreateGrid()
        {
            MainGrid.Children.Clear();
            MainGrid.RowDefinitions.Clear();
            MainGrid.ColumnDefinitions.Clear();

            MainGrid.HorizontalAlignment = HorizontalAlignment.Center;
            MainGrid.VerticalAlignment = VerticalAlignment.Center;
            MainGrid.Margin = new Thickness(40);

            for (int i = 0; i < 10; i++)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
            }

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    var cell = CreateCell(row, col);
                    Border[row, col] = cell;

                    Grid.SetRow(cell, row);
                    Grid.SetColumn(cell, col);
                    MainGrid.Children.Add(cell);
                }
            }

            if (_debugMode)
            {
                ShowAllMines();
            }
        }

        private Border CreateCell(int row, int col)
        {
            var cell = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Background = Brushes.LightBlue,
                Tag = new Point(row, col)
            };

            cell.MouseLeftButtonDown += Cell_MouseLeftButtonDown;
            cell.MouseRightButtonDown += Cell_MouseRightButtonDown;

            return cell;
        }

        private void Cell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Field.GameOver || Field.GameWon)
                return;

            var cell = (Border)sender;
            var point = (Point)cell.Tag;
            int row = (int)point.X;
            int col = (int)point.Y;

            OpenCell(row, col);
        }

        private void Cell_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Field.GameOver || Field.GameWon)
                return;

            var cell = (Border)sender;
            var point = (Point)cell.Tag;
            int row = (int)point.X;
            int col = (int)point.Y;

            ToggleFlag(row, col);
        }

        private void OpenCell(int row, int col)
        {
            var cell = Border[row, col];

            if (cell.Background == Brushes.White || (cell.Child is TextBlock textBlock && textBlock.Text == "🚩"))
                return;

            Field.RevealCell(row, col);

            UpdateCellDisplay(row, col);

            CheckGameStatus();
        }

        private void UpdateCellDisplay(int row, int col)
        {
            var cell = Border[row, col];

            if (Field.Revealed[row, col])
            {
                cell.Background = Brushes.White;

                if (Field.GetMine(row, col))
                {
                    ShowMine(cell);
                }
                else
                {
                    int minesCount = Field.GetNumber(row, col);
                    if (minesCount > 0)
                    {
                        ShowNumber(cell, minesCount);
                    }
                }
            }
        }

        private void CheckGameStatus()
        {
            if (Field.GameOver)
            {
                ShowAllMines();
                MessageBox.Show("Поражение", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (Field.GameWon)
            {
                MessageBox.Show("Победа", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void ShowNumber(Border cell, int number)
        {
            var textBlock = new TextBlock
            {
                Text = number.ToString(),
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = GetNumberColor(number)
            };
            cell.Child = textBlock;
        }

        private void ShowMine(Border cell)
        {
            var textBlock = new TextBlock
            {
                Text = "💥",
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            cell.Child = textBlock;
            cell.Background = Brushes.Red;
        }

        private void ShowMineDebug(Border cell)
        {
            var textBlock = new TextBlock
            {
                Text = "💥",
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.Purple
            };
            cell.Child = textBlock;
            cell.Background = Brushes.LightPink;
        }

        private void ShowAllMines()
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (Field.GetMine(row, col))
                    {
                        var cell = Border[row, col];
                        if (cell.Child == null)
                        {
                            ShowMine(cell);
                        }
                    }
                }
            }
        }

        private void ShowAllMinesDebug()
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (Field.GetMine(row, col))
                    {
                        var cell = Border[row, col];
                        ShowMineDebug(cell);
                    }
                }
            }
        }

        private void HideAllMinesDebug()
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (Field.GetMine(row, col))
                    {
                        var cell = Border[row, col];
                        if (cell.Background != Brushes.Red && cell.Background != Brushes.White && cell.Background != Brushes.LightGreen)
                        {
                            cell.Child = null;
                            cell.Background = Brushes.LightBlue;
                        }
                    }
                }
            }
        }

        private void ToggleFlag(int row, int col)
        {
            var cell = Border[row, col];

            if (Field.Revealed[row, col])
                return;

            Field.ToggleFlag(row, col);

            if (Field.Flag[row, col])
            {
                var textBlock = new TextBlock
                {
                    Text = "🚩",
                    FontSize = 14,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                cell.Child = textBlock;
                cell.Background = Brushes.LightYellow;
            }
            else
            {
                cell.Child = null;
                cell.Background = Brushes.LightBlue;
            }

            CheckGameStatus();
        }

        private SolidColorBrush GetNumberColor(int number)
        {
            switch (number)
            {
                case 1: return Brushes.Blue;
                case 2: return Brushes.Green;
                case 3: return Brushes.Red;
                case 4: return Brushes.DarkBlue;
                case 5: return Brushes.DarkRed;
                case 6: return Brushes.Teal;
                case 7: return Brushes.Black;
                case 8: return Brushes.Gray;
                default: return Brushes.Black;
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void DebugButton_Click(object sender, RoutedEventArgs e)
        {
            if (Field.GameOver || Field.GameWon)
                return;

            _debugMode = !_debugMode;

            if (_debugMode)
            {
                DebugButton.Content = "Скрыть мины";
                DebugButton.Background = Brushes.LightGreen;
                ShowAllMinesDebug();
            }
            else
            {
                DebugButton.Content = "Показать мины";
                DebugButton.Background = Brushes.LightCoral;
                HideAllMinesDebug();
            }
        }
    }
}