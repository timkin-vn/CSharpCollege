using Blackjack.Common.Contracts.Services;
using Blackjack.Common.Models;
using System;
using System.Windows;

namespace Blackjack.Wpf.Views
{
    public partial class UserLoginWindow : Window
    {
        private readonly IUserService _userService;

        public UserLoginWindow(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
            Loaded += (s, e) => NameTextBox.Focus();
        }

        public UserModel User { get; private set; }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User = _userService.GetOrCreateUser(NameTextBox.Text);
                DialogResult = true;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Вход", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка входа: " + ex.Message, "Вход", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
