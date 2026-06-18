using System;
using System.Windows;
using FifteenGame;

namespace FifteenGame.Wpf
{
    /// <summary>
    /// Логика взаимодействия для GameSelectionWindow.xaml
    /// </summary>
    public partial class GameSelectionWindow : Window
    {
        public GameSelectionWindow()
        {
            InitializeComponent();
        }

        private void BtnFifteenGame_Click(object sender, RoutedEventArgs e)
        {
            // Запускаем игру "Пятнашки"
            var fifteenGameWindow = new MainWindow();
            fifteenGameWindow.Show();
        }

        private void BtnTicTacToe_Click(object sender, RoutedEventArgs e)
        {
            // Запускаем игру "Крестики-нолики"
            var ticTacToeWindow = new TicTacToeWindow();
            ticTacToeWindow.Show();
        }
    }
} 