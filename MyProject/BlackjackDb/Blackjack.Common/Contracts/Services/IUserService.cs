using Blackjack.Common.Models;

namespace Blackjack.Common.Contracts.Services
{
    public interface IUserService
    {
        UserModel GetOrCreateUser(string name);
    }
}
