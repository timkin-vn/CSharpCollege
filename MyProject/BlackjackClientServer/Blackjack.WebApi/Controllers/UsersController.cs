using Blackjack.Common.Contracts.Services;
using Blackjack.Common.Dtos;
using Blackjack.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blackjack.WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // POST api/users/login   { "name": "Вася" }
        [HttpPost("login")]
        public ActionResult<UserModel> Login([FromBody] UserNameRequest request)
        {
            try
            {
                return Ok(_userService.GetOrCreateUser(request?.Name));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
