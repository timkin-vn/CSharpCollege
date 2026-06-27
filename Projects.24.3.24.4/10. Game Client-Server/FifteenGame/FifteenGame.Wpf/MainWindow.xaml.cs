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
            // Получаем элемент, по которому кликнули (это наша сетка или кнопка клетки)
            var element = sender as FrameworkElement;
            if (element == null) return;

            // Из DataContext элемента вытаскиваем CellViewModel, которая хранит координаты клетки
            var cellViewModel = element.DataContext as CellViewModel;
            if (cellViewModel == null) return;

            // Передаем координаты row и column, а также метод окончания игры OnGameFinished
            ViewModel.MakeMove(cellViewModel.Row, cellViewModel.Column, OnGameFinished);
        }

        private void OnGameFinished()
        {
            if (MessageBox.Show("Игра окончена. Повторить?", "Раунд завершен!", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                // Используем новый метод сброса игры, который мы добавили во вью-модель
                ViewModel.RestartGame();
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