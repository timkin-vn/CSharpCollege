using FifteenGame.Common.Models;

namespace FifteenGame.Common.Contracts
{
    public interface IUserService
    {
        UserModel GetOrCreateUser(string name);
    }
}