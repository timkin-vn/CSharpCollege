// FifteenGame.Wpf/MainWindow.xaml.cs
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
                ViewModel.MakeMoveAt(cell.Row, cell.Column, OnGameFinished, OnGameLost);
            }
        }

        private void OnGameFinished()
        {
            if (MessageBox.Show("Вы погасили все лампочки! Повторить?", "Победа!", MessageBoxButton.YesNo, MessageBoxImage.Information)
                == MessageBoxResult.Yes)
            {
                ViewModel.ReInitialize();
            }
        }

        private void OnGameLost()
        {
            if (MessageBox.Show("У вас закончились ходы! Попробовать снова?", "Поражение", MessageBoxButton.YesNo, MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            {
                ViewModel.ReInitialize();
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