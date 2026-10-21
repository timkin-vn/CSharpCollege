using FifteenGame.Common.BusinessModels;
using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
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

namespace FifteenGame.Wpf
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

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var cell = (CellViewModel)((FrameworkElement)sender).DataContext;
            ViewModel.OpenCell(cell.Row, cell.Column);
        }

        private void OnGameFinished()
        {
            if (MessageBox.Show("Ира окончена. Повторить?", "Поздравляем!", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dialog = new UserLoginWindow();
            dialog.ViewModel.MainViewModel = ViewModel;
            dialog.ShowDialog();
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Initialize();
        }

        private void Colors_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SetTheme("Colors");
        }

        private void Animals_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SetTheme("Animals");
        }

        private void Fruits_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SetTheme("Fruits");
        }

        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SetDifficulty(4);
        }

        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SetDifficulty(6);
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SetDifficulty(8);
        }
    }
}
