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
            DataContext = new MainWindowViewModel(); // Убедитесь, что DataContext установлен
        }

        private void Cell_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
            {
                var cellViewModel = grid.DataContext as CellViewModel;
                if (cellViewModel != null)
                {
                    // Здесь вы можете выполнить нужные действия с cellViewModel
                    // Например, сделать ход в игре
                    int row = cellViewModel.Row;
                    int column = cellViewModel.Column;

                    // Вызовите метод для обработки хода
                    (DataContext as MainWindowViewModel)?.MakeMove(row, column, () =>
                    {
                        // Действия при завершении игры
                        if (MessageBox.Show("Игра окончена. Повторить?", "Поздравляем!", MessageBoxButton.YesNo, MessageBoxImage.Information) ==
                        MessageBoxResult.Yes)
                        {
                            ViewModel.Initialize();
                        }
                    });
                }
            }
        }
        private void GameFinished()
        {
            if (MessageBox.Show("Игра окончена. Повторить?", "Поздравляем!", MessageBoxButton.YesNo, MessageBoxImage.Information) == 
                MessageBoxResult.Yes)
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
