using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FifteenGame.Business.Models;
using FifteenGame.Data.Entities;
using FifteenGame.Wpf.ViewModels;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {

        public MainWindow(User user)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(user);
        }


        public MainWindow()
        {
            InitializeComponent();
            
        }

        private MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (ViewModel == null) return;

            switch (e.Key)
            {
                case Key.Up: ViewModel.Move(MoveDirection.Up); break;
                case Key.Down: ViewModel.Move(MoveDirection.Down); break;
                case Key.Left: ViewModel.Move(MoveDirection.Left); break;
                case Key.Right: ViewModel.Move(MoveDirection.Right); break;
            }
        }


        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.StartNewGame();
            this.Focus();
        }


        private void DirectionButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                if (Enum.TryParse(button.Tag.ToString(), out MoveDirection direction))
                {
                    ViewModel?.Move(direction);
                }
            }
            this.Focus();
        }

  
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.SaveGame();
            this.Focus();
        }
    }
}