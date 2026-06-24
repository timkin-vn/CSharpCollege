using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
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
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is CellViewModel cellViewModel)
            {
                ViewModel.ProcessCellClick(cellViewModel, OnGameFinished);
            }
        }

        private void OnGameFinished(GameResult result)
        {
            string message = result == GameResult.Win
                ? "Поздравляем! Ваш бизнес выжил и успешно накормил город!"
                : "Банкротство! Вы ушли в минус. Шаурмичные закрыты.";

            string title = result == GameResult.Win ? "Победа!" : "Игра окончена";
            MessageBoxImage icon = result == GameResult.Win ? MessageBoxImage.Information : MessageBoxImage.Error;

            if (MessageBox.Show($"{message}\nХотите открыть новую сеть?", title, MessageBoxButton.YesNo, icon) == MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
