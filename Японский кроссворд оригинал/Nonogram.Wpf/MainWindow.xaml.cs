using Nonogram.Business.Models;
using Nonogram.Wpf.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nonogram.Wpf
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;

            _viewModel.GameOver += OnGameOver;
            _viewModel.GameWon += OnGameWon;
        }

        private void Cell_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton != MouseButton.Left)
            {

                e.Handled = true;
                return;
            }

            var grid = sender as Grid;
            if (grid == null) return;

            var cellViewModel = grid.Tag as CellViewModel;
            if (cellViewModel == null) return;


            _viewModel.MakeMove(cellViewModel.Row, cellViewModel.Column);

            e.Handled = true;
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            MessageBox.Show("Игра окончена! Вы сделали 5 ошибок.", "Поражение",
                MessageBoxButton.OK, MessageBoxImage.Information);
            _viewModel.Initialize();
        }

        private void OnGameWon(object sender, EventArgs e)
        {
            MessageBox.Show("Поздравляем! Вы решили кроссворд!", "Победа",
                MessageBoxButton.OK, MessageBoxImage.Information);
            _viewModel.Initialize();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Initialize();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Initialize();
        }
    }
}