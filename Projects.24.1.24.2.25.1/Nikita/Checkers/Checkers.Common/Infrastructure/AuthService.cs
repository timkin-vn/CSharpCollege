using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Common.Contracts;

namespace Checkers.Common.Infrastructure
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> RegisterNewUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return "Имя пользователя было пустым";
            if (await _userRepository.UserExistsAsync(username)) return "Имя пользователя уже занято. Пожалуйста, выберите другое.";
            bool isRegistered = await _userRepository.UserExistsAsync(username);
            return isRegistered ? "Регистрация прошла успешно!" : "Произошла ошибка на сервере. Попробуйте позже.";
        }
    }
}
