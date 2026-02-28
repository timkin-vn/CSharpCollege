using FifteenGame.Common.Dto;

namespace FifteenGame.Common.BusinessDtos
{
    public class UserReply
    {
        public UserDto User { get; set; }
        public bool HasSavedGame { get; set; }
    }
}