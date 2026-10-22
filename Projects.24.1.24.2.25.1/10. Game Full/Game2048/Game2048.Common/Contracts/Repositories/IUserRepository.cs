using Game2048.Common.BusinessModels;

namespace Game2048.Common.Contracts.Repositories;

public interface IUserRepository
{
    IEnumerable<UserModel> GetAll();
    UserModel? GetByName(string name);
    UserModel Create(string name);
}
