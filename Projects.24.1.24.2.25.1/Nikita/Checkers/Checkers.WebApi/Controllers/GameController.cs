using Checkers.Business.Models;
using Checkers.Common.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Checkers.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public GameController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] string username)
        {
            bool exists = await _userRepository.RegisterUserAsync(username);
            if (exists)
            {
                return BadRequest(new {
                    Susccess = false,
                    ErrorMessage = "Имя занято."
                });
            }
            return Ok(new
            {
                Success = true
            });
        }
    }
}
