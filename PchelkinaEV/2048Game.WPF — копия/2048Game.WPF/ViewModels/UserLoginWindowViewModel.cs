using _2048Game.Business.Services;
using _2048Game.Common.BusinessModels;
using _2048Game.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game.WPF.ViewModels
{
    public class UserLoginWindowViewModel
    {
        private readonly IUserService _userService;
        private UserModel _userModel;

        public string UserName { get; set; }
        internal MainWindowViewModel MainViewModel { get; set; }

        public UserLoginWindowViewModel()
        {
            _userService = new UserService();
        }

        public bool FindUser()
        {
            _userModel = _userService.GetUserByName(UserName);
            return _userModel != null;
        }

        public void CreateUser()
        {
            _userModel = _userService.GetOrCreateUser(UserName);
        }
        public void SaveUser()
        {
            MainViewModel.SetUser(_userModel);
        }
    }
}
