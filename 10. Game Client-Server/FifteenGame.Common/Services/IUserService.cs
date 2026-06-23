using FifteenGame.Common.BusinessDtos;

namespace FifteenGame.Common.Services
{
    public interface IUserService
    {
        UserDto Register(string username, string password);
        UserDto Login(string username, string password);
        UserDto GetGlobalTopPlayer();
    }
}