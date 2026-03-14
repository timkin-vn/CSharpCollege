using System;
using System.Web.Http;
using Pacman.Common.Interfaces.Services;

namespace Pacman.Server.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // Создать или получить пользователя
        [HttpPost]
        [Route("")]
        public IHttpActionResult GetOrCreateUser(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    return BadRequest("Name is required");

                var user = _userService.GetOrCreateUser(name);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Получить пользователя по ID
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetUser(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
