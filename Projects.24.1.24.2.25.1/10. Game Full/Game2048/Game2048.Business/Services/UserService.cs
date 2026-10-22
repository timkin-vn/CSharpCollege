using Game2048.Common.BusinessModels;
using Game2048.Common.Contracts.Repositories;
using Game2048.Common.Contracts.Services;

namespace Game2048.Business.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<UserModel> GetAllUsers()
        => _userRepository.GetAll();

    public UserModel GetOrCreateUser(string userName)
    {
        var existing = _userRepository.GetByName(userName);
        return existing ?? _userRepository.Create(userName);
    }

    public UserModel? GetUserByName(string userName)
        => _userRepository.GetByName(userName);
}
