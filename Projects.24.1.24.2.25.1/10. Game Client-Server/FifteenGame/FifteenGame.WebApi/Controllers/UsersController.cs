using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Infrastructure;
using Ninject;
using System;
using System.Linq;
using System.Web.Http;

namespace FifteenGame.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userService =
            NinjectKernel.Instance.Get<IUserService>();

        [HttpGet]
        [Route("api/users/get-all")]
        public IHttpActionResult GetAllUsers()
        {
            return Ok(new UsersReply
            {
                Users = _userService.GetAllUsers()
                    .Select(ToDto)
                    .ToList()
            });
        }

        [HttpPost]
        [Route("api/users/get-or-create")]
        public IHttpActionResult GetOrCreateUser(
            [FromBody] UserNameRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.UserName))
            {
                return BadRequest("Введите имя пользователя.");
            }

            try
            {
                var user = _userService.GetOrCreateUser(
                    request.UserName.Trim());

                return Ok(ToDto(user));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/users/get-by-name/{userName}")]
        public IHttpActionResult GetUserByName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return BadRequest("Введите имя пользователя.");

            var user = _userService.GetUserByName(userName.Trim());

            if (user == null)
                return NotFound();

            return Ok(ToDto(user));
        }

        private UserReply ToDto(UserModel user)
        {
            if (user == null)
                return null;

            return new UserReply
            {
                Id = user.Id,
                Name = user.Name
            };
        }
    }
}