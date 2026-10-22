using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel MainContext
        {
            get => DataContext as MainWindowViewModel;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnGridKeyPress(object sender, KeyEventArgs args)
        {
            if (MainContext == null) return;

            switch (args.Key)
            {
                case Key.A:
                case Key.Left:
                    MainContext.HandleDirection(MoveDirection.Left);
                    break;
                case Key.D:
                case Key.Right:
                    MainContext.HandleDirection(MoveDirection.Right);
                    break;
                case Key.W:
                case Key.Up:
                    MainContext.HandleDirection(MoveDirection.Up);
                    break;
                case Key.S:
                case Key.Down:
                    MainContext.HandleDirection(MoveDirection.Down);
                    break;
            }
        }

        private void OnRestartAction(object sender, RoutedEventArgs args)
        {
            MainContext?.RestartSession();
        }
    }
}