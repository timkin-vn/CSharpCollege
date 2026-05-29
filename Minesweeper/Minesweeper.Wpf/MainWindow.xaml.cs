using Minesweeper.Business.Models;
using Minesweeper.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minesweeper.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CellButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is CellViewModel cellViewModel)
            {
                ViewModel.HandleLeftClick(cellViewModel.Row, cellViewModel.Column, OnGameFinished);
            }
        }

        private void CellButton_RightClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button button && button.DataContext is CellViewModel cellViewModel)
            {
                e.Handled = true;
                ViewModel.HandleRightClick(cellViewModel.Row, cellViewModel.Column);
            }
        }

        private void OnGameFinished()
        {
            string message = "";
            string title = "";

            if (ViewModel.CurrentState == GameState.Won)
            {
                message = "Поздравляем! Вы выиграли!";
                title = "Победа!";
            }
            else if (ViewModel.CurrentState == GameState.Lost)
            {
                message = "Вы подорвались на мине!";
                title = "Проигрыш!";
            }

            if (MessageBox.Show(message + "\n\nНачать новую игру?", title,
                MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                ViewModel.NewGame();
            }
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NewGame();
        }
    }
}