using Pacman.Common.Dtos;
using Pacman.Common.Services;
using Pacman.Data.Repositories;

namespace Pacman.Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repo;

        public UserService(UserRepository repo)
        {
            _repo = repo;
        }

        public UserDto Login(string username)
        {
            return _repo.GetOrCreate(username);
        }

        public void UpdateScore(ScoreRequest request)
        {
            _repo.UpdateScore(request.UserId, request.Score);
        }
    }
}