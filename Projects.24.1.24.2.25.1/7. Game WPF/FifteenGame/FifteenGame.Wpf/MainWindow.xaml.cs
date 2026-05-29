using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    ViewModel.MakeMove(MoveDirection.Left);
                    break;
                case Key.Right:
                    ViewModel.MakeMove(MoveDirection.Right);
                    break;
                case Key.Up:
                    ViewModel.MakeMove(MoveDirection.Up);
                    break;
                case Key.Down:
                    ViewModel.MakeMove(MoveDirection.Down);
                    break;
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NewGame();
        }
    }
}