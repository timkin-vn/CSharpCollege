using LightsOutGame.Wpf.ViewModels;
using LightsOutGame.Wpf.Views;
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

namespace LightsOutGame.Wpf
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
            var cell = (CellViewModel)((FrameworkElement)sender).Tag;
            ViewModel.PressCell(cell, OnGameFinished);
        }

        private void OnGameFinished()
        {
            if (MessageBox.Show("Все лампы погашены. Сыграть ещё раз?", "Поздравляем!", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
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
    }
}
