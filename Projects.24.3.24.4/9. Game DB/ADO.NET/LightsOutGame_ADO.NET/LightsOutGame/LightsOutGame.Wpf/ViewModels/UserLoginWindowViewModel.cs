using LightsOutGame.Business.Services;
using LightsOutGame.Common.BusinessModels;
using LightsOutGame.Common.Contracts.Services;
using LightsOutGame.Common.Infrastucture;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.Wpf.ViewModels
{
    public class UserLoginWindowViewModel : ViewModelBase
    {
        private readonly IUserService _userService = NinjectKernel.Instance.Get<IUserService>();

        private UserModel _userModel;

        public string UserName { get; set; }

        internal MainWindowViewModel MainViewModel { get; set; }

        public bool FindUser()
        {
            _userModel = _userService.GetUserByName(UserName);
            return _userModel != null;
        }

        public void CreateUser()
        {
            _userModel = _userService.GetOrCreateUser(UserName);
        }

        public void CommitUser()
        {
            MainViewModel.CommitUser(_userModel);
        }
    }
}
