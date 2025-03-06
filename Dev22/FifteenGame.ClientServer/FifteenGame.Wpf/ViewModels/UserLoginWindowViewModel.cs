using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class UserLoginWindowViewModel
    {
        private readonly IUserService _userService = NinjectKernel.Instance.Get<IUserService>();

        private UserModel _user;

        public string UserName { get; set; }

        internal MainWindowViewModel MainViewModel { get; set; }

        public bool FindUser()
        {
            _user = _userService.GetUserByName(UserName);
            return _user != null;
        }

        public void CreateUser()
        {
            _user = _userService.GetOrCreateUser(UserName);
        }

        public void SaveUser()
        {
            MainViewModel.SetUser(_user);
        }
    }
}
