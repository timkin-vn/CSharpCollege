using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Game2048.Common.BusinessDtos;
using Game2048.Common.BusinessModels;
using Game2048.Common.Infrastructure;
using Game2048.Common.Services;
using Ninject;

namespace Game2048.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController()
        {
            _userService = NinjectKernel.Instance.Get<IUserService>();
        }

        [HttpGet]
        [Route("api/users/get-all")]
        public AllUsersReply GetAll()
        {
            var reply = _userService.GetAll();
            return new AllUsersReply
            {
                Users = reply.Select(ToDto).ToList(),
            };
        }

        [HttpPost]
        [Route("api/users/get-by-name")]
        public IHttpActionResult GetByName([FromBody] UserNameRequest request)
        {
            var user = _userService.GetByName(request.UserName);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(ToDto(user));
        }

        [HttpPost]
        [Route("api/users/create")]
        public UserReply Create([FromBody] UserNameRequest request)
        {
            var reply = _userService.Create(request.UserName);
            return ToDto(reply);
        }

        private UserReply ToDto(UserModel user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserReply
            {
                Id = user.Id,
                Name = user.Name,
            };
        }
    }
}