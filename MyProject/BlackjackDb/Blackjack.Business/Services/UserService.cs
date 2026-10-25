using Blackjack.Common.Contracts.Repositories;
using Blackjack.Common.Contracts.Services;
using Blackjack.Common.Models;
using System;

namespace Blackjack.Business.Services
{
    public class UserService : IUserService
    {
        public const int MaxNameLength = 50;

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel GetOrCreateUser(string name)
        {
            name = name?.Trim();

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Введите имя игрока.");
            }

            if (name.Length > MaxNameLength)
            {
                throw new ArgumentException($"Имя не длиннее {MaxNameLength} символов.");
            }

            return _userRepository.GetByName(name) ?? _userRepository.Create(name);
        }
    }
}
