using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessDtos;
using System.Web.Http;

namespace FifteenGame.Server.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly ServerUserService _service = new ServerUserService();

        [HttpPost, Route("login")]
        public IHttpActionResult Login(LoginRequest req)
        {
            var user = _service.Login(req.Username, req.Password);
            if (user == null) return BadRequest("Неверные данные");
            return Ok(user);
        }

        [HttpPost, Route("register")]
        public IHttpActionResult Register(LoginRequest req)
        {
            var user = _service.Register(req.Username, req.Password);
            if (user == null) return BadRequest("Пользователь существует");
            return Ok(user);
        }

        [HttpGet, Route("top")]
        public IHttpActionResult GetTop() => Ok(_service.GetGlobalTopPlayer());
    }
}