using _2048Game.Business.Services;
using _2048Game.Common.BusinessDtos;
using _2048Game.Common.BusinessModels;
using _2048Game.Common.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _2048Game.Server.Controllers
{
    /// <summary>
    /// Контроллер для пользователя
    /// </summary>
    public class UsersController : ApiController
    {
        /// <summary>
        /// ...
        /// </summary>
        private readonly IUserService _userService;
        /// <summary>
        /// Конструктор
        /// </summary>
        public UsersController()
        {
            _userService = new UserService();
        }
        /// <summary>
        /// Получение всех пользователей из БД
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
        /// Создать или вернуть существующего пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/user/get-or-create")]
        public UserReply GetOrCreate([FromBody] UserAndPasswordRequest request)
        {
            var reply = _userService.GetOrCreateUser(request.UserName, request.Password);
            return ToDto(reply);
        }
        /// <summary>
        /// Получить по имени и паролю
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/user/get-by-name-password")]
        public UserReply GetByNameAndPassword([FromBody] UserAndPasswordRequest request)
        {
            var reply = _userService.GetUserByNameAndPassword(request.UserName, request.Password);
            return ToDto(reply);
        }

        /// <summary>
        /// Получить только по имени
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/user/get-by-username-only")]
        public UserReply GetByUserNameOnly([FromBody] UserAndPasswordRequest request)
        {
            var reply = _userService.GetByUserNameOnly(request.UserName);
            return ToDto(reply);
        }
        /// <summary>
        /// Получить по ID
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
        /// <summary>
        /// Преобразование в DTO
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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
                Password = user.Password,
            };
        }
    }
}
