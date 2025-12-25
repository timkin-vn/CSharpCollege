namespace Game2048.Common.Repositories
{
    public interface IUserRepository
    {
        int CreateUser(string username);
        int GetUserIdByUsername(string username);
    }
}
