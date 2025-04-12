using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class UserLoginWindowViewModel
    {
        private readonly IUserService _userService;

        private UserModel _user;

        public string UserName { get; set; }

        internal MainWindowViewModel MainViewModel { get; set; }

        public UserLoginWindowViewModel()
        {
            _userService = new UserService();
        }

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
