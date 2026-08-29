using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Dto;

namespace FifteenGame.Common.Services
{
    public interface IUserService
    {
        UserReply Login(UserNameRequest request);
        void UpdateBestTime(UserDto user);
    }
}