using Pacman.Common.Dtos;
using Pacman.Common.Services;
using System.Web.Http;

namespace Pacman.Server.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("api/user/login")]
        public UserDto Login([FromBody] string username)
        {
            return _userService.Login(username);
        }

        [HttpPost]
        [Route("api/user/score")]
        public void PostScore([FromBody] ScoreRequest request)
        {
            _userService.UpdateScore(request);
        }
    }
}