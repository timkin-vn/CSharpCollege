using Minesweeper.Wpf.ViewModels;
using Minesweeper.Wpf.DataAccess;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Minesweeper.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            var cell = (CellViewModel)((Button)sender).Tag;
            ViewModel.RevealCell(cell.Row, cell.Column, OnGameFinished);
        }

        private void Cell_RightClick(object sender, MouseButtonEventArgs e)
        {
            var cell = (CellViewModel)((Button)sender).Tag;
            ViewModel.ToggleFlag(cell.Row, cell.Column);
            e.Handled = true;
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Initialize();
        }

        private void OnGameFinished(bool won)
        {
            try
            {
                new GameResultRepository().SaveResult(won, ViewModel.Moves);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить результат в PostgreSQL. Проверьте подключение и SQL-скрипт.\\n\\n" + ex.Message,
                    "Ошибка базы данных", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            string message = won
                ? "Поздравляем! Вы открыли все безопасные клетки."
                : "Вы открыли мину. Попробовать ещё раз?";

            var result = MessageBox.Show(message + "\n\nНачать новую игру?",
                won ? "Победа!" : "Игра окончена",
                MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
                ViewModel.Initialize();
        }
    }
}
