using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Wpf.Infrastructure;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class UserLoginWindowViewModel
    {
        private readonly UserService _userService;

        public string UserName { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public UserModel CurrentUser { get; private set; }

        public ICommand LoginCommand { get; }
        public ICommand CancelCommand { get; }

        public UserLoginWindowViewModel(UserService userService)
        {
            _userService = userService;

            LoginCommand = new RelayCommand(Login);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Login(object parameter)
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                ErrorMessage = "Please enter your name";
                return;
            }

            try
            {
                CurrentUser = _userService.GetOrCreateUser(UserName.Trim());
                var window = Application.Current.Windows.OfType<Window>()
                    .FirstOrDefault(w => w.DataContext == this);

                if (window != null)
                {
                    window.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error creating user: {ex.Message}";
            }
        }

        private void Cancel(object parameter)
        {
            var window = Application.Current.Windows.OfType<Window>()
                .FirstOrDefault(w => w.DataContext == this);

            if (window != null)
            {
                window.DialogResult = false;
            }
        }
    }
}