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
            try
            {
                _userService = NinjectKernel.Instance.Get<IUserService>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error initializing UserService: {ex.Message}");
                throw;
            }
        }

        [HttpGet]
        [Route("api/user/get-all")]
        public AllUsersReply GetAll()
        {
            try
            {
                var reply = _userService.GetAllUsers();
                return new AllUsersReply
                {
                    Users = reply.Select(ToDto).ToList()
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetAll: {ex.Message}");
                throw;
            }
        }

        [HttpPost]
        [Route("api/user/get-by-name")]
        public UserReply GetByName([FromBody] UserNameRequest request)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"GetByName called with: {request?.Name}");
                
                if (request == null)
                {
                    System.Diagnostics.Debug.WriteLine("Request is null");
                    return null;
                }

                var reply = _userService.GetUserByName(request.Name);
                System.Diagnostics.Debug.WriteLine($"User found: {reply != null}");
                return ToDto(reply);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetByName: {ex.Message}");
                throw;
            }
        }

        [HttpPost]
        [Route("api/user/get-or-create")]
        public UserReply GetOrCreate([FromBody] UserNameRequest request)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"GetOrCreate called with: {request?.Name}");
                
                if (request == null)
                {
                    System.Diagnostics.Debug.WriteLine("Request is null");
                    return null;
                }

                var reply = _userService.GetOrCreateUser(request.Name);
                System.Diagnostics.Debug.WriteLine($"User created/found: {reply != null}, Id: {reply?.Id}");
                return ToDto(reply);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetOrCreate: {ex.Message}");
                throw;
            }
        }

        private UserReply ToDto(UserModel user)
        {
            if (user == null)
            {
                return new UserReply { Id = 0, Name = "Unknown" };
            }

            return new UserReply { Id = user.Id, Name = user.Name, };
        }

        // Swagger
    }
}
