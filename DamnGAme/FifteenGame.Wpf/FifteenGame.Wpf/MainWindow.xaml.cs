using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid grid && grid.DataContext is CellViewModel cell)
            {
                ViewModel.MakeAttack(cell.Row, cell.Column, PlayerWon, ComputerWon);
            }
        }

        private void PlayerWon()
        {
            if (MessageBox.Show("Вы победили! Начать заново?", "Победа!", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
            }
        }

        private void ComputerWon()
        {
            if (MessageBox.Show("Компьютер победил! Начать заново?", "Проигрыш", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
            }
        }
    }
}