using FifteenGame.Business.Services;
using FifteenGame.Data.Entities;
using FifteenGame.Wpf.Commands;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService = new AuthService();
        public event Action<User> OnLoginSuccess;

        public string Username { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<object>(Login);
            RegisterCommand = new RelayCommand<object>(Register);
        }

        private void Login(object parameter)
        {
            try
            {
                var passwordBox = parameter as PasswordBox;
                var user = _authService.Login(Username, passwordBox.Password);
                OnLoginSuccess?.Invoke(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Register(object parameter)
        {
            try
            {
                var passwordBox = parameter as PasswordBox;
                _authService.Register(Username, passwordBox.Password);
                MessageBox.Show("Регистрация успешна! Теперь выполните вход.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}