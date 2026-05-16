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
using FifteenGames.Common.BusinessModels;
using FifteenGames.Wpf.ViewModels;

namespace FifteenGames.Wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel => (MainViewModel)DataContext;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var direction = (MoveDirection)((FrameworkElement)sender).Tag;
            ViewModel.MakeMove(direction, OnGameFinished);
        }
        private void OnGameFinished()
        {
            if (MessageBox.Show("Игра окончкна, Повторить?", "Поздравляем!", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
            }
        }
    }
}
