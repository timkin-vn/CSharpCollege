using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Dto;
using FifteenGame.Common.Services;
using System.Web.Http; 

namespace FifteenGame.Server.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("api/user/login")]
        public UserReply Login([FromBody] UserNameRequest request)
        {
            return _userService.Login(request);
        }

        [HttpPost]
        [Route("api/user/update-best-time")]
        public void UpdateBestTime([FromBody] UserDto user)
        {
            _userService.UpdateBestTime(user);
        }
    }
}