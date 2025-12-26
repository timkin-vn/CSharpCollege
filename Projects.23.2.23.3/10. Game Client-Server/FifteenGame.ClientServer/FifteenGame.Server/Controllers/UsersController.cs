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
    /// <summary>
    /// Контроллер пользователей
    /// </summary>
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController() 
        {
            _userService = NinjectKernel.Instance.Get<IUserService>();
        }

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user/get-all")]
        public AllUsersReply GetAll()
        {
            var reply = _userService.GetAllUsers();
            return new AllUsersReply
            {
                Users = reply.Select(ToDto).ToList(),
            };
        }

        /// <summary>
        /// Получить существующего пользователя по имени или создать нового
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/user/get-or-create")]
        public UserReply GetOrCreate([FromBody] UserNameRequest request)
        {
            var reply = _userService.GetOrCreateUser(request.UserName);
            return ToDto(reply);
        }

        /// <summary>
        /// Получить пользователя по имени
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/user/get-by-name")]
        public UserReply GetByName([FromBody] UserNameRequest request)
        {
            var reply = _userService.GetUserByName(request.UserName);
            return ToDto(reply);
        }

        /// <summary>
        /// Получить пользователя по id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/user/get-by-id")]
        public UserReply GetById([FromBody] UserIdRequest request)
        {
            var reply = _userService.GetUserById(request.UserId);
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

        // swagger
    }
}
