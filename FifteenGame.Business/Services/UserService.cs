using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Repositories;
using FifteenGame.Common.Services;
using FifteenGame.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FifteenGame.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _repository.GetAll()
                .Select(d => new UserModel { Id = d.Id, Name = d.Name, BestScore = d.BestScore })
                .ToList();
        }

        public UserModel GetUserByName(string name)
        {
            var dto = _repository.GetByName(name);
            return dto == null ? null : new UserModel { Id = dto.Id, Name = dto.Name, BestScore = dto.BestScore };
        }

        public UserModel GetOrCreateUser(string name)
        {
            var dto = _repository.GetByName(name);
            if (dto == null) dto = _repository.Create(name);
            return dto == null ? null : new UserModel { Id = dto.Id, Name = dto.Name, BestScore = dto.BestScore };
        }

        public void UpdateBestScore(int userId, int bestScore)
        {
            _repository.UpdateBestScore(userId, bestScore);
        }
    }
}