using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
using System.Windows;
using System.Windows.Controls;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        internal MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is CellViewModel cell)
                ViewModel.SelectCell(cell);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dialog = new UserLoginWindow();
            dialog.ViewModel.MainViewModel = ViewModel;
            dialog.ShowDialog();
        }
    }
}