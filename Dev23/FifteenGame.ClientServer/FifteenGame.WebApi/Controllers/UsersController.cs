using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FifteenGame.WebApi.Controllers
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
        public UserReply GetByName([FromBody] UserNameRequest request)
        {
            var reply = _userService.GetByName(request.UserName);
            return ToDto(reply);
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
