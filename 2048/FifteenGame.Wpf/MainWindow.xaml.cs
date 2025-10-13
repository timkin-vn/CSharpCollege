using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        private Point _startPoint;
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainWindowViewModel)DataContext;
        }

        private void GameField_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(this);
        }

        private void GameField_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Point endPoint = e.GetPosition(this);
            double dx = endPoint.X - _startPoint.X;
            double dy = endPoint.Y - _startPoint.Y;
            double threshold = 40;

            if (Math.Abs(dx) < threshold && Math.Abs(dy) < threshold)
                return;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                if (dx > 0)
                    _viewModel.MakeMove(MoveDirection.Right, OnGameFinished);
                else
                    _viewModel.MakeMove(MoveDirection.Left, OnGameFinished);
            }
            else
            {
                if (dy > 0)
                    _viewModel.MakeMove(MoveDirection.Down, OnGameFinished);
                else
                    _viewModel.MakeMove(MoveDirection.Up, OnGameFinished);
            }
        }

        private void OnGameFinished()
        {
            if (MessageBox.Show("Игра окончена. Повторить?", "Поздравляем!", MessageBoxButton.YesNo, MessageBoxImage.Information) ==
                MessageBoxResult.Yes)
            {
                _viewModel.Initialize();
            }
        }
    }
}
