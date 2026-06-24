using Game2048.Common.BusinessModels;

namespace Game2048.Common.Contracts.Services;

public interface IUserService
{
    IEnumerable<UserModel> GetAllUsers();
    UserModel GetOrCreateUser(string userName);
    UserModel? GetUserByName(string userName);
}
