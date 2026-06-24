using FifteenGame.Common.BusinessModels;
using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dialog = new UserLoginWindow();
            dialog.ViewModel.MainViewModel = ViewModel;
            dialog.ShowDialog();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left: ViewModel.MakeMove(MoveDirection.Left, OnGameFinished); break;
                case Key.Right: ViewModel.MakeMove(MoveDirection.Right, OnGameFinished); break;
                case Key.Up: ViewModel.MakeMove(MoveDirection.Up, OnGameFinished); break;
                case Key.Down: ViewModel.MakeMove(MoveDirection.Down, OnGameFinished); break;
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NewGame();
        }

        private void OnGameFinished()
        {
            if (MessageBox.Show("Игра завершена. Хотите начать новую?", "Игра окончена",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ViewModel.NewGame();
            }
        }
    }
}