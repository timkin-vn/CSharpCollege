using Nonogram.Common.BusinessModels;
using Nonogram.Common.Infrastructure;
using Nonogram.Common.Services;
using Ninject;
using System;

namespace Nonogram.Wpf.ViewModels
{
    public class UserLoginWindowViewModel
    {
        private readonly IUserService _userService;
        private UserModel _userModel;

        public UserLoginWindowViewModel()
        {
            try
            {
                // Получаем сервис через Ninject
                if (NinjectKernel.Instance != null)
                {
                    _userService = NinjectKernel.Instance.Get<IUserService>();
                    Console.WriteLine("UserLoginWindowViewModel: UserService obtained");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserLoginWindowViewModel constructor: {ex.Message}");
            }
        }

        public string UserName { get; set; }

        internal MainWindowViewModel MainViewModel { get; set; }

        public bool FindUser()
        {
            if (_userService == null)
            {
                Console.WriteLine("UserService is null!");
                return false;
            }

            _userModel = _userService.GetUserByName(UserName);
            return _userModel != null;
        }

        public void CreateUser()
        {
            if (_userService == null)
            {
                Console.WriteLine("UserService is null!");
                return;
            }

            _userModel = _userService.GetOrCreateUser(UserName);
        }

        public void SaveUser()
        {
            if (MainViewModel != null && _userModel != null)
            {
                MainViewModel.SetUser(_userModel);
            }
        }
    }
}