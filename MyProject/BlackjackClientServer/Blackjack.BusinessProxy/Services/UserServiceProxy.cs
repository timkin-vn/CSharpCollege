using Blackjack.BusinessProxy.Infrastructure;
using Blackjack.Common.Contracts.Services;
using Blackjack.Common.Dtos;
using Blackjack.Common.Models;
using System;

namespace Blackjack.BusinessProxy.Services
{
    public class UserServiceProxy : IUserService
    {
        public UserModel GetOrCreateUser(string name)
        {
            name = name?.Trim();
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Введите имя игрока.");
            }

            return HttpConnection.Post<UserModel>("api/users/login", new UserNameRequest { Name = name });
        }
    }
}
