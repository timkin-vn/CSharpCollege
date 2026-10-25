using Blackjack.Common.Models;

namespace Blackjack.Common.Contracts.Repositories
{
    public interface IUserRepository
    {
        UserModel GetByName(string name);

        UserModel Create(string name);
    }
}
