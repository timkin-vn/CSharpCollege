using System.Windows;
using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dialog = new UserLoginWindow { Owner = this };
            dialog.ViewModel.MainViewModel = (MainWindowViewModel)DataContext;

            if (dialog.ShowDialog() != true)
            {
                Close();
            }
        }
    }
}
