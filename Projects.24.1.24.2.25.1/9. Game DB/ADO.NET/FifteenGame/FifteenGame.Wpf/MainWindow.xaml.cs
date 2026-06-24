using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel =>
            (MainWindowViewModel)DataContext;

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
                case Key.Left:
                    ViewModel.MoveLeft();
                    break;

                case Key.Right:
                    ViewModel.MoveRight();
                    break;

                case Key.Up:
                    ViewModel.MoveUp();
                    break;

                case Key.Down:
                    ViewModel.MoveDown();
                    break;
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NewGame();
        }
    }
}