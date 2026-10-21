using MemoryGame.Wpf.ViewModels;
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
        }
    }
}
