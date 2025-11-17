using FifteenGame.Business.Services;
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
        private readonly IUserService _userService;

        private UserModel _model;

        public string UserName { get; set; }

        internal MainWindowViewModel MainViewModel { get; set; }

        public UserLoginWindowViewModel()
        {
            _userService = NinjectKernel.Instance.Get<IUserService>();
        }

        public bool FindUser()
        {
            _model = _userService.GetUserByName(UserName);
            return _model != null;
        }

        public void CreateUser()
        {
            _model = _userService.GetOrCreateUser(UserName);
        }

        public void SaveUser()
        {
            MainViewModel.SetUser(_model);
        }
    }
}
