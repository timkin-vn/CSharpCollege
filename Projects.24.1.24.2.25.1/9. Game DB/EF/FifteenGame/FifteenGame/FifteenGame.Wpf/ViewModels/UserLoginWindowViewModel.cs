using CheckersGame.Wpf.ViewModels;
using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersGame.Business.Contracts;
using CheckersGame.Business.Models;
using Ninject;
using Ninject.Modules;
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

        // Конструктор по умолчанию для дизайнера
        public UserLoginWindowViewModel()
        {
        }

        // Основной конструктор с DI
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

            // Сначала пытаемся найти последнюю незавершённую игру
            GameModel model = _checkersGameService.GetLastActiveGame(_userModel.Id);
            if (model == null)
            {
                // Нет активной игры – создаём новую
                model = _checkersGameService.CreateNewGame(_userModel.Id);
                if (model == null)
                    throw new Exception("Не удалось создать новую игру.");
            }
            // Если игра найдена, model уже содержит загруженное состояние

            UserLoggedIn?.Invoke(model);
        }
    }
}