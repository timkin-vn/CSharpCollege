using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using FifteenGame.Wpf.ViewModels;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        private int _currentSize = 9;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            GameField.ItemsSource = _viewModel.Cells;

           
            StartNewGame();
        }

        private void StartNewGame()
        {
            _viewModel.SelectSize(_currentSize);
            UpdateGridSize();
        }

        private void UpdateGridSize()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                var uniformGrid = FindVisualChild<UniformGrid>(GameField);
                if (uniformGrid != null)
                {
                    uniformGrid.Rows = _currentSize;
                    uniformGrid.Columns = _currentSize;
                }
            }));
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T result)
                    return result;
                var descendant = FindVisualChild<T>(child);
                if (descendant != null)
                    return descendant;
            }
            return null;
        }

        
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void SmallButton_Click(object sender, RoutedEventArgs e)
        {
            _currentSize = 9;
            StartNewGame();
        }

        private void MediumButton_Click(object sender, RoutedEventArgs e)
        {
            _currentSize = 16;
            StartNewGame();
        }

        private void LargeButton_Click(object sender, RoutedEventArgs e)
        {
            _currentSize = 24;
            StartNewGame();
        }

       
        private void OpenModeButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ToggleMode("open");
            OpenModeButton.Background = Brushes.LightGreen;
            FlagModeButton.Background = Brushes.LightGray;
        }

        private void FlagModeButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ToggleMode("flag");
            FlagModeButton.Background = Brushes.LightGreen;
            OpenModeButton.Background = Brushes.LightGray;
        }

      
        private void CellButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is CellViewModel cellViewModel)
            {
                _viewModel.HandleCellClick(cellViewModel);
            }
        }

        
        private void CellButton_RightClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button button && button.DataContext is CellViewModel cellViewModel)
            {
                _viewModel.HandleCellRightClick(cellViewModel);
                e.Handled = true;
            }
        }
    }
}