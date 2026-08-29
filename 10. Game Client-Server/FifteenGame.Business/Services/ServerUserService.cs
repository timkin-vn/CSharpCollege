using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Services;
using FifteenGame.DataAccess.Repositories;

namespace FifteenGame.Business.Services
{
    public class ServerUserService : IUserService
    {
        private readonly UserRepository _repo = new UserRepository();

        public UserDto GetGlobalTopPlayer() => _repo.GetTopPlayer();
        public UserDto Login(string username, string password) => _repo.GetByLogin(username, password);
        public UserDto Register(string username, string password) => _repo.Create(username, password);
    }
}