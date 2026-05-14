using Minesweeper.Common.BusinessDtos;
using Minesweeper.Common.Infrastructure;
using Minesweeper.Common.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Minesweeper.Server.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получить или создать пользователя
        /// </summary>
        [HttpPost]
        [Route("get-or-create")]
        public IHttpActionResult GetOrCreateUser([FromBody] UserRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.Username))
                    return BadRequest("Username is required");

                var user = _userService.GetOrCreateUser(request.Username);
                return Ok(ApiResponse<UserResponse>.Ok(user, "User retrieved or created successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<UserResponse>.Error(ex.Message).Message);
            }
        }

        /// <summary>
        /// Получить пользователя по ID
        /// </summary>
        [HttpGet]
        [Route("{userId:int}")]
        public IHttpActionResult GetUser(int userId)
        {
            try
            {
                var user = _userService.GetUser(userId);
                if (user == null)
                    return NotFound();

                return Ok(ApiResponse<UserResponse>.Ok(user, "User found"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<UserResponse>.Error(ex.Message).Message);
            }
        }

        /// <summary>
        /// Получить пользователя по имени
        /// </summary>
        [HttpGet]
        [Route("by-name/{username}")]
        public IHttpActionResult GetUserByUsername(string username)
        {
            try
            {
                var user = _userService.GetUserByUsername(username);
                if (user == null)
                    return NotFound();

                return Ok(ApiResponse<UserResponse>.Ok(user, "User found"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<UserResponse>.Error(ex.Message).Message);
            }
        }

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();
                return Ok(ApiResponse<IEnumerable<UserResponse>>.Ok(users, "Users retrieved successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<IEnumerable<UserResponse>>.Error(ex.Message).Message);
            }
        }
    }
}