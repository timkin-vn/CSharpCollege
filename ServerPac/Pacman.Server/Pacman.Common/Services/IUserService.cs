using Pacman.Common.Dtos;

namespace Pacman.Common.Services
{
    public interface IUserService
    {
        UserDto Login(string username);
        void UpdateScore(ScoreRequest request);
    }
}