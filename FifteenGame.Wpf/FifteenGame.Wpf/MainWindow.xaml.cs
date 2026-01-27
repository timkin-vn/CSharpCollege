using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
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

        private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            UpdateScore();
        }

        // НАЖАЛИ МЫШЬ
        private void Field_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(this);
        }

        // ОТПУСТИЛИ МЫШЬ → ОПРЕДЕЛЯЕМ СВАЙП
        private void Field_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var endPoint = e.GetPosition(this);

            double dx = endPoint.X - _startPoint.X;
            double dy = endPoint.Y - _startPoint.Y;

            // минимальная длина свайпа
            if (Math.Abs(dx) < 30 && Math.Abs(dy) < 30)
                return;

            MoveDirection direction;

            if (Math.Abs(dx) > Math.Abs(dy))
                direction = dx > 0 ? MoveDirection.Right : MoveDirection.Left;
            else
                direction = dy > 0 ? MoveDirection.Down : MoveDirection.Up;

            ViewModel.MakeMove(direction, OnGameFinished);
            UpdateScore();
        }

        private void UpdateScore()
        {
            ScoreText.Text = $"Score: {ViewModel.Score}";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnGameFinished()
        {
            string title = ViewModel.IsWin ? "Победа!" : "Игра окончена";
            string msg = ViewModel.IsWin
                ? "Вы собрали 2048! Сыграть ещё раз?"
                : "Ходов больше нет. Сыграть ещё раз?";

            if (MessageBox.Show(msg, title, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
                UpdateScore();
            }
        }
    }
}