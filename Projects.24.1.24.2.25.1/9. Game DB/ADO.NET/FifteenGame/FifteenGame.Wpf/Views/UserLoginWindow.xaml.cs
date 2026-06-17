using FifteenGame.Wpf.ViewModels;
using System.Windows;

namespace FifteenGame.Wpf.Views
{
    public partial class UserLoginWindow : Window
    {
        public UserLoginWindowViewModel ViewModel => (UserLoginWindowViewModel)DataContext;

        public UserLoginWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ViewModel.UserName))
                {
                    MessageBox.Show("Введите имя пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!ViewModel.FindUser())
                {
                    if (MessageBox.Show($"Пользователь '{ViewModel.UserName}' не найден. Создать нового?",
                        "Создание пользователя", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        ViewModel.CreateUser();
                    }
                    else
                    {
                        return;
                    }
                }

                ViewModel.CommitUser();
                DialogResult = true;
                Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}