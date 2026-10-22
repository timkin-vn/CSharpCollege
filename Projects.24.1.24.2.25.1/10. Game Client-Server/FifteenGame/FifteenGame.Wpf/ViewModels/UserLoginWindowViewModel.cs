using CheckersGame.Business.Contracts;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using System;
using GameModel = CheckersGame.Business.Models.GameModel;

namespace FifteenGame.Wpf.ViewModels
{
    public class UserLoginWindowViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly ICheckersGameService _checkersGameService;

        private UserModel _userModel;

        public string UserName { get; set; }

        public event Action<GameModel> UserLoggedIn;

        // Конструктор по умолчанию (для дизайнера)
        public UserLoginWindowViewModel()
        {
        }

        // Основной конструктор, зависимости подставляет Ninject
        public UserLoginWindowViewModel(IUserService userService, ICheckersGameService checkersGameService)
        {
            _userService = userService;
            _checkersGameService = checkersGameService;
        }

        public bool FindUser()
        {
            if (_userService == null)
                throw new InvalidOperationException("Сервис пользователей не инициализирован.");
            _userModel = _userService.GetUserByName(UserName);
            return _userModel != null;
        }

        public void CreateUser()
        {
            if (_userService == null)
                throw new InvalidOperationException("Сервис пользователей не инициализирован.");
            _userModel = _userService.GetOrCreateUser(UserName);
        }

        public void CommitUser()
        {
            if (_userModel == null)
                throw new InvalidOperationException("Пользователь не выбран.");
            if (_checkersGameService == null)
                throw new InvalidOperationException("Сервис игры не инициализирован.");

            // Продолжаем последнюю незаконченную партию или начинаем новую
            GameModel model = _checkersGameService.GetLastActiveGame(_userModel.Id);
            if (model == null)
                model = _checkersGameService.CreateNewGame(_userModel.Id);

            UserLoggedIn?.Invoke(model);
        }
    }
}
