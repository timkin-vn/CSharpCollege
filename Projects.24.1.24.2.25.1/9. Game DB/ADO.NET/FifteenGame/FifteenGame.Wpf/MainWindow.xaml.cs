using FifteenGame.Common.BusinessModels;
using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel =>
            (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(
            object sender,
            MouseButtonEventArgs e)
        {
            if (!(sender is FrameworkElement element))
                return;

            if (!ViewModel.HasGame || element.Tag == null)
                return;

            var direction = (MoveDirection)element.Tag;

            if (direction == MoveDirection.None)
                return;

            ViewModel.MakeMove(direction, OnGameFinished);
        }

        private void OnGameFinished()
        {
            MessageBox.Show(
                "Поздравляем! Вы собрали пятнашки.",
                "Игра окончена",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void Window_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            if (ViewModel.HasUser)
                return;

            if (!ShowLoginDialog())
                Close();
        }

        private bool ShowLoginDialog()
        {
            var dialog = new UserLoginWindow
            {
                Owner = this
            };

            dialog.ViewModel.MainViewModel = ViewModel;

            return dialog.ShowDialog() == true;
        }

        private void SwitchUserButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            ShowLoginDialog();
        }

        private void DeleteGameButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (!ViewModel.HasGame)
                return;

            var result = MessageBox.Show(
                "Удалить текущую партию?\n" +
                "Пользователь и другие сохранённые партии останутся.",
                "Удаление партии",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                ViewModel.DeleteCurrentGame();

                MessageBox.Show(
                    "Партия удалена.",
                    "Готово",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось удалить партию:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void DeleteUserButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (!ViewModel.HasUser)
                return;

            string userName = ViewModel.UserName;

            var result = MessageBox.Show(
                "Удалить пользователя \"" + userName + "\"?\n\n" +
                "Все его сохранённые партии будут удалены. " +
                "Это действие необратимо.",
                "Удаление пользователя",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                ViewModel.DeleteCurrentUser();

                MessageBox.Show(
                    "Пользователь и его партии удалены.",
                    "Готово",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                if (!ShowLoginDialog())
                    Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось удалить пользователя:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}