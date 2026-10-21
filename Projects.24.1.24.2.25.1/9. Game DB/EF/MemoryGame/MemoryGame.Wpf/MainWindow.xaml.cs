using MemoryGame.Wpf.ViewModels;
using MemoryGame.Wpf.Views;
using System.Windows;
using System.Windows.Input;

namespace MemoryGame.Wpf
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
            var cell = (CellViewModel)((FrameworkElement)sender).DataContext;
            ViewModel.MakeMove(cell.Row, cell.Column, OnGameFinished);
        }

        private void OnGameFinished()
        {
            if (MessageBox.Show($"Все пары найдены за {ViewModel.MoveCount} попыток. Повторить?", "Победа!",
                MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
            }
            else
            {
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dialog = new UserLoginWindow();
            dialog.ViewModel.MainViewModel = ViewModel;
            if (dialog.ShowDialog() != true)
            {
                Close();
            }
        }
    }
}
