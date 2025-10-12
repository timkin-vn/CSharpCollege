using System;
using System.Windows;
using System.Windows.Input;
using LightsOut.Wpf.ViewModels;


namespace LightsOut.Wpf
{
    public partial class MainWindow : Window
    {
        internal MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var fe = (FrameworkElement)sender;
            if (fe?.DataContext is CellViewModel cell)
            {
                ViewModel.MakeMoveAt(cell.Row, cell.Column, OnGameFinished);
            }
        }

        private void OnGameFinished()
        {
            if (MessageBox.Show("Вы погасили все лампочки! Повторить?", "Победа!", MessageBoxButton.YesNo, MessageBoxImage.Information) ==
                MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
            }
        }
    }
}
