using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Окно загружено. PlayerCells: " + ViewModel.PlayerCells.Count + ", ComputerCells: " + ViewModel.ComputerCells.Count);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid grid && grid.DataContext is CellViewModel cell)
            {
                ViewModel.MakeAttack(cell.Row, cell.Column, GameFinished);
            }
        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid grid && grid.DataContext is CellViewModel cell)
            {
                ViewModel.ToggleFlag(cell.Row, cell.Column);
            }
        }

        private void GameFinished()
        {
            string message = ViewModel.PlayerShipsLeft == 0 ? "Компьютер победил!" : "Вы победили!";
            if (MessageBox.Show(message + " Начать заново?", "Конец игры", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
            }
        }
    }
}