using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game2048.Common.BusinessModels;
using Game2048.Common.Infrastructure;
using Game2048.Common.Services;
using Ninject;

namespace Game2048.WPF.ViewModels
{
    public class UserLoginWindowViewModel
    {
        private IUserService _userService;

        private UserModel _user;

        public UserLoginWindowViewModel()
        {
            _userService = NinjectKernel.Instance.Get<IUserService>();
        }

        public string UserName { get; set; }

        public MainWindowViewModel MainViewModel { get; set; }

        public bool FindUser()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }

            _user = _userService.GetByName(UserName);
            return _user != null;
        }

        public void CreateUser()
        {
            _user = _userService.Create(UserName);
        }

        public void SaveUser()
        {
            MainViewModel.SetUser(_user);
        }
    }
}
