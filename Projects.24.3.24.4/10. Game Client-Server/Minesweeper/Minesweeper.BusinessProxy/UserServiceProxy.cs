using Minesweeper.Common.Dto;
using Minesweeper.Common.Interfaces;
using Minesweeper.Common.Models;

namespace Minesweeper.BusinessProxy
{
    public class UserServiceProxy : ApiClient, IUserService
    {
        public User Login(string name)
        {
            var reply = Post<UserReply>("api/users/login", new UserNameRequest { Name = name });
            return new User { Id = reply.Id, Name = reply.Name };
        }

        public User GetById(int id)
        {
            var reply = Get<UserReply>("api/users/get-by-id/" + id);
            return reply == null ? null : new User { Id = reply.Id, Name = reply.Name };
        }
    }
}
