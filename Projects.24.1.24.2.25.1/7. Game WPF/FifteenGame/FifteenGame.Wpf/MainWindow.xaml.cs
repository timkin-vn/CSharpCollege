using System;
using System.Windows;
using System.Windows.Input;
using Game2048.Wpf.Models;
using Game2048.Wpf.ViewModels;

namespace Game2048.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            MoveDirection? direction = null;

            switch (e.Key)
            {
                case Key.Up:
                    direction = MoveDirection.Up;
                    break;
                case Key.Down:
                    direction = MoveDirection.Down;
                    break;
                case Key.Left:
                    direction = MoveDirection.Left;
                    break;
                case Key.Right:
                    direction = MoveDirection.Right;
                    break;
            }

            if (direction.HasValue)
            {
                _viewModel.HandleKeyPress(direction.Value);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RestartGame();
        }

        // Обработчики кликов для 4 кнопок-стрелок на форме
        private void BtnMoveUp_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.HandleKeyPress(MoveDirection.Up);
        }

        private void BtnMoveLeft_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.HandleKeyPress(MoveDirection.Left);
        }

        private void BtnMoveDown_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.HandleKeyPress(MoveDirection.Down);
        }

        private void BtnMoveRight_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.HandleKeyPress(MoveDirection.Right);
        }
    }
}
