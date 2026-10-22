using Game2048.Common.BusinessModels;
using Game2048.Common.Contracts.Repositories;
using Game2048.DataAccess.Models;

namespace Game2048.DataAccess.Repositories;

public class UserRepositoryEF : IUserRepository
{
    private readonly Game2048DbContext _db;

    public UserRepositoryEF(Game2048DbContext db)
    {
        _db = db;
    }

    public IEnumerable<UserModel> GetAll()
        => _db.Users.Select(u => ToModel(u)).ToList();

    public UserModel? GetByName(string name)
    {
        var entity = _db.Users.FirstOrDefault(u => u.Name == name);
        return entity == null ? null : ToModel(entity);
    }

    public UserModel Create(string name)
    {
        var entity = new UserEntity { Name = name };
        _db.Users.Add(entity);
        _db.SaveChanges();
        return ToModel(entity);
    }

    private static UserModel ToModel(UserEntity e)
        => new() { Id = e.Id, Name = e.Name };
}
