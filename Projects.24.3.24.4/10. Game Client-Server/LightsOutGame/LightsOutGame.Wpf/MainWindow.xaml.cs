using LightsOutGame.Wpf.ViewModels;
using LightsOutGame.Wpf.Views;
using System.Windows;
using System.Windows.Input;

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

        private void Cell_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var cell = (CellViewModel)((FrameworkElement)sender).Tag;
            ViewModel.MakeMove(cell.Row, cell.Column, OnGameFinished);
        }

        private void OnGameFinished()
        {
            if (MessageBox.Show("Игра пройдена! Все огни погашены. Повторить?", "Поздравляем!", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
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
