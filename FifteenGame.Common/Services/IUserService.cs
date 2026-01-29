using FifteenGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FifteenGame.Common.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetUserByName(string name);
        UserModel GetOrCreateUser(string name);
        void UpdateBestScore(int userId, int bestScore);
    }
}