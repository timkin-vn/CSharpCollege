using FifteenGame.Common.BusinessModels;
using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
using Npgsql;
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
        private Point _startPoint;

        internal MainWindowViewModel ViewModel
        {
            get { return (MainWindowViewModel)DataContext; }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ShowLoginOrClose();
        }

        private void ShowLoginOrClose()
        {
            if (ViewModel.IsLoggedIn)
                return;

            var dialog = new UserLoginWindow(ViewModel);
            var result = dialog.ShowDialog();

            if (result != true)
                Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Logout();
            ShowLoginOrClose();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.IsLoggedIn)
                return;

            ViewModel.NewGame();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Field_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(this);
        }

        private void Field_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!ViewModel.IsLoggedIn)
                return;

            var endPoint = e.GetPosition(this);

            double dx = endPoint.X - _startPoint.X;
            double dy = endPoint.Y - _startPoint.Y;

            if (Math.Abs(dx) < 30 && Math.Abs(dy) < 30)
                return;

            MoveDirection direction;

            if (Math.Abs(dx) > Math.Abs(dy))
                direction = dx > 0 ? MoveDirection.Right : MoveDirection.Left;
            else
                direction = dy > 0 ? MoveDirection.Down : MoveDirection.Up;

            ViewModel.MakeMove(direction, OnGameFinished);
        }

        private void OnGameFinished()
        {
            if (MessageBox.Show("Игра окончена. Начать новую игру?", "Конец игры",
                MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                ViewModel.NewGame();
            }
        }
    }
}