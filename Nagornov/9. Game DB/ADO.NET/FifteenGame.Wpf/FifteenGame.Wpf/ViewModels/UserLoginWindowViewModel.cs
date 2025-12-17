using FifteenGame.Common.BusinessModels;
using System;
using System.Windows;

namespace FifteenGame.Wpf.ViewModels
{
    public class UserLoginWindowViewModel
    {
        private UserModel _userModel;

        public string UserName { get; set; }

        internal MainWindowViewModel MainViewModel { get; set; }

        public bool FindUser()
        {
            _userModel = new UserModel
            {
                Id = 1,
                Name = UserName
            };
            return true;
        }

        public void CreateUser()
        {
            _userModel = new UserModel
            {
                Id = 1,
                Name = UserName
            };
        }

        public void SaveUser()
        {
            try
            {
                MainViewModel?.SetUser(_userModel);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}