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
using Game2048.Business.Models;
using Game2048.Wpf.ViewModels;

namespace Game2048.WPF
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
            MoveDirection direction = MoveDirection.None;

            switch (e.Key)
            {
                case Key.Left:
                    direction = MoveDirection.Left;
                    break;
                case Key.Right:
                    direction = MoveDirection.Right;
                    break;
                case Key.Up:
                    direction = MoveDirection.Up;
                    break;
                case Key.Down:
                    direction = MoveDirection.Down;
                    break;
            }

            if (direction != MoveDirection.None)
            {
                ViewModel.MakeMove(direction, GameFinished);
            }
        }

        private void GameFinished()
        {
            if (MessageBox.Show("Игра окончена", "Конец игры", MessageBoxButton.YesNo, MessageBoxImage.Information) ==
                MessageBoxResult.Yes)
            {
                ViewModel.Initialize();
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Initialize();
        }
    }
}
