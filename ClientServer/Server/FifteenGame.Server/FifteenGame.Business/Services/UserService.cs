using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Dto;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using Newtonsoft.Json;
using System.Text.Json; 
using FifteenGame.Common.BusinessModels;
namespace FifteenGame.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserReply Login(UserNameRequest request)
        {
            var user = _userRepository.GetUserByName(request.Username);
            if (user == null)
            {
                user = new UserDto { Username = request.Username, BestTimeSeconds = null };
                _userRepository.SaveUser(user);
                user = _userRepository.GetUserByName(request.Username);
            }

            bool hasSavedGame = _userRepository.HasSavedGame(user.Id);

            return new UserReply
            {
                User = user,
                HasSavedGame = hasSavedGame
            };
        }

        public void UpdateBestTime(UserDto userDto)
        {
            _userRepository.SaveUser(userDto);
        }

        
        public void SaveCurrentGame(int userId, GameModel gameModel)
        {
            _userRepository.SaveCurrentGameAsSaved(userId, gameModel);
        }

        public void ClearSavedGameAfterFinish(int userId)
        {
            _userRepository.ClearSavedGame(userId);
        }
        public GameModel LoadSavedGame(int userId)
        {
            return _userRepository.LoadSavedGame(userId);
        }
    }
}
