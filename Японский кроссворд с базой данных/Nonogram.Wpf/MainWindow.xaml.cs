using Nonogram.Wpf.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Nonogram.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Debug.WriteLine("=== MainWindow создан ===");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка создания MainWindow: {ex.Message}");
                throw;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("=== MainWindow загружен ===");

            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                Debug.WriteLine($"Cells count: {viewModel.Cells?.Count ?? 0}");
                Debug.WriteLine($"RowClues count: {viewModel.RowClues?.Count ?? 0}");
                Debug.WriteLine($"ColumnClues count: {viewModel.ColumnClues?.Count ?? 0}");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Debug.WriteLine("=== MainWindow закрыт ===");
            Application.Current.Shutdown();
        }

        private void Cell_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("=== Клик по клетке ===");

            if (e.ChangedButton != MouseButton.Left)
            {
                Debug.WriteLine("Нажата не левая кнопка мыши");
                return;
            }

            var grid = sender as System.Windows.Controls.Grid;
            if (grid == null)
            {
                Debug.WriteLine("Ошибка: sender не Grid");
                return;
            }

            var cellViewModel = grid.Tag as CellViewModel;
            if (cellViewModel == null)
            {
                Debug.WriteLine("Ошибка: Tag не содержит CellViewModel");
                return;
            }

            Debug.WriteLine($"Клик по клетке [{cellViewModel.Row}, {cellViewModel.Column}]");

            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel == null)
            {
                Debug.WriteLine("Ошибка: DataContext не MainWindowViewModel");
                return;
            }

            viewModel.MakeMove(cellViewModel.Row, cellViewModel.Column);

            e.Handled = true;
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("=== Новая игра ===");

            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.Initialize();
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("=== Сброс ===");

            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.Initialize();
            }
        }
    }
}