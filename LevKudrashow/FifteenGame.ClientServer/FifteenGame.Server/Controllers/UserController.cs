using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FifteenGame.Server.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController()
        {
            _userService = NinjectKernel.Instance.Get<IUserService>();
        }

        [HttpGet]
        [Route("api/user/get-all")]
        public GetAllUsersReply GetAll()
        {
            var reply = _userService.GetAllUsers();
            return new GetAllUsersReply
            {
                Users = reply.Select(ToDto).ToList(),
            };
        }

        [HttpPost]
        [Route("api/user/get-by-name")]
        public UserReply GetByName([FromBody] GetUserByNameRequest request)
        {
            var reply = _userService.GetUserByName(request.Name);
            return ToDto(reply);
        }

        [HttpPost]
        [Route("api/user/get-or-create")]
        public UserReply GetOrCreate([FromBody] GetUserByNameRequest request)
        {
            var reply = _userService.GetOrCreateUser(request.Name);
            return ToDto(reply);
        }

        public UserReply ToDto(UserModel user)
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
