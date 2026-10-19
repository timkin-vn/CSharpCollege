using FifteenGame.Wpf.ViewModels;
using System;
using System.Windows;

namespace FifteenGame.Wpf.Views
{
    public partial class UserLoginWindow : Window
    {
        public UserLoginWindowViewModel ViewModel =>
            (UserLoginWindowViewModel)DataContext;

        public UserLoginWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ViewModel.UserName))
            {
                MessageBox.Show(
                    "Введите имя пользователя.",
                    "Вход в систему",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            try
            {
                if (!ViewModel.FindUser())
                {
                    var result = MessageBox.Show(
                        "Пользователь не найден. Создать нового?",
                        "Вход в систему",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result != MessageBoxResult.Yes)
                        return;

                    ViewModel.CreateUser();
                }

                ViewModel.CommitUser();

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось выполнить вход:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}