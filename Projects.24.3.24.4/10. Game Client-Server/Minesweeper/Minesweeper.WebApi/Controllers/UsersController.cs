using System.Web.Http;
using Minesweeper.Common.Dto;
using Minesweeper.Common.Interfaces;

namespace Minesweeper.WebApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost, Route("login")]
        public UserReply Login([FromBody] UserNameRequest request)
        {
            var user = _service.Login(request.Name);
            return new UserReply { Id = user.Id, Name = user.Name };
        }

        [HttpGet, Route("get-by-id/{id}")]
        public UserReply GetById(int id)
        {
            var user = _service.GetById(id);
            return user == null ? null : new UserReply { Id = user.Id, Name = user.Name };
        }
    }
}
