using System;
using System.Windows;
using FifteenGame.Wpf.ViewModels;

namespace FifteenGame.Wpf
{
    /// <summary>
    /// Логика взаимодействия для TicTacToeWindow.xaml
    /// </summary>
    public partial class TicTacToeWindow : Window
    {
        public TicTacToeWindow()
        {
            InitializeComponent();
            DataContext = new TicTacToeViewModel();
            
            // Завершаем приложение при закрытии окна
            Closed += (s, e) => Application.Current.Shutdown();
        }
    }
} 