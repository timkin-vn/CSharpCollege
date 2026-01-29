using FifteenGame.Common.Dtos;
using FifteenGame.Common.Repositories;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class UserRepositoryEF : IUserRepository
    {
        public UserDto Create(string userName)
        {
            // Сначала проверяем и создаем базу если нужно
            EnsureDatabaseExists();
            
            // Создаем пользователя в отдельном контексте
            using (var context = new FifteenGameDataContext())
            {
                try
                {
                    var newUser = new User { Name = userName };
                    context.Users.Add(newUser);
                    context.SaveChanges();

                    System.Diagnostics.Debug.WriteLine($"Created user: {newUser.Id} - {newUser.Name}");
                    return ToDto(newUser);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error creating user: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                    throw;
                }
            }
        }

        private void EnsureDatabaseExists()
        {
            using (var context = new FifteenGameDataContext())
            {
                try
                {
                    if (!context.Database.Exists())
                    {
                        context.Database.Create();
                        System.Diagnostics.Debug.WriteLine("Database created successfully");
                    }
                    else
                    {
                        // Проверяем совместимость модели
                        context.Users.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Database check failed: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine("Recreating database...");
                    
                    try
                    {
                        context.Database.Delete();
                        context.Database.Create();
                        System.Diagnostics.Debug.WriteLine("Database recreated successfully");
                    }
                    catch (Exception recreateEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to recreate database: {recreateEx.Message}");
                        throw;
                    }
                }
            }
        }

        public IEnumerable<UserDto> GetAll()
        {
            using (var context = new FifteenGameDataContext())
            {
                var users = context.Users;
                return users.Select(ToDto).ToList();
            }
        }

        public UserDto GetByName(string userName)
        {
            using (var context = new FifteenGameDataContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Name == userName);
                System.Diagnostics.Debug.WriteLine($"Looking for user: {userName}, Found: {user != null}");
                return ToDto(user);
            }
        }

        private UserDto ToDto(User u)
        {
            if (u == null)
            {
                return null;
            }

            return new UserDto { Id = u.Id, Name = u.Name, };
        }
    }
}
