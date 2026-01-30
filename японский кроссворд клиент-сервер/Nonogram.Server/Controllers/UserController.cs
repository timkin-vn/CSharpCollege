using Ninject;
using Nonogram.Common.BusinessDtos;
using Nonogram.Common.BusinessModels;
using Nonogram.Common.Infrastructure;
using Nonogram.Common.Services;
using System.Linq;
using System.Web.Http;

namespace Nonogram.Server.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController()
        {
            // Получаем экземпляр через NinjectKernel
            _userService = NinjectKernel.Instance.Get<IUserService>();
        }

        [HttpGet]
        [Route("api/user/get-all")]
        public AllUsersReply GetAll()
        {
            var users = _userService.GetAllUsers();
            return new AllUsersReply
            {
                Users = users.Select(u => new UserReply
                {
                    Id = u.Id,
                    Name = u.Name
                }).ToList()
            };
        }

        [HttpPost]
        [Route("api/user/get-by-name")]
        public UserReply GetByName([FromBody] UserNameRequest request)
        {
            if (request == null) return null;

            var user = _userService.GetUserByName(request.Name);
            if (user == null) return null;

            return new UserReply
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        [HttpPost]
        [Route("api/user/get-or-create")]
        public UserReply GetOrCreate([FromBody] UserNameRequest request)
        {
            if (request == null) return null;

            var user = _userService.GetOrCreateUser(request.Name);
            if (user == null) return null;

            return new UserReply
            {
                Id = user.Id,
                Name = user.Name
            };
        }
    }
}