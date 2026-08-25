using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FifteenGame.WebApi.Controllers
{
    /// <summary>
    /// Контроллер пользователей
    /// </summary>
    public class UsersController : ApiController
    {
        private readonly IUserService _userService = NinjectKernel.Instance.Get<IUserService>();

        [HttpGet]
        [Route("api/users/get-all")]
        public UsersReply GetAllUsers()
        {
            return new UsersReply
            {
                Users = _userService.GetAllUsers().Select(ToDto).ToList(),
            };
        }

        [HttpPost]
        [Route("api/users/get-or-create")]
        public UserReply GetOrCreateUser([FromBody] UserNameRequest request)
        {
            return ToDto(_userService.GetOrCreateUser(request.UserName));
        }

        [HttpGet]
        [Route("api/users/get-by-name/{userName}")]
        public UserReply GetUserByName(string userName)
        {
            return ToDto(_userService.GetUserByName(userName));
        }

        private UserReply ToDto(UserModel user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserReply { Id = user.Id, Name = user.Name, };
        }

        // Swagger
        // Swashbuckle
    }
}
